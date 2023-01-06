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

    // Une fois que la difficult� de la partie a �t� selectionn�.
    /* @desc
     * Active l'�cran de chargement, r�cup�re un mot al�atoirement en fonction de la 
     * difficult� et initialise les �l�ments n�cessaires au jeu.
     * Puis retire l'�cran de chargement en affichant la fen�tre du jeu.
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


    // Lorsque le mot est trouv�.
    // @param Est-ce que la partie est gagn�e ou non.
    /* @desc Affiche l'�cran des scores en envoyant la dur�e de la partie et si la partie est gagn�e.
     */
    public void EndGame(bool isWin)
    {
        uIScore.ShowScore(Timer.instance.GetTimer(), isWin);
    }


    // En cliquant sur le bouton Nouvelle Partie.
    /* @desc 
     * R�initialise toutes les donn�es de la partie en cours puis relance 
     * une partie en demandant de selectionner la difficult�.
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
