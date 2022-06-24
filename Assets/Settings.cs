using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Settings : MonoBehaviour
{
    #region Singleton

    public static Settings instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Settings");
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        int[] count = new int[10];
        for (int i = 0; i < 10000; i++)
        {
            int val = Random.Range(0, 1000);
            if (val < 100) count[0]++;
            if (val > 100 && val < 200) count[1]++;
            if (val > 200 && val < 300) count[2]++;
            if (val > 300 && val < 400) count[3]++;
            if (val > 400 && val < 500) count[4]++;
            if (val > 500 && val < 600) count[5]++;
            if (val > 600 && val < 700) count[6]++;
            if (val > 700 && val < 800) count[7]++;
            if (val > 800 && val < 900) count[8]++;
            if (val > 900 && val < 1000) count[9]++;
            if (val == 1000) count[10]++;
        }

        // for some reason random.range seems to behave better after you smash it a few 1000 times. so .. I did that

        // for (int i = 0; i < count.Length; i++)
        // {
        //     Debug.Log(count[i] + " for " + i);
        // }

    }
    #endregion
    [Header("Settings")]
    public TimeDisplay timePreference;
    public int seed;
    public bool skipTutorial = false;
    public SortBy sortBy;
    public float hiddenNeighbourTierRateMulti = 0.2f;

    [Header("uiHolders")]
    TMPro.TMP_Dropdown timeDropdown;
    TMPro.TMP_Dropdown sortByDropdown;
    GameObject options;
    GameObject tick;
    public void Quit()
    {
        Application.Quit();
    }
    public void AssignOptions()
    {
        options = GameObject.Find("Options Menu");
        timeDropdown = GameObject.Find("Time Display").GetComponent<TMPro.TMP_Dropdown>();
        sortByDropdown = GameObject.Find("Hand Sort Dropdown").GetComponent<TMPro.TMP_Dropdown>();
        tick = GameObject.Find("Skip Tutorial Tick");
        int tutInt = PlayerPrefs.GetInt("Tutorial");
        if (tutInt == 1)
        {
            ToggleSkipTutorial();
        }
        else { tick.SetActive(false); }
        options.SetActive(false);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ToggleSkipTutorial()
    {
        if (skipTutorial)
        {
            skipTutorial = false;
            PlayerPrefs.SetInt("Tutorial", 0);
            tick.SetActive(false);
        }
        else
        {
            skipTutorial = true;
            PlayerPrefs.SetInt("Tutorial", 1);

            tick.SetActive(true);
        }
        PlayerPrefs.Save();
    }
    public void UpdateSeed(int _input)
    {
        // int input;
        // int.TryParse(_input, out input);
        // Debug.Log(input);

        if (_input != 0)
        {
            seed = _input;
            Random.InitState(seed);
        }
        else
        {
            seed = Random.Range(0, 444483647);
            Random.InitState(seed);
        }

        int[] count = new int[10];
        for (int i = 0; i < 10000; i++)
        {
            int val = Random.Range(0, 1000);
            if (val < 100) count[0]++;
            if (val > 100 && val < 200) count[1]++;
            if (val > 200 && val < 300) count[2]++;
            if (val > 300 && val < 400) count[3]++;
            if (val > 400 && val < 500) count[4]++;
            if (val > 500 && val < 600) count[5]++;
            if (val > 600 && val < 700) count[6]++;
            if (val > 700 && val < 800) count[7]++;
            if (val > 800 && val < 900) count[8]++;
            if (val > 900 && val < 1000) count[9]++;
            if (val == 1000) count[10]++;
        }
    }
    public void UpdateTimeDisplay()
    {
        var val = timeDropdown.value;
        if (val == 0) timePreference = TimeDisplay.DHMRemaining;
        else if (val == 1) timePreference = TimeDisplay.HMRemaining;
        else if (val == 2) timePreference = TimeDisplay.MRemaining;
        else if (val == 3) timePreference = TimeDisplay.Time;
        else if (val == 4) timePreference = TimeDisplay.TimeMil;
    }
    public void UpdateSortBy()
    {
        var val = sortByDropdown.value;
        if (val == 0) sortBy = SortBy.Durability;
        if (val == 1) sortBy = SortBy.Type;
        if (val == 2) sortBy = SortBy.Rarity;
        if (val == 3) sortBy = SortBy.Name;
    }
}
public enum TimeDisplay
{
    DHMRemaining,
    Time,
    TimeMil,
    HMRemaining,
    MRemaining
}

public enum SortBy { Durability, Type, Rarity, Name }