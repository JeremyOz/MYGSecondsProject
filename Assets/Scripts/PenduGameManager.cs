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
    private GameObject panelScoreValue;

    [SerializeField]
    public UIDifficulty uIDifficulty;
    [SerializeField]
    public UILoadingScreen uILoadingScreen;
    [SerializeField]
    private AllWordInGame allWordInGame;
    [SerializeField]
    public ControllerWordToFind controllerWordToFind;
    [SerializeField]
    private ControllerSpritePendu controllerSpritePendu;
    [SerializeField]
    private ControllerInputField controllerInputField;
    [SerializeField]
    private UIControllerLetterTried controllerLetterTried;
    [SerializeField]
    private UIScore uIScore;

    public Word wordForThisGame;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        
        uIDifficulty.ShowDifficultySelection();
    }

    // @return Le nombre d'erreur commise par le joueur.
    public int GetWrongLetters() { return controllerSpritePendu.GetNumberWrongLetters(); }

    // Une fois que la difficulté de la partie a été selectionné.
    /* @desc
     * Active l'écran de chargement, récupère un mot aléatoirement en fonction de la 
     * difficulté et initialise les éléments nécessaires au jeu.
     * Puis retire l'écran de chargement en affichant la fenêtre du jeu.
     */
    public IEnumerator StartNewGame()
    {
        uILoadingScreen.ShowLoadingScreen();

        wordForThisGame = allWordInGame.GetRandomWordByDifficulty(uIDifficulty.GetIndexDifficulty());

        yield return new WaitForSeconds(3f);

        controllerWordToFind.InitializeWordToFind(wordForThisGame);

        uILoadingScreen.CloseLoadingScreen();
        panelGame.SetActive(true);

        Timer.instance.RestartTimer();
    }


    // Lorsque le mot est trouvé.
    // @param Est-ce que la partie est gagnée ou non.
    /* @desc Affiche l'écran des scores en envoyant la durée de la partie et si la partie est gagnée.
     */
    public void EndGame(bool isWin)
    {
        uIScore.ShowScore(Timer.instance.GetTimer(), isWin);
    }


    // En cliquant sur le bouton Nouvelle Partie.
    /* @desc 
     * Réinitialise toutes les données de la partie en cours puis relance 
     * une partie en demandant de selectionner la difficulté.
     */
    public void RestartGame()
    {
        panelScore.SetActive(false);
        panelScoreValue.SetActive(false);
        panelGame.SetActive(false);
        ResetAllDataInGame();

    }

    public void ResetAllDataInGame()
    {
        APIManager.instance.Reset();
        controllerSpritePendu.Reset();
        controllerInputField.Reset();
        controllerWordToFind.Reset();
        controllerLetterTried.Reset();
        uIDifficulty.ShowDifficultySelection();
    }
}
