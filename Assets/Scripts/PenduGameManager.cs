using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenduGameManager : MonoBehaviour
{
    public static PenduGameManager instance;

    [SerializeField]
    private GameObject panelGame;
    [SerializeField]
    private GameObject panelScore;

    [SerializeField]
    public UIDifficulty uIDifficulty;
    [SerializeField]
    private AllWordInGame allWordInGame;
    [SerializeField]
    private ControllerWordToFind controllerWordToFind;
    [SerializeField]
    private ControllerSpritePendu controllerSpritePendu;
    [SerializeField]
    private ControllerInputField controllerInputField;
    [SerializeField]
    private UIScore uIScore;

    // @return Le nombre d'erreur commise par le joueur.
    public int GetWrongLetters() { return controllerSpritePendu.GetNumberWrongLetters(); }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        
        uIDifficulty.ShowDifficultySelection();
    }

    // Une fois que la difficulté de la partie a été selectionné.
    // @param La difficulté de la partie.
    /* @desc
     * Active le panel du jeu, récupère un mot aléatoirement en fonction de la difficulté et initialise 
     * les éléments nécessaires au jeu */
    public void StartNewGame(int indexDifficulty)
    {
        panelGame.SetActive(true);
        Word wordForThisGame = allWordInGame.GetRandomWordByDifficulty(indexDifficulty);
        controllerWordToFind.InitializeWordToFind(wordForThisGame);
        Timer.instance.RestartTimer();
    }

    // En cliquant sur le bouton Nouvelle Partie.
    /* @desc Réinitialise toutes les données de la partie en cours puis relance une partie en demandant de selectionner la difficulté.
     */
    public void RestartGame()
    {
        panelScore.SetActive(false);
        panelGame.SetActive(false);
        controllerSpritePendu.Reset();
        controllerInputField.Reset();
        controllerWordToFind.Reset();
        uIDifficulty.ShowDifficultySelection();
    }

    // Lorsque le mot est trouvé.
    /* @desc Affiche l'écran des scores.
     */
    public void EndGame()
    {
        uIScore.ShowScore(Timer.instance.GetTimer());
    }
}
