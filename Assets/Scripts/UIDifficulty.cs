using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDifficulty : MonoBehaviour
{
    [SerializeField]
    private GameObject uIPanelSelectDifficulty;
    private int indexDifficulty;

    // Renvoi la difficult� selectionn�.
    public int GetIndexDifficulty()
    {
        return indexDifficulty;
    }
    
    // Au d�marrage d'une nouvelle partie.
    /* @desc Affiche le menu demandant au joueur de selectionner la difficult� de la prochaine partie. */
    public void ShowDifficultySelection()
    {
        uIPanelSelectDifficulty.SetActive(true);
    }

    // En selectionnant la difficult�.
    /* Ferme le menu difficult�. */
    public void CloseDifficultySelection()
    {
        uIPanelSelectDifficulty.SetActive(false);
    }

    // Trois boutons appelent cette m�thode.
    // @param Indique la difficult�    0 = Facile   |   1 = Moyenne   |   2 = Difficule
    public void SelectDifficulty(int value)
    {
        indexDifficulty = value;
        CloseDifficultySelection();

        PenduGameManager.instance.StartNewGame(value);
    }
}
