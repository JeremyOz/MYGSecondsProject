using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPointLoading : MonoBehaviour
{
    // Garde en mémoire la position initial de l'objet.
    Vector3 initialPosition;

    // Indique si l'objet est animé ou non.
    public bool isActive;

    // Indique si l'objet monte ou descend.
    public bool upAnimation = false;
    // La force d'impulsion (la vitesse) que l'objet reçoit
    // lorsqu'il commence à monter.
    public float initialImpulseForce;
    // Impacte la vitesse de réduction ou d'augmentation de
    // la force d'impulsion si l'objet monte ou descend.
    public float speedMultiplicatorImpulseForce;
    // Multiplicator affectant la vitesse de l'animation.
    public float animationSpeedMultiplier;
    // Indique la position locale sur l'axe 'Y' que l'objet doit
    // atteindre lors de sa chute pour remonter (rebondir).
    public float yPositionForRebounding;
    // La force d'impulsion (la vitesse) actuelle de l'objet.
    public float impulsion;

    // Affecte la distance que parcours l'objet sur l'axe X lors
    // de son animation.
    public float distanceMoving;
    // Force l'objet à se déplacer vers la gauche, sinon la droite.
    public bool isLeftMoving;

    public void AnimationStart()
    {
        initialPosition = transform.position;
        impulsion = initialImpulseForce;
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (impulsion <= 0 && upAnimation)
            {
                upAnimation = false;
            }
            else if (transform.localPosition.y <= yPositionForRebounding && !upAnimation)
            {
                upAnimation = true;
                impulsion = initialImpulseForce;

                isLeftMoving = !isLeftMoving;
            }

            transform.localPosition =
                new Vector3(
                    isLeftMoving ?
                        transform.localPosition.x + Time.deltaTime * animationSpeedMultiplier * distanceMoving
                        :
                        transform.localPosition.x - Time.deltaTime * animationSpeedMultiplier * distanceMoving,
                    upAnimation ?
                        transform.localPosition.y + Time.deltaTime * (speedMultiplicatorImpulseForce + impulsion) * animationSpeedMultiplier
                        :
                        transform.localPosition.y - Time.deltaTime * (speedMultiplicatorImpulseForce + impulsion) * animationSpeedMultiplier,
                    0);

            if (upAnimation)
                impulsion -= Time.deltaTime * (speedMultiplicatorImpulseForce + impulsion) * animationSpeedMultiplier;
            else
                impulsion += Time.deltaTime * (speedMultiplicatorImpulseForce + impulsion) * animationSpeedMultiplier;
        }
    }

    public void ResetPosition()
    {
        isActive = false;
        upAnimation = false;
        isLeftMoving = false;
        transform.position = initialPosition;
    }
}
