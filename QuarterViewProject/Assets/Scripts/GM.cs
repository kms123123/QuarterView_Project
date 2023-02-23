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
    GameObject rankUI;
    [SerializeField]
    Slider bgmSlider;
    [SerializeField]
    Slider sfxSlider;
    [SerializeField]
    AudioMixer mixer;

    bool isTutorialOn;
    bool isOptionOn;
    bool isRankOn;


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
        if(!isOptionOn && !isRankOn)
        {
            tutorialUI.SetActive(true);
        }
        isTutorialOn= true;
    }

    public void ExitTutorial()
    {
        tutorialUI.SetActive(false);
        isTutorialOn= false;
    }

    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }
    
    public void OpenOption()
    {
        if (!isTutorialOn && !isRankOn)
        {
            optionUI.SetActive(true);
        }
        isOptionOn= true;
    }

    public void ExitOption()
    {
        optionUI.SetActive(false);
        isOptionOn= false;
    }

    public void OpenRank()
    {
        if (!isTutorialOn && !isOptionOn)
        {
            rankUI.SetActive(true);
        }
        isRankOn= true;
    }

    public void ExitRank()
    {
        if(!rankUI.GetComponent<RankUI>().isLoading)
        {
            rankUI.SetActive(false);
            isRankOn = false;
        }

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
