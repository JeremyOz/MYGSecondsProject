using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoadingScreen : MonoBehaviour
{
    public List<GameObject> listLoadingPoint = new List<GameObject>();

    public GameObject panelLoadingScreen;

    // Au début de la préparation des éléments nécessaires pour lancer une nouvelle partie.
    /* @desc Affiche l'écran de chargement et lance la coroutine pour les animations. */
    public void ShowLoadingScreen()
    {
        panelLoadingScreen.SetActive(true);
        StartCoroutine(ActivateAnimationLoadingPoint());
    }

    // Une fois la préparation finie.
    /* @desc 
     * Réinitialise la position de tous les points après avoir interrompu l'animation. 
     * Puis retire l'écran de chargement.*/
    public void CloseLoadingScreen()
    {
        foreach (GameObject item in listLoadingPoint)
        {
            item.GetComponent<MovingPointLoading>().ResetPosition();
        }

        panelLoadingScreen.SetActive(false);
    }

    /* @desc 
     * Lance les animations de l'écran de chargement, 
     * avec un interval de 0.1 seconde entre elles.*/
    private IEnumerator ActivateAnimationLoadingPoint()
    {
        for (int i = 0; i < listLoadingPoint.Count; i++)
        {
            listLoadingPoint[i].GetComponent<MovingPointLoading>().AnimationStart();

            yield return new WaitForSeconds(.1f);
        }
    }
}
