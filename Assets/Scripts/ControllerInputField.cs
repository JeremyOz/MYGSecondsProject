using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Text.RegularExpressions;

public class ControllerInputField : MonoBehaviour
{
    [SerializeField]
    private ControllerWordToFind controllerWordToFind;
    [SerializeField]
    private ControllerSpritePendu controllerSpritePendu;

    [SerializeField]
    private TMP_InputField inputField;
    [SerializeField]
    private TextMeshProUGUI letterTried;

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
    /* @desc V�rifie que l'InputField ne contient pas plus d'un caract�re et garde uniquement le dernier si c'est le cas.*/
    void TestLongueurInput(string input)
    {
        if (input.Length > 1)
        {
            inputField.text = input.Substring(input.Length - 1);
        }
    }

    // @param Le champ de texte � v�rifier.
    /* @desc V�rifie que l'InputField ne contient que des caract�res autoris�es par la jeu [A-Z]. Sinon l'efface.*/
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
        if (inputField.text != "" && !letterTried.text.Contains(inputField.text))
        {
            AddLetterInLetterTried();

            // R�cup�re l'information de si la lettre est bien pr�sente dans le mot.
            bool letterFind = controllerWordToFind.OnSearchLetterInWord(inputField.text[0]);

            if (!letterFind)
            {
                controllerSpritePendu.WrongLetter();
            }

            inputField.text = "";
        }
        else if(letterTried.text.Contains(inputField.text))
        {
            coroutineLettreDejaEssaye = StartCoroutine(AffichageLettreDejaEssaye());
        }
    }

    /* @desc Affiche la lettre essay� par et pour le joueur.*/
    private void AddLetterInLetterTried()
    {
        if (letterTried.text != "")
            letterTried.text = letterTried.text + " | " + inputField.text;
        else
            letterTried.text = inputField.text;
    }

    /* @desc Affiche pendant deux secondes un message indiquant que la lettre saisie par la joueur l'a d�j� �t�.*/
    public IEnumerator AffichageLettreDejaEssaye()
    {
        textMeshProErrorInputField.text = "Vous avez d�j� essay� cette lettre !";
        yield return new WaitForSeconds(2);
        textMeshProErrorInputField.text = "";
    }

    // Au lancement d'une nouvelle partie.
    public void Reset()
    {
        letterTried.text = "";
        inputField.text = "";

        if(coroutineLettreDejaEssaye != null)
            StopCoroutine(coroutineLettreDejaEssaye);
    }
}
