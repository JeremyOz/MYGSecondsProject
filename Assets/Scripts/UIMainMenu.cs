using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : MonoBehaviour
{
    public GameObject panelMainMenu;
    public GameObject panelGame;
    
    // En cliquant sur le bouton pour afficher le menu principal.
    public void OnClickShowMainMenu()
    {
        panelGame.SetActive(false);
        panelMainMenu.SetActive(true);
    }

    // En cliquant sur le bouton pour fermer le menu principal.
    public void OnClickCloseMainMenu()
    {
        panelMainMenu.SetActive(false);
        panelGame.SetActive(true);
    }

    // En cliquant sur le bouton pour relancer une nouvelle partie.
    public void OnClickRestartGame()
    {
        panelMainMenu.SetActive(false);
        PenduGameManager.instance.RestartGame();
    }

    // En cliquant sur le bouton pour fermer le jeu.
    public void OnClickExitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
