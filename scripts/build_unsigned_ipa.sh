#!/usr/bin/env bash

set -euo pipefail

usage() {
  cat <<'EOF'
Build an unsigned iOS IPA from an Xcode project/workspace.

Usage:
  build_unsigned_ipa.sh --path <project_or_workspace_path> [options]

Required:
  -p, --path <path>             Path to .xcodeproj, .xcworkspace, or a directory containing one.

Optional:
  -s, --scheme <name>           Shared scheme to build. Auto-detected when only one scheme exists.
  -c, --configuration <name>    Build configuration (default: Release).
  -o, --output <dir>            Output directory root (default: ./build/unsigned-ipa).
  -h, --help                    Show this help text.

Examples:
  ./scripts/build_unsigned_ipa.sh --path /tmp/MyApp.xcodeproj --scheme MyApp
  ./scripts/build_unsigned_ipa.sh --path /tmp/ios --configuration Debug --output /tmp/output
EOF
}

die() {
  echo "Error: $*" >&2
  exit 1
}

require_tool() {
  command -v "$1" >/dev/null 2>&1 || die "Required tool '$1' is not installed or not in PATH."
}

abspath() {
  local input="$1"
  if [[ -d "$input" ]]; then
    (cd "$input" && pwd -P)
    return 0
  fi

  if [[ -e "$input" ]]; then
    local dir
    dir="$(cd "$(dirname "$input")" && pwd -P)"
    echo "$dir/$(basename "$input")"
    return 0
  fi

  return 1
}

resolve_build_target() {
  local input="$1"
  local ext="${input##*.}"

  if [[ -d "$input" ]]; then
    local -a workspaces=()
    local -a projects=()
    shopt -s nullglob
    workspaces=("$input"/*.xcworkspace)
    projects=("$input"/*.xcodeproj)
    shopt -u nullglob

    if (( ${#workspaces[@]} == 1 )); then
      BUILD_KIND="workspace"
      BUILD_PATH="${workspaces[0]}"
      return 0
    fi
    if (( ${#workspaces[@]} > 1 )); then
      die "Multiple .xcworkspace files found in '$input'. Pass one explicitly via --path."
    fi

    if (( ${#projects[@]} == 1 )); then
      BUILD_KIND="project"
      BUILD_PATH="${projects[0]}"
      return 0
    fi
    if (( ${#projects[@]} > 1 )); then
      die "Multiple .xcodeproj files found in '$input'. Pass one explicitly via --path."
    fi

    die "No .xcworkspace or .xcodeproj found in '$input'."
  fi

  case "$ext" in
    xcworkspace)
      BUILD_KIND="workspace"
      BUILD_PATH="$input"
      ;;
    xcodeproj)
      BUILD_KIND="project"
      BUILD_PATH="$input"
      ;;
    *)
      die "Unsupported path '$input'. Provide a directory, .xcworkspace, or .xcodeproj."
      ;;
  esac
}

read_schemes() {
  local -a cmd=(xcodebuild)
  if [[ "$BUILD_KIND" == "workspace" ]]; then
    cmd+=(-workspace "$BUILD_PATH")
  else
    cmd+=(-project "$BUILD_PATH")
  fi
  cmd+=(-list)

  local output
  if ! output="$("${cmd[@]}" 2>&1)"; then
    echo "$output" >&2
    die "Failed to list schemes with xcodebuild."
  fi

  echo "$output" | awk '
    /Schemes:/ { in_schemes=1; next }
    in_schemes {
      if ($0 ~ /^[[:space:]]*$/) { exit }
      gsub(/^[[:space:]]+/, "", $0)
      print
    }
  '
}

PROJECT_INPUT=""
SCHEME=""
CONFIGURATION="Release"
OUTPUT_ROOT="$(pwd -P)/build/unsigned-ipa"

while [[ $# -gt 0 ]]; do
  case "$1" in
    -p|--path)
      [[ $# -ge 2 ]] || die "Missing value for $1"
      PROJECT_INPUT="$2"
      shift 2
      ;;
    -s|--scheme)
      [[ $# -ge 2 ]] || die "Missing value for $1"
      SCHEME="$2"
      shift 2
      ;;
    -c|--configuration)
      [[ $# -ge 2 ]] || die "Missing value for $1"
      CONFIGURATION="$2"
      shift 2
      ;;
    -o|--output)
      [[ $# -ge 2 ]] || die "Missing value for $1"
      OUTPUT_ROOT="$2"
      shift 2
      ;;
    -h|--help)
      usage
      exit 0
      ;;
    *)
      die "Unknown argument: $1 (use --help for usage)"
      ;;
  esac
done

[[ -n "$PROJECT_INPUT" ]] || { usage; exit 1; }

require_tool xcodebuild
require_tool zip

PROJECT_INPUT="$(abspath "$PROJECT_INPUT")" || die "Path does not exist: $PROJECT_INPUT"
if [[ "$OUTPUT_ROOT" != /* ]]; then
  OUTPUT_ROOT="$(pwd -P)/$OUTPUT_ROOT"
fi

resolve_build_target "$PROJECT_INPUT"

if [[ -z "$SCHEME" ]]; then
  schemes_output="$(read_schemes)"
  schemes=()
  while IFS= read -r scheme_line; do
    [[ -n "$scheme_line" ]] && schemes+=("$scheme_line")
  done <<< "$schemes_output"
  if (( ${#schemes[@]} == 1 )); then
    SCHEME="${schemes[0]}"
  elif (( ${#schemes[@]} == 0 )); then
    die "Could not auto-detect a scheme. Pass --scheme explicitly."
  else
    {
      echo "Multiple schemes found. Pass --scheme explicitly:"
      printf '  - %s\n' "${schemes[@]}"
    } >&2
    exit 1
  fi
fi

timestamp="$(date +"%Y%m%d-%H%M%S")"
safe_scheme="$(echo "$SCHEME" | tr '[:space:]/' '__')"
work_dir="$OUTPUT_ROOT/${safe_scheme}-${timestamp}"
archive_path="$work_dir/${safe_scheme}.xcarchive"
payload_dir="$work_dir/Payload"
ipa_path="$work_dir/${safe_scheme}-unsigned.ipa"

mkdir -p "$work_dir"

echo "Build target: $BUILD_KIND"
echo "Path: $BUILD_PATH"
echo "Scheme: $SCHEME"
echo "Configuration: $CONFIGURATION"
echo "Output: $work_dir"

xcodebuild \
  "-$BUILD_KIND" "$BUILD_PATH" \
  -scheme "$SCHEME" \
  -configuration "$CONFIGURATION" \
  -destination "generic/platform=iOS" \
  -sdk iphoneos \
  -archivePath "$archive_path" \
  SKIP_INSTALL=NO \
  CODE_SIGNING_ALLOWED=NO \
  CODE_SIGNING_REQUIRED=NO \
  CODE_SIGN_IDENTITY="" \
  DEVELOPMENT_TEAM="" \
  archive

app_path="$(find "$archive_path/Products/Applications" -maxdepth 1 -type d -name "*.app" | head -n 1)"
[[ -n "$app_path" ]] || die "No .app found in archive at '$archive_path'."

mkdir -p "$payload_dir"
cp -R "$app_path" "$payload_dir/"

(
  cd "$work_dir"
  zip -qry "$ipa_path" Payload
)

echo
echo "Unsigned IPA created:"
echo "$ipa_path"
