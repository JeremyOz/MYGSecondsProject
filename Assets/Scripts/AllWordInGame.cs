using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AllWordInGame : MonoBehaviour
{
    public static AllWordInGame instance;

    [Tooltip("Contient tous les mots de la difficult� Facile")]
    public List<Word> listWordEasy = new List<Word>();
    [Tooltip("Contient tous les mots de la difficult� Moyenne")]
    public List<Word> listWordMedium = new List<Word>();
    [Tooltip("Contient tous les mots de la difficult� Difficile")]
    public List<Word> listWordHard = new List<Word>();

    private void Start()
    {
        instance = this;
    }

    // @param La difficult�.
    /* @desc R�cup�re un mot al�atoirement en fonction de la difficult�.*/
    // @return Le mot choisi al�atoirement.
    public Word GetRandomWordByDifficulty(int indexDifficulty)
    {
        if (indexDifficulty == 0)
        {
            int random = Random.Range(0, listWordEasy.Count);
            return listWordEasy[random];
        }
        else if (indexDifficulty == 1)
        {
            int random = Random.Range(0, listWordMedium.Count);
            return listWordMedium[random];
        }
        else
        {
            int random = Random.Range(0, listWordHard.Count);
            return listWordHard[random];
        }
    }
}
