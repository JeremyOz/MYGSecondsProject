using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Text.RegularExpressions;

public class ControllerInputField : MonoBehaviour
{
    [SerializeField]
    private UIControllerLetterTried controllerLetterTried;
    [SerializeField]
    private ControllerWordToFind controllerWordToFind;
    [SerializeField]
    private ControllerSpritePendu controllerSpritePendu;

    [SerializeField]
    private TMP_InputField inputField;

    [SerializeField]
    private TextMeshProUGUI textMeshProErrorInputField;
    private Coroutine coroutineLettreDejaEssaye;

    private string regexCharacterOnly = "^[A-Z]$";

    public void Update()
    {
        inputField.text = inputField.text.ToUpper();

        TestLongueurInput(inputField.text);

        TestRegexInput(inputField.text);
    }

    // @param Le champ de texte � v�rifier.
    /* @desc 
     * V�rifie que l'InputField ne contient pas plus d'un caract�re et garde uniquement le dernier 
     * si c'est le cas.
     */
    void TestLongueurInput(string input)
    {
        if (input.Length > 1)
        {
            inputField.text = input.Substring(input.Length - 1);
        }
    }

    // @param Le champ de texte � v�rifier.
    /* @desc 
     * V�rifie que l'InputField ne contient que des caract�res autoris�es par la jeu [A-Z]. 
     * Sinon l'efface.
     */
    void TestRegexInput(string input)
    {
        if (!Regex.IsMatch(input, regexCharacterOnly))
        {
            inputField.text = "";
        }
    }

    // @desc Lorsque le joueur appui sur le bouton pour valider la saisie et v�rifier la lettre.
    public void OnClickValiderButton()
    {
        if (inputField.text != "")
        {
            if(!controllerLetterTried.CheckIsLetterTried(inputField.text))
            {
                controllerLetterTried.AddLetterInLetterTried(inputField.text);

                // R�cup�re l'information de si la lettre est bien pr�sente dans le mot.
                bool letterFind = controllerWordToFind.OnSearchLetterInWord(inputField.text[0]);

                controllerSpritePendu.WrongLetter(letterFind);

                inputField.text = "";
            }
            else
            {
                coroutineLettreDejaEssaye = StartCoroutine(AffichageLettreDejaEssaye());
            }
        }
    }

    public void OnClickValiderButtonVirtualKeyboard(string value)
    {
        if (!controllerLetterTried.CheckIsLetterTried(value))
        {
            controllerLetterTried.AddLetterInLetterTried(value);

            // R�cup�re l'information de si la lettre est bien pr�sente dans le mot.
            bool letterFind = controllerWordToFind.OnSearchLetterInWord(value[0]);

            controllerSpritePendu.WrongLetter(letterFind);
        }
        else
        {
            coroutineLettreDejaEssaye = StartCoroutine(AffichageLettreDejaEssaye());
        }
    }


    /* @desc 
     * Affiche pendant deux secondes un message indiquant que la lettre saisie par la joueur l'a 
     * d�j� �t�.
     */
    public IEnumerator AffichageLettreDejaEssaye()
    {
        textMeshProErrorInputField.text = "Vous avez d�j� essay� cette lettre !";
        yield return new WaitForSeconds(2);
        textMeshProErrorInputField.text = "";
    }

    // Au lancement d'une nouvelle partie.
    public void Reset()
    {
        inputField.text = "";

        if(coroutineLettreDejaEssaye != null)
            StopCoroutine(coroutineLettreDejaEssaye);
    }
}
