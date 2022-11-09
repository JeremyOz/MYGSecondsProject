using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    public GameObject panelScore;
    public GameObject panelGame;

    public TextMeshProUGUI timerValue;
    public TextMeshProUGUI nbWrongLetterValue;
    public TextMeshProUGUI difficultyValue;

    public TextMeshProUGUI timerPoints;
    public TextMeshProUGUI nbWrongLetterPoints;
    public TextMeshProUGUI difficultyPoints;

    public TextMeshProUGUI tmpScoreTotal;

    private float timer;

    private int scoreTimer;
    private int scoreWrongLetters;

    private int scoreTotal;

    // Lorsque la partie prend fin une fois le mot trouvé.
    // @param La durée de la partie.
    /* @desc Active l'écran des scores et affiche son score après calcul. */
    public void ShowScore(float timer)
    {
        this.timer = timer;

        panelGame.SetActive(false);
        panelScore.SetActive(true);

        DefineTimerScore();
        DefineWrongLetterScore();
        DefineDifficultyScore();

        tmpScoreTotal.text = scoreTotal.ToString();
    }

    /* Calcul le score selon le temps de la partie et l'affiche dans l'écran des scores. */
    public void DefineTimerScore()
    {
        scoreTimer = 5000 - (int)(timer * 30);

        TimeSpan time = TimeSpan.FromSeconds(timer);
        timerValue.text = string.Format("{0},{1} secondes", time.Seconds, time.Milliseconds);

        timerPoints.text = scoreTimer.ToString();

    }

    /* Calcul le score selon le nombre d'erreurs et l'affiche dans l'écran des scores. */
    public void DefineWrongLetterScore()
    {
        int nbWrongLetters = PenduGameManager.instance.GetWrongLetters();

        scoreWrongLetters = 0 - (300 * nbWrongLetters);

        nbWrongLetterValue.text = nbWrongLetters.ToString();
        nbWrongLetterPoints.text = scoreWrongLetters.ToString();

        scoreTotal = scoreTimer + scoreWrongLetters;
    }

    /* Calcul le score selon la difficulté et l'affiche dans l'écran des scores. */
    public void DefineDifficultyScore()
    {
        int indexDifficulty = PenduGameManager.instance.uIDifficulty.GetIndexDifficulty();

        if (indexDifficulty == 0)
        {
            difficultyValue.text = "Facile";
            difficultyPoints.text = "x1";
            scoreTotal *= 1;
        }
        else if (indexDifficulty == 1)
        {
            difficultyValue.text = "Moyenne";
            difficultyPoints.text = "x1.1";
            scoreTotal = Mathf.RoundToInt(scoreTotal * 1.1f);
        }
        else 
        {
            difficultyValue.text = "Difficile";
            difficultyPoints.text = "x1.25";
            scoreTotal = Mathf.RoundToInt(scoreTotal * 1.25f);
        }
    }
}
