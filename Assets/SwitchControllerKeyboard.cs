using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchControllerKeyboard : MonoBehaviour
{
    [SerializeField]
    private GameObject objectControllerLetterInput;
    [SerializeField]
    private GameObject objectControllerVirtualKeyboard;

    private void OnEnable()
    {
        UpdateDisplayOnSettings();
    }

    public void UpdateDisplayOnSettings()
    {

        if (Settings.instance.GetIsVirtualKeyboard())
        {
            objectControllerVirtualKeyboard.SetActive(true);
            objectControllerLetterInput.SetActive(false);
        }
        else
        {
            objectControllerVirtualKeyboard.SetActive(false);
            objectControllerLetterInput.SetActive(true);
        }
    }
}
