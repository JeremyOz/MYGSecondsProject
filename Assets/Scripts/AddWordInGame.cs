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

    // Uniquement dans l'inspector en cliquant sur le bouton "Add Word In Game".
    /* [UNITY_EDITOR]
     * Lance un appel à l'API via CallAPIAddWordInGame(), retournant une liste de mot à ajouter dans le jeu.
     * Execute la méthode AddWordInList() en lui envoyant cette liste.
     * Ajoute les mots ainsi récupéré de l'API dans les listes prévues de la classe AllWordInGame.*/
    [ExecuteInEditMode]
    public class AddWordInGame : MonoBehaviour
    {
        [SerializeField]
        private APIManager aPIManager;
        [SerializeField]
        private AllWordInGame allWordInGame;

        public void OnButtonClick()
        {
            AddWordInList(aPIManager.CallAPIAddWordInGame());
        }

        public void AddWordInList(List<Word> listWord)
        {
            foreach (Word item in listWord)
            {
                if (item.indexDifficulty == 0 && !allWordInGame.listWordEasy.Contains(item))
                    allWordInGame.listWordEasy.Add(item);
                else if (item.indexDifficulty == 1 && !allWordInGame.listWordMedium.Contains(item))
                    allWordInGame.listWordMedium.Add(item);
                else if (!allWordInGame.listWordHard.Contains(item))
                    allWordInGame.listWordHard.Add(item);
            }
        }
    }

#endif
