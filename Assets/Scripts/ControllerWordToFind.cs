using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class ControllerWordToFind : MonoBehaviour
{
    [Tooltip("Le mot que le joueur devra trouver.")]
    public string wordToFind = "-66";
    [Tooltip("L'état actuel du mot selon les lettres qu'à trouvé le joueur.")]
    public string actualWord = "";
    public TextMeshProUGUI textMeshProWordToFind;

    [SerializeField]
    private GameObject prefabParticleSystem;

    // Au démarrage de la partie.
    // @param Le mot à trouver sur cette partie.
    /* @desc 
     * Stock le mot à trouver pour la partie en cours et place un '_' pour chaque caractère. */
    public void InitializeWordToFind(Word wordToFind)
    {
        this.wordToFind = wordToFind.nameWord;

        for (int i = 0; i < wordToFind.nbLetter; i++)
        {
            actualWord += "_";
        }

        textMeshProWordToFind.text = actualWord;
    }

    // Lorsque le joueur valide la saisie (clique sur un bouton).
    // @param La lettre que le joueur a saisie.
    /* @desc
     * Compare la saisie avec le mot recherché lettre par lettre pour retrouver des correspondances et 
     * remplacer les caractères "_" par la saisie lorsque ça correspond. 
     */
    // @return -true- si la lettre est présent dans le mot à rechercher. Sinon -false-.
    public bool OnSearchLetterInWord(char letter)
    {
        bool letterFind = false;

        for (int i = 0; i < wordToFind.Length; i++)
        {
            // Compare caractère par caractère avec la saisie en ignorant les accents et les majuscules/minuscules.
            if(string.Compare(wordToFind[i].ToString(), letter.ToString(), CultureInfo.CurrentCulture, 
                CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) == 0)
            {
                letterFind = true;

                actualWord = actualWord.Remove(i, 1);
                actualWord = actualWord.Insert(i, wordToFind[i].ToString());

                textMeshProWordToFind.text = actualWord;

                Vector3 bottomLeft = textMeshProWordToFind.textInfo.characterInfo[i].bottomLeft;
                Vector3 topRight = textMeshProWordToFind.textInfo.characterInfo[i].topRight;

                float centerX = Mathf.Lerp(bottomLeft.x, topRight.x, .5f);
                
                Vector3 localCenter = new Vector3(centerX, 5.6f, bottomLeft.z);

                Vector3 worldCenter = textMeshProWordToFind.transform.TransformPoint(localCenter);

                Instantiate(prefabParticleSystem, new Vector3(worldCenter.x, worldCenter.y, worldCenter.z), Quaternion.Euler(90, 0, 0));

                CheckWordFound();
            }
        }

        return letterFind;
    }

    /* Si toutes les lettres ont été trouvé, lance la fin de la partie.*/
    public void CheckWordFound()
    {
        if (wordToFind == actualWord)
        {
            PenduGameManager.instance.EndGame(true);
        }
    }

    // Lorsqu'une nouvelle partie commence.
    public void Reset()
    {
        wordToFind = "-66";
        actualWord = "";
        textMeshProWordToFind.text = "";
    }
}
