using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    public class BookViewer : MonoBehaviour
    {
        public Image targetImage;

        public string pathBeforeNumber;
        public string pathAfterNumber;

        public int minNumber;
        public int maxNumber;

        public int startPage;

        private int currentPageNumber;

        public void Awake()
        {
            currentPageNumber = startPage;
        }

        private void SwapImage(string path)
        {
            Sprite newSprite = Resources.Load<Sprite>(path);
            if (newSprite != null)
                targetImage.sprite = newSprite;
            else
                Debug.LogWarning("Sprite not found: " + path);
        }

        public void NextPage()
        {
            if (currentPageNumber + 1 > maxNumber)
            {
                Debug.LogWarning("At the max page");
            }

            else
            {
                currentPageNumber++;
                SwapImage($"{pathBeforeNumber}{currentPageNumber}{pathAfterNumber}");
            }
        }
        
        public void PreviousPage()
        {
            if (currentPageNumber - 1 < minNumber)
            {
                Debug.LogWarning("At the min page");
            }

            else
            {
                currentPageNumber--;
                SwapImage($"{pathBeforeNumber}{currentPageNumber}{pathAfterNumber}");
            }
        }
    }
}