using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public static Settings instance;

    [SerializeField]
    private List<CanvasScaler> listCanvasScaler = new List<CanvasScaler>();

    [SerializeField]
    private bool isVirtualKeyboard;
    [SerializeField]
    private Toggle toggleVirtualKeyboard;

    [SerializeField]
    private Slider sliderEnvironment;
    [SerializeField]
    private Slider sliderEffects;

    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private AudioSource sourceTestUpdateVolume;
    [SerializeField]
    private AudioClip clipTestUpdateVolume;

    public bool GetIsVirtualKeyboard() { return isVirtualKeyboard; }

    private void Awake()
    {
        instance = this;

        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            foreach (CanvasScaler item in listCanvasScaler)
            {
                item.matchWidthOrHeight = 1;
            }
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            foreach (CanvasScaler item in listCanvasScaler)
            {
                item.matchWidthOrHeight = .5f;
            }
        }

        LoadSettings();
    }

    public void UpdateVirtualKeyboard(bool isActive)
    {
        isVirtualKeyboard = isActive;
    }

    public void UpdateVolumeEnvironment(float volume)
    {
        audioMixer.SetFloat("VolumeEnvironment", volume);
    }

    public void UpdateVolumeEffects(float volume)
    {
        audioMixer.SetFloat("VolumeEffects", volume);
    }

    public void TestVolumeEffects()
    {
        sourceTestUpdateVolume.PlayOneShot(clipTestUpdateVolume);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("settingsVirtualKeyboard", isVirtualKeyboard ? 1 : 0);

        audioMixer.GetFloat("VolumeEnvironment", out float volumeEnvironment);
        PlayerPrefs.SetInt("settingsVolumeEnvironment", (int)volumeEnvironment);

        audioMixer.GetFloat("VolumeEffects", out float volumeEffects);
        PlayerPrefs.SetInt("settingsVolumeEffects", (int)volumeEffects);
    }

    public void LoadSettings()
    {
        if (PlayerPrefs.GetInt("settingsVirtualKeyboard") == 1)
            isVirtualKeyboard = true;
        else
            isVirtualKeyboard = false;

        toggleVirtualKeyboard.isOn = isVirtualKeyboard;

        audioMixer.SetFloat("VolumeEnvironment", PlayerPrefs.GetInt("settingsVolumeEnvironment"));
        sliderEnvironment.value = PlayerPrefs.GetInt("settingsVolumeEnvironment");

        audioMixer.SetFloat("VolumeEffects", PlayerPrefs.GetInt("settingsVolumeEffects"));
        sliderEffects.value = PlayerPrefs.GetInt("settingsVolumeEffects");
    }
}
