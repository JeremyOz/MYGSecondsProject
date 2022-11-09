#if UNITY_EDITOR

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using UnityEditor;
    using UnityEngine;


    [CustomEditor(typeof(AddWordInGame))]
    public class AddWordInGame_Inspector : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            AddWordInGame myScript = (AddWordInGame)target;
            if (GUILayout.Button("Add Word In Game"))
            {
                myScript.OnButtonClick();
            }
        }
    }

    // @when Uniquement dans l'inspector en cliquant sur le bouton "Add Word In Game".
    /* [UNITY_EDITOR]
     * Parcours une cha�ne de caract�re comprenant des mots et la difficult� des mots, se pr�sentant sous 
     * la forme : motA 1 motB 1 motC 0          peut contenir autant de mot que n�cessaire.
     * Ajoute ces mots avec leur difficult� dans la liste des mots du jeu, script AllWordInGame*/
    [ExecuteInEditMode]
    public class AddWordInGame : MonoBehaviour
    {
        public AllWordInGame allWordInGame;

        [Tooltip("N�cessite un mot suivi de sa difficult� : motA 1 motB 2 motC 0 : etc...")]
        public string allWordToAdd;

        public void OnButtonClick()
        {
            allWordToAdd.ToLower();

            string regexNumberOnly = "^[0-9]$";
            string previousWord = "";

            foreach (string word in allWordToAdd.Split(' '))
            {
                if (Regex.IsMatch(word, regexNumberOnly))
                {
                    Word newWord = new Word(previousWord.Substring(0, 1).ToUpper() + previousWord.Substring(1), int.Parse(word));

                    if (word == "0")
                        allWordInGame.listWordEasy.Add(newWord);
                    else if (word == "1")
                        allWordInGame.listWordMedium.Add(newWord);
                    else
                        allWordInGame.listWordHard.Add(newWord);
                }

                previousWord = word;
            }
        }
    }

#endif
