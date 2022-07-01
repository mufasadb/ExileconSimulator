using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuTools : MonoBehaviour
{
    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;
    [SerializeField] Slider mastVolume;
    [SerializeField] Slider SFXVolume;
    [SerializeField] Slider MusicVolume;
    [SerializeField] TMP_InputField seedInput;

    public void Start()
    {
        Settings.instance.AssignOptions();
        float tempLevel;
        tempLevel = PlayerPrefs.GetFloat("levelMaster");
        if (tempLevel != 0)
        {
            SetLevelMaster(tempLevel);
            mastVolume.SetValueWithoutNotify(tempLevel);
        }
        tempLevel = PlayerPrefs.GetFloat("levelMusic");
        if (tempLevel != 0)
        {
            SetLevelMusic(tempLevel);
            MusicVolume.SetValueWithoutNotify(tempLevel);
        }
        tempLevel = PlayerPrefs.GetFloat("levelSFX");
        if (tempLevel != 0)
        {
            SetLevelSFX(tempLevel);
            SFXVolume.SetValueWithoutNotify(tempLevel);
        }
        if (resolutionDropdown != null) HandleResolutions();
        seedInput.SetTextWithoutNotify(Settings.instance.seed.ToString());

    }
    void HandleResolutions()
    {
        int currentResolutionIndex = 0;
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) currentResolutionIndex = i;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void UpdateSeed(string _input)
    {

        Settings.instance.UpdateSeed(_input.GetHashCode());
    }
    public void UpdateTimeDisplay()
    {
        Settings.instance.UpdateTimeDisplay();
    }
    public void UpdateSortBy()
    {
        Settings.instance.UpdateSortBy();
    }
    public void ToggleSkipTutorial()
    {
        Settings.instance.ToggleSkipTutorial();
    }
    public void ToggleAutoFastForward()
    {
        Settings.instance.ToggleAutoFastForward();
    }
    public void SetLevelMaster(float sliderVal)
    {
        PlayerPrefs.SetFloat("levelMaster", sliderVal);
        AudioManager.instance.SetLevelMaster(sliderVal);
    }
    public void SetLevelMusic(float sliderVal)
    {
        PlayerPrefs.SetFloat("levelMusic", sliderVal);
        AudioManager.instance.SetLevelMusic(sliderVal);
    }
    public void SetLevelSFX(float sliderVal)
    {
        PlayerPrefs.SetFloat("levelSFX", sliderVal);
        AudioManager.instance.SetLevelSFX(sliderVal);
    }
    public void SavePlayerPrefS()
    {
        PlayerPrefs.Save();
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }





}
