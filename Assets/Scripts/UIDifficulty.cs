using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDifficulty : MonoBehaviour
{
    [SerializeField]
    private GameObject uIPanelSelectDifficulty;
    private int indexDifficulty;

    // Renvoi la difficulté selectionné.
    public int GetIndexDifficulty() { return indexDifficulty; }
    
    // Au démarrage d'une nouvelle partie.
    /* @desc Affiche le menu demandant au joueur de selectionner 
     * la difficulté de la prochaine partie. */
    public void ShowDifficultySelection()
    {
        uIPanelSelectDifficulty.SetActive(true);
    }

    // Trois boutons appelent cette méthode.
    // @param Indique la difficulté    0 = Facile   |   1 = Moyenne   |   2 = Difficule
    public void SelectDifficulty(int value)
    {
        indexDifficulty = value;
        CloseDifficultySelection();
        StartCoroutine(PenduGameManager.instance.StartNewGame());
    }

    // En selectionnant la difficulté.
    /* Ferme le menu difficulté. */
    public void CloseDifficultySelection()
    {
        uIPanelSelectDifficulty.SetActive(false);
    }
}
