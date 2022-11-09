using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerSpritePendu : MonoBehaviour
{
    [SerializeField]
    private Image spritePendu;

    [Tooltip("Liste de tous les sprites du pendu.")]
    [SerializeField]
    private List<Sprite> listSpritePendu = new List<Sprite>();

    [Tooltip("Nombre d'erreur commise par le joueur. Doit être à 0 initialement.")]
    [SerializeField]
    private int numberWrongLetters;

    // @return Le nombre d'erreur commise par le joueur.
    public int GetNumberWrongLetters() { return numberWrongLetters; }

    // Lorsque le joueur donne une lettre qui n'est pas présente dans le mot.
    /* @desc Augmente le nombre d'erreur commise par le joueur et change l'image du pendu en conséquence.
     */
    public void WrongLetter()
    {
        Debug.Log("letterFind wrongletter ");

        numberWrongLetters++;

        spritePendu.sprite = listSpritePendu[numberWrongLetters];
    }

    // Au lancement d'une nouvelle partie.
    public void Reset()
    {
        numberWrongLetters = 0;

        spritePendu.sprite = listSpritePendu[numberWrongLetters];
    }
}
