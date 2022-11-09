using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Calcul la durée de la partie, qui sera ensuite utilisé pour le Score.
public class Timer : MonoBehaviour
{
    public static Timer instance;

    private float timer = 0;

    public float GetTimer() { return timer; }

    private void Start()
    {
        instance = this;
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    // Au lancement d'une nouvelle partie.
    /* @desc Réinitialise le timer pour la nouvelle partie. */
    public void RestartTimer()
    {
        timer = 0;
    }
}
