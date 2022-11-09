using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Mot à trouver pour le jeu du pendu.
[System.Serializable]
public class Word
{
    public string nameWord;
    public int nbLetter;
    public int indexDifficulty;

    public Word(string nameWord, int indexDifficulty)
    {
        this.nameWord = nameWord;
        nbLetter = nameWord.Length;
        this.indexDifficulty = indexDifficulty;
    }
}
