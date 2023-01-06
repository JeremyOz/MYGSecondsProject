using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AllWordInGame : MonoBehaviour
{
    public static AllWordInGame instance;

    [Tooltip("Contient tous les mots de la difficulté Facile")]
    public List<Word> listWordEasy = new List<Word>();
    [Tooltip("Contient tous les mots de la difficulté Moyenne")]
    public List<Word> listWordMedium = new List<Word>();
    [Tooltip("Contient tous les mots de la difficulté Difficile")]
    public List<Word> listWordHard = new List<Word>();

    private void Start()
    {
        instance = this;
    }

    // @param La difficulté.
    /* @desc Récupère un mot aléatoirement en fonction de la difficulté.*/
    // @return Le mot choisi aléatoirement.
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
