using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class KeypadUI : MonoBehaviour
{
    [FormerlySerializedAs("Code")] public int code = 0000;

    private List<int> _currentCode = new();
    private int _currentCodeIndex = -1; // Holds what number we are currently at.

    public int sceneIDToLoadOnCorrectInput = 0;
    
    [FormerlySerializedAs("CharacterSlotOne")] public GameObject characterSlotOne;
    [FormerlySerializedAs("CharacterSlotTwo")] public GameObject characterSlotTwo;
    [FormerlySerializedAs("CharacterSlotThree")] public GameObject characterSlotThree;
    [FormerlySerializedAs("CharacterSlotFour")] public GameObject characterSlotFour;
    
    /// <summary>
    /// Check whether the keypad can support another character.
    /// </summary>
    /// <param name="index">The index in the list that we are at</param>
    /// <returns>A boolean. True if another number can be added to the keypad (or in other words the keypad does not have 4 characters yet). False if the keypad has 4 characters or more.</returns>
    private bool CheckIfCodeIsFull(int index)
    {
        if ((_currentCodeIndex + 1) /* We add one as the index is 0-based. */ <= 3)
        {
            return true;
        }

        return false;
    }


    private TextMeshProUGUI _slotOneText;
    private TextMeshProUGUI _slotTwoText;
    private TextMeshProUGUI _slotThreeText;
    private TextMeshProUGUI _slotFourText;
    
    
    void Start()
    {
        // Cache TMP components for better performance.
        _slotOneText = characterSlotOne.GetComponent<TextMeshProUGUI>();
        _slotTwoText = characterSlotTwo.GetComponent<TextMeshProUGUI>();
        _slotThreeText = characterSlotThree.GetComponent<TextMeshProUGUI>();
        _slotFourText = characterSlotFour.GetComponent<TextMeshProUGUI>();

        RenderCodeToButtons();
    }
    
    private void RenderCodeToButtons()
    {
        Debug.Log($"The current index is {_currentCodeIndex}");
        
        switch (_currentCodeIndex)
        {
            case -1:
                _slotOneText.text = "-";
                _slotTwoText.text = "-";
                _slotThreeText.text = "-";
                _slotFourText.text = "-";
                break;
            
            case 0:
                _slotOneText.text = _currentCode[0].ToString();
                _slotTwoText.text = "-";
                _slotThreeText.text = "-";
                _slotFourText.text = "-";
                break;
            case 1:
                _slotOneText.text = _currentCode[0].ToString();
                _slotTwoText.text = _currentCode[1].ToString();
                _slotThreeText.text = "-";
                _slotFourText.text = "-";
                break;
            case 2:
                _slotOneText.text = _currentCode[0].ToString();
                _slotTwoText.text = _currentCode[1].ToString();
                _slotThreeText.text = _currentCode[2].ToString();
                _slotFourText.text = "-";
                break;
            case 3:
                _slotOneText.text = _currentCode[0].ToString();
                _slotTwoText.text = _currentCode[1].ToString();
                _slotThreeText.text = _currentCode[2].ToString();
                _slotFourText.text = _currentCode[3].ToString();
                break;
        }
    }
    
    // Yes, I hate this too.
    public void OneClicked()
    {
        if (CheckIfCodeIsFull(_currentCodeIndex))
        {
            _currentCode.Add(1);
            _currentCodeIndex++;
            RenderCodeToButtons();
        }
    }
    public void TwoClicked()
    {
        if (CheckIfCodeIsFull(_currentCodeIndex))
        {
            _currentCode.Add(2);
            _currentCodeIndex++;
            RenderCodeToButtons();
        }
    }
    public void ThreeClicked()
    {
        if (CheckIfCodeIsFull(_currentCodeIndex))
        {
            _currentCode.Add(3);
            _currentCodeIndex++;
            RenderCodeToButtons();
        }
    }
    public void FourClicked()
    {
        if (CheckIfCodeIsFull(_currentCodeIndex))
        {
            _currentCode.Add(4);
            _currentCodeIndex++;
            RenderCodeToButtons();
        }
    }
    public void FiveClicked()
    {
        if (CheckIfCodeIsFull(_currentCodeIndex))
        {
            _currentCode.Add(5);
            _currentCodeIndex++;
            RenderCodeToButtons();
        }
    }
    public void SixClicked()
    {
        if (CheckIfCodeIsFull(_currentCodeIndex))
        {
            _currentCode.Add(6);
            _currentCodeIndex++;
            RenderCodeToButtons();
        }
    }
    public void SevenClicked()
    {
        if (CheckIfCodeIsFull(_currentCodeIndex))
        {
            _currentCode.Add(7);
            _currentCodeIndex++;
            RenderCodeToButtons();
        }
    }
    public void EightClicked()
    {
        if (CheckIfCodeIsFull(_currentCodeIndex))
        {
            _currentCode.Add(8);
            _currentCodeIndex++;
            RenderCodeToButtons();
        }
    }
    public void NineClicked()
    {
        if (CheckIfCodeIsFull(_currentCodeIndex))
        {
            _currentCode.Add(9);
            _currentCodeIndex++;
            RenderCodeToButtons();
        }
    }
    public void ZeroClicked()
    {
        if (CheckIfCodeIsFull(_currentCodeIndex))
        {
            _currentCode.Add(0);
            _currentCodeIndex++;
            RenderCodeToButtons();

        }
    }
    public void ClearClicked()
    {
        _currentCode.Clear();
        _currentCodeIndex = -1;
        
        RenderCodeToButtons();
    }

    IEnumerator FlashIncorrect()
    {
        _slotOneText.color = Color.red;
        _slotTwoText.color = Color.red;
        _slotThreeText.color = Color.red;
        _slotFourText.color = Color.red;
        yield return new WaitForSeconds(1);
        _slotOneText.color = Color.white;
        _slotTwoText.color = Color.white;
        _slotThreeText.color = Color.white;
        _slotFourText.color = Color.white;
    }
    
    public void EnterClicked()
    {
        if (int.Parse(string.Join("", _currentCode) /* We use String to join the List of int's here*/) /* And then compare it to an Integer as C# does not have implicit type conversions. */ == code)
        {
            SceneManager.LoadScene(sceneIDToLoadOnCorrectInput);
        }
        else
        {
            StartCoroutine(FlashIncorrect());
            ClearClicked();
        }
    }
    
    public GameObject panel;

    public void CloseUI()
    {
        panel.SetActive(false);
    }
    
}
