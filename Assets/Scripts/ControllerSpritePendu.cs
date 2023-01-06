using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerSpritePendu : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particleBackgroundLifeIndicator;
    [SerializeField]
    private ParticleSystem particleWrong;
    [SerializeField]
    private ParticleSystem particleGood;

    [SerializeField]
    private AudioSource audioWrong;
    [SerializeField]
    private AudioSource audioGood;

    [SerializeField]
    private GameObject prefabParticleInsertLetterFind;

    [SerializeField]
    private Image actualSprite;

    [SerializeField]
    private Image lifeBar;

    [Tooltip("Liste de tous les sprites du pendu.")]
    [SerializeField]
    private List<Sprite> listSpritePendu = new List<Sprite>();

    [Tooltip("Nombre d'erreur commise par le joueur. Doit être à 0 initialement.")]
    [SerializeField]
    private int numberWrongLetters;

    private void Start()
    {
        UpdateLifeParticleAndBar(0);

        RectTransform rt = lifeBar.gameObject.GetComponent<RectTransform>();
        lifeBar.material.SetVector("_Size", rt.sizeDelta);

        ActiveNextSprite();
    }

    // @return Le nombre d'erreur commise par le joueur.
    public int GetNumberWrongLetters() { return numberWrongLetters; }

    // Lorsque le joueur donne une lettre qui n'est pas dans le mot.
    /* @desc 
     * Active les effets de particules et audio nécessaires.
     * Ajuste la vie restante en cas d'erreur.
     */
    public void WrongLetter(bool letterIsFind)
    {
        if(letterIsFind)
        {
            particleGood.Play();
            audioGood.Play();
        }
        else
        {
            particleWrong.Play();
            audioWrong.Play();

            numberWrongLetters++;
            UpdateLifeParticleAndBar(numberWrongLetters);

            ActiveNextSprite();

            if (numberWrongLetters == 6)
            {
                PenduGameManager.instance.EndGame(false);
            }
        }
    }

    private void ActiveNextSprite()
    {
        actualSprite.sprite = listSpritePendu[numberWrongLetters];
    }

    private void UpdateLifeParticleAndBar(int numberWrong)
    {
        Material material = lifeBar.material;
        material.SetFloat("_Vie", 6 - numberWrong);

        ParticleSystemRenderer particleSystemRenderer = 
            (ParticleSystemRenderer)particleBackgroundLifeIndicator
            .GetComponent<Renderer>();
        material = particleSystemRenderer.material;
        material.SetFloat("_LifeCurrent", 6 - numberWrong);
    }

    // Au lancement d'une nouvelle partie.
    public void Reset()
    {
        numberWrongLetters = 0;
        actualSprite.sprite = listSpritePendu[numberWrongLetters];
        UpdateLifeParticleAndBar(0);
    }
}
