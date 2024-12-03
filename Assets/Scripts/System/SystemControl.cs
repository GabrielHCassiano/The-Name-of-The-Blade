using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SystemControl : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderEffects;
    [SerializeField] private Toggle toggle;
    [SerializeField] private TMP_Dropdown dropResulution;

    void Start()
    {
        sliderMaster.value = PlayerPrefs.GetFloat("Master", 0.5f);
        sliderMusic.value = PlayerPrefs.GetFloat("Music", 0.5f);
        sliderEffects.value = PlayerPrefs.GetFloat("Effects", 0.5f);
        toggle.isOn = PlayerPrefs.GetInt("Full") != 1;
        dropResulution.value = PlayerPrefs.GetInt("Resolution");
    }

    private void Update()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public void SetMaster()
    {
        PlayerPrefs.SetFloat("Master", sliderMaster.value);
        audioMixer.SetFloat("Master", Mathf.Log10(sliderMaster.value) * 20);
    }

    public void SetMusic()
    {
        PlayerPrefs.SetFloat("Music", sliderMusic.value);
        audioMixer.SetFloat("Music", Mathf.Log10(sliderMusic.value) * 20);
    }
    public void SetEffects()
    {
        PlayerPrefs.SetFloat("Effects", sliderEffects.value);
        audioMixer.SetFloat("Effects", Mathf.Log10(sliderEffects.value) * 20);
    }

    public void SetFullScreen()
    {
        PlayerPrefs.SetInt("Full", toggle.isOn ? 0 : 1);
        Screen.fullScreen = toggle.isOn;
    }

    public void SetResolution()
    {
        PlayerPrefs.SetInt("Resolution", dropResulution.value);

        switch (dropResulution.value)
        {
            case 0:
                Screen.SetResolution(1920, 1080, Screen.fullScreen = toggle.isOn);
                break;
            case 1:
                Screen.SetResolution(1366, 768, Screen.fullScreen = toggle.isOn);
                break;
            case 2:
                Screen.SetResolution(1280, 720, Screen.fullScreen = toggle.isOn);
                break;
            case 3:
                Screen.SetResolution(1024, 768, Screen.fullScreen = toggle.isOn);
                break;
            case 4:
                Screen.SetResolution(800, 480, Screen.fullScreen = toggle.isOn);
                break;
        }
    }
}
