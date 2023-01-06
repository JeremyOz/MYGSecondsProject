using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    public static APIManager instance;

    private bool wordIsDefine;
    // API url
    private string apiKey = "lG65Nl_yiiNO-woLEYlrPMBeB0CU3md2";
    private string apiUrl = "https://api.dicolink.com/v1/mots/motsauhasard?avecdef=true&minlong=4&maxlong=-1&verbeconjugue=false&limite=50&api_key=";
    // private string apiUrl = "https://api.dicolink.com/v1/mots/motauhasard?avecdef=true&minlong=4&maxlong=20&verbeconjugue=false&api_key=";

    public TextMeshProUGUI connexionLoadingFailed;

    private List<Word> listWordsToAddInGame;
    public Word word;

    public bool requestInProgress;
    public float timeRequest = 0;


    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (requestInProgress)
            timeRequest += Time.deltaTime;
    }

    public void CallAPIFindWordToGame()
    {
        requestInProgress = true;

        StartCoroutine(GetRequest(apiUrl + apiKey, LoadJsonDataCallBackFindWordToGame));
    }

    public List<Word> CallAPIAddWordInGame()
    {
        requestInProgress = true;

        StartCoroutine(GetRequest(apiUrl + apiKey, LoadJsonDataCallBackAddWordInGame));

        return listWordsToAddInGame;
    }

    private IEnumerator GetRequest(string url, Action<string> callback)
    {
        wordIsDefine = false;

        string response;

        do
        {
            UnityWebRequest www = UnityWebRequest.Get(url);
            www.downloadHandler = new DownloadHandlerBuffer();

            yield return www.SendWebRequest();

            if(www.result == UnityWebRequest.Result.ProtocolError)
            {
                response = "Protocol Error";
            }
            else if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                response = "Connection Error";
            }
            else if (www.result == UnityWebRequest.Result.DataProcessingError)
            {
                response = "Data Error";
            }
            else
            {
                response = www.downloadHandler.text;
            }

            callback(response);
        } while (!wordIsDefine);
    }

    private void LoadJsonDataCallBackFindWordToGame(string res)
    {
        if (res == "Protocol Error" || res == "Data Error")
        {
            connexionLoadingFailed.text = "Erreur lors de la r�cup�ration des donn�es du serveur.\nContacter le support si le probl�me persiste.";
        }
        else if (res == "Connection Error")
        {
            connexionLoadingFailed.text = "Connexion impossible avec le serveur, r�cup�ration du mot en attente.\nV�rifier votre connexion internet.";
        }
        else if (res != null)
        {
            connexionLoadingFailed.text = "";

            RootRandomWordAPI wordsFind = JsonUtility.FromJson<RootRandomWordAPI>("{\"randomWord\":" + res + "}");

            foreach (RandomWordAPI randomWord in wordsFind.randomWord)
            {
                int difficultyWord = EstimateDifficultyWord(randomWord.mot);

                if (difficultyWord == PenduGameManager.instance.uIDifficulty.GetIndexDifficulty())
                {
                    word = new Word(randomWord.mot.Substring(0, 1).ToUpper() + randomWord.mot.Substring(1),
                        randomWord.dicolinkUrl, difficultyWord);

                    wordIsDefine = true;

                    break;
                }
            }

            if (!wordIsDefine)
                return;
        }
        else
        {
            // Afficher erreur
        }
        requestInProgress = false;
    }

    private void LoadJsonDataCallBackAddWordInGame(string res)
    {
        listWordsToAddInGame.Clear();

        if (res == "Protocol Error" || res == "Data Error")
        {
            connexionLoadingFailed.text = "Erreur lors de la r�cup�ration des donn�es du serveur.\nContacter le support si le probl�me persiste.";
        }
        else if (res == "Connection Error")
        {
            connexionLoadingFailed.text = "Connexion impossible avec le serveur, r�cup�ration du mot en attente.\nV�rifier votre connexion internet.";
        }
        else if (res != null)
        {
            connexionLoadingFailed.text = "";

            RootRandomWordAPI wordsFind = JsonUtility.FromJson<RootRandomWordAPI>("{\"randomWord\":" + res + "}");

            foreach (RandomWordAPI randomWord in wordsFind.randomWord)
            {
                int difficultyWord = EstimateDifficultyWord(randomWord.mot);

                Word word = new Word(randomWord.mot.Substring(0, 1).ToUpper() + randomWord.mot.Substring(1),
                        randomWord.dicolinkUrl, difficultyWord);

                listWordsToAddInGame.Add(word);
            }
        }
        else
        {
            // Afficher erreur
        }

        wordIsDefine = true;
        requestInProgress = false;
    }

    public int EstimateDifficultyWord(string mot)
    {
        // Difficult� = 0, 1 ou 2 | Facile, Moyen, Difficile

        /*
            Character 8+ : +1
            Lettre identique 3* && 10- charactere : -1
            Aucune lettre identique : +1
            Voyelle 50%+ : -1
            Voyelle 20%- : +1
            Contient 2 charactere rare : +1  (w x y z)
         */

        // Donnera l'index de la difficult� du mot
        int difficultyEstimate = 0;

        if (mot.Length > 8)
            difficultyEstimate++;

        int countVoyelle = 0;
        int countRareCaractere = 0;
        bool threeIdenticalLetters = false;
        bool neverIdenticalLetters = true;

        // On boucle sur tous les caract�res du mot pour les tests.
        foreach (char charToCount in mot)
        {
            // Si un caract�re rare est pr�sent, on le comptabilise.
            if (mot.Contains("w") || mot.Contains("x") || mot.Contains("z") || mot.Contains("y") ||
                mot.Contains("W") || mot.Contains("X") || mot.Contains("Z") || mot.Contains("Y"))
                countRareCaractere++;


            // Si une voyelle est pr�sente on la comptabilise.
            if (string.Compare(charToCount.ToString(), "a", CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) == 0)
                countVoyelle++;
            else if (string.Compare(charToCount.ToString(), "e", CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) == 0)
                countVoyelle++;
            else if (string.Compare(charToCount.ToString(), "i", CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) == 0)
                countVoyelle++;
            else if (string.Compare(charToCount.ToString(), "o", CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) == 0)
                countVoyelle++;
            else if (string.Compare(charToCount.ToString(), "u", CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) == 0)
                countVoyelle++;

            // On d�fini une variable � 0 qui calculera les occurences d'un m�me caract�re dans un mot.
            int countSameChar = 0;

            // On parcourt de nouveau tous les caract�res du mot pour les comparer au caract�re en cours de v�rification.
            foreach (char c in mot)
            {
                // Si les deux caract�res sont identiques, on le comptabilise, ce qui arrive obligatoirement au moins 1 fois.
                if (charToCount == c)
                    countSameChar++;
            }

            // Si nous avons au moins deux caract�res identiques, on le garde en m�moire.
            if (countSameChar > 1)
            {
                neverIdenticalLetters = false;
            }

            // Si nous avons trois fois le m�me caract�re dans un mot de 10 caract�res ou moins, on le garde en m�moire.
            if (countSameChar >= 3 && mot.Length <= 10)
            {
                threeIdenticalLetters = true;
            }
        }

        // Selon les v�rifications pr�c�dentes, on ajuste la difficult�.
        if (countRareCaractere >= 2)
            difficultyEstimate++;
        if (threeIdenticalLetters)
            difficultyEstimate--;
        if (neverIdenticalLetters)
            difficultyEstimate++;

        // On calcule le pourcentage de voyelle pr�sent dans le mot.
        int pourcentageVoyelle = Mathf.RoundToInt((float)countVoyelle / mot.Length * 100f);

        // Selon le pourcentage, on ajuste la difficult�.
        if (pourcentageVoyelle >= 50)
            difficultyEstimate--;
        else if (pourcentageVoyelle < 20)
            difficultyEstimate++;
     
        // Si la difficult� ne compte pas une valeur autoris�, on l'ajuste.
        if (difficultyEstimate < 0)
            difficultyEstimate = 0;
        else if (difficultyEstimate > 2)
            difficultyEstimate = 2;

        Debug.Log("difficultyEstimate : " + difficultyEstimate);

        // On retourne la difficult�.
        return difficultyEstimate;
    }

    public void Reset()
    {
        wordIsDefine = false;

        word = null;
        timeRequest = 0;
    }

    [Serializable]
    public class RandomWordAPI
    {
        public string mot;
        public string dicolinkUrl;
    }

    [Serializable]
    public class RootRandomWordAPI
    {
        public RandomWordAPI[] randomWord;
    }
}
