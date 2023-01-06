using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Text.RegularExpressions;

public class ControllerVirtualKeyboard : MonoBehaviour
{
    [SerializeField]
    private UIControllerLetterTried controllerLetterTried;
    [SerializeField]
    private ControllerWordToFind controllerWordToFind;
    [SerializeField]
    private ControllerSpritePendu controllerSpritePendu;


    public void OnClickValiderButtonVirtualKeyboard(string value)
    {
        if (!controllerLetterTried.CheckIsLetterTried(value))
        {
            controllerLetterTried.AddLetterInLetterTried(value);

            // R�cup�re l'information de si la lettre est bien pr�sente dans le mot.
            bool letterFind = controllerWordToFind.OnSearchLetterInWord(value[0]);

            controllerSpritePendu.WrongLetter(letterFind);
        }
    }
}
