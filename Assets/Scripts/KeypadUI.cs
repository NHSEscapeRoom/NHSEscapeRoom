using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeypadUI : MonoBehaviour
{
    public int Code = 1234;
    
    List<int> CurrentCode = new List<int>( new int[4] ); 
    int CurrentCodeIndex = 0; // Holds what number we are currently at.

    public int sceneIDToLoadOnCorrectInput = 2;
    
    // Yes, I hate this too.
    public void OneClicked()
    {
        CurrentCode[CurrentCodeIndex] = 1;
    }
    public void TwoClicked()
    {
        CurrentCode[CurrentCodeIndex] = 2;
    }
    public void ThreeClicked()
    {
        CurrentCode[CurrentCodeIndex] = 3;
    }
    public void FourClicked()
    {
        CurrentCode[CurrentCodeIndex] = 4;
    }
    public void FiveClicked()
    {
        CurrentCode[CurrentCodeIndex] = 5;
    }
    public void SixClicked()
    {
        CurrentCode[CurrentCodeIndex] = 6;
    }
    public void SevenClicked()
    {
        CurrentCode[CurrentCodeIndex] = 7;
    }
    public void EightClicked()
    {
        CurrentCode[CurrentCodeIndex] = 8;
    }
    public void NineClicked()
    {
        CurrentCode[CurrentCodeIndex] = 9;
    }
    public void ZeroClicked()
    {
        CurrentCode[CurrentCodeIndex] = 0;
    }
    public void ClearClicked()
    {
        CurrentCode.Clear();
    }
    public void EnterClicked()
    {
        if (int.Parse(CurrentCode.ToString()) == Code)
        {
            SceneManager.LoadScene(sceneIDToLoadOnCorrectInput);
        }
    }
    
}
