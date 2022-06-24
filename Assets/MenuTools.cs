using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTools : MonoBehaviour
{
    public void Start()
    {
        Settings.instance.AssignOptions();
        float tempLevel;
        tempLevel = PlayerPrefs.GetFloat("levelMaster");
        if (tempLevel != 0) SetLevelMaster(tempLevel);
        tempLevel = PlayerPrefs.GetFloat("levelMusic");
        if (tempLevel != 0) SetLevelMusic(tempLevel);
        tempLevel = PlayerPrefs.GetFloat("levelSFX");
        if (tempLevel != 0) SetLevelSFX(tempLevel);
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
}
