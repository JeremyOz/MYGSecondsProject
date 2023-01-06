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
    public string linkDefinition;


    public Word(string nameWord, string linkDefinition, int indexDifficulty)
    {
        this.nameWord = nameWord;
        nbLetter = nameWord.Length;
        this.linkDefinition = linkDefinition;
        this.indexDifficulty = indexDifficulty;
    }

}
