using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (seed != 0)
        {
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
        for (int i = 0; i < count.Length; i++)
        {
            Debug.Log(count[i] + " for " + i);
        }

    }
    #endregion
    public TimeDisplay timePreference;
    public int seed;
    public SortBy sortBy;
    public float hiddenNeighbourTierRateMulti = 0.2f;

}
public enum TimeDisplay
{
    DHMRemaining,
    Time,
    TimeMil,
    HMRemaining,
    MRemaining
}

public enum SortBy { Durability, Type, Rarity }