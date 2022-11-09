using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Calcul la dur�e de la partie, qui sera ensuite utilis� pour le Score.
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
    /* @desc R�initialise le timer pour la nouvelle partie. */
    public void RestartTimer()
    {
        timer = 0;
    }
}
