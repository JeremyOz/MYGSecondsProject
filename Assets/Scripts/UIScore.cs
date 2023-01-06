using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
    public GameObject panelScore;
    public GameObject panelScoreValue;
    public GameObject panelGame;

    public ParticleSystem loose;
    public ParticleSystem victory;

    public TextMeshProUGUI winOrLoose;

    public TextMeshProUGUI worldToFind;
    public TextMeshProUGUI worldToFindLinkDefinition;
    public List<string> listMessageLoose = new List<string>();

    public TextMeshProUGUI timerValue;
    public TextMeshProUGUI nbWrongLetterValue;
    public TextMeshProUGUI difficultyValue;

    public TextMeshProUGUI timerPoints;
    public TextMeshProUGUI nbWrongLetterPoints;
    public TextMeshProUGUI difficultyPoints;

    public TextMeshProUGUI tmpScoreTotal;

    public List<Image> listImageNumber = new List<Image>();
    public List<Sprite> listSpriteNumber = new List<Sprite>();

    private float timer;

    private int scoreTimer;
    private int scoreWrongLetters;

    private int scoreTotal;

    private bool updateScore;


    // Lorsque la partie prend fin une fois le mot trouvé.
    // @param La durée de la partie.
    /* @desc Active l'écran des scores et affiche son score après calcul. */
    public void ShowScore(float timer, bool isWin)
    {
        this.timer = timer;

        panelGame.SetActive(false);
        panelScore.SetActive(true);
        panelScoreValue.SetActive(true);

        worldToFind.text = PenduGameManager.instance.wordForThisGame.nameWord;
        worldToFindLinkDefinition.text = PenduGameManager.instance.wordForThisGame.linkDefinition;

        if(isWin)
        {
            winOrLoose.text = "Vous avez trouvé !";
            winOrLoose.color = Color.green;
            victory.Play();
        }
        else
        {
            int randomValue = UnityEngine.Random.Range(0, listMessageLoose.Count);
            winOrLoose.text = listMessageLoose[randomValue];
            winOrLoose.color = Color.red;
            loose.Play();
        }

        DefineTimerScore(isWin);
        DefineWrongLetterScore();
        DefineDifficultyScore();

        StartCoroutine(StartAnimationScore());
        
        updateScore = true;
    }

    public void OpenURL()
    {
        Application.OpenURL(PenduGameManager.instance.wordForThisGame.linkDefinition);
    }

    // Calcul le score selon le temps de la partie et l'affiche dans l'écran des scores.
    public void DefineTimerScore(bool isWin)
    {
        if (isWin)
            scoreTimer = 5000 - (int)(timer * 30);
        else
            scoreTimer = 0;

        TimeSpan time = TimeSpan.FromSeconds(timer);
        timerValue.text = string.Format("{0} secondes", time.TotalSeconds);

        scoreTotal = scoreTimer;
        timerPoints.text = scoreTimer.ToString();
    }

    // Calcul le score selon le nombre d'erreurs et l'affiche dans l'écran des scores.
    public void DefineWrongLetterScore()
    {
        int nbWrongLetters = PenduGameManager.instance.GetWrongLetters();

        scoreWrongLetters = 0 - (300 * nbWrongLetters);

        nbWrongLetterValue.text = nbWrongLetters.ToString();

        scoreTotal += scoreWrongLetters;
        nbWrongLetterPoints.text = scoreWrongLetters.ToString();
    }

    // Calcul le score selon la difficulté et l'affiche dans l'écran des scores.
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

        if (scoreTotal < 0)
            scoreTotal = 0;
    }

    public IEnumerator StartAnimationScore()
    {
        int index = 0;
        float timer = 0;
        int countImageUnactive = 0;

        tmpScoreTotal.text = scoreTotal.ToString();

        do
        {
            foreach (Image item in listImageNumber)
            {
                if(item.gameObject.activeSelf)
                    item.sprite = listSpriteNumber[index];
            }

            yield return new WaitForSeconds(.025f);
            timer += .025f;

            if (timer >= .4f)
            {
                listImageNumber[countImageUnactive].gameObject.SetActive(false);
                countImageUnactive++;

                timer -= .4f;
            }

            index++;

            if (index == 10)
                index = 0;
        }
        while (updateScore);
    }
}

