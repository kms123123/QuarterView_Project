using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    [SerializeField]
    GameObject tutorialUI;
    [SerializeField]
    GameObject optionUI;
    [SerializeField]
    Slider bgmSlider;
    [SerializeField]
    Slider sfxSlider;
    [SerializeField]
    AudioMixer mixer;


    public void Start()
    {
        bgmSlider.value = PlayerPrefs.GetFloat("BGM", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFX", 1f);
    }


    /// <summary>
    /// Codes related to the main menu UI
    /// </summary>
    public void OpenTutorial()
    {
        tutorialUI.SetActive(true);
    }

    public void ExitTutorial()
    {
        tutorialUI.SetActive(false);
    }

    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }
    
    public void OpenOption()
    {
        optionUI.SetActive(true);
    }

    public void ExitOption()
    {
        optionUI.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void SetBGM()
    {
        mixer.SetFloat("BGMVolume", Mathf.Log10(bgmSlider.value) * 20);
        PlayerPrefs.SetFloat("BGM", bgmSlider.value);
    }

    public void SetSFX()
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(sfxSlider.value) * 20);
        PlayerPrefs.SetFloat("SFX", sfxSlider.value);
    }
}
