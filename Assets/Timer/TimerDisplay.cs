using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TimerDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    private void Update()
    {
        string timeText = GetTextForM();
        if (Settings.instance.timePreference == TimeDisplay.DHMRemaining) { timeText = GetTextForDHM(); }
        if (Settings.instance.timePreference == TimeDisplay.HMRemaining) { timeText = GetTextForHM(); }
        if (Settings.instance.timePreference == TimeDisplay.TimeMil) { timeText = GetTextForTimeMil(); }
        if (Settings.instance.timePreference == TimeDisplay.Time) { timeText = GetTextForTimeStd(); }
        text.text = timeText;
        // if (Settings.instance.timePreference == TimeDisplay.DHMRemaining) { timeText = GetTextForDHM; }
    }
    private string GetTextForTimeStd()
    {
        int days = 0;
        if (GlobalVariables.instance.timer > 8 * 60) { days = 1; }
        int hours = ((int)Mathf.FloorToInt(GlobalVariables.instance.timer - (days * 8 * 60)) / 60);
        int minutes = 60 - (int)Mathf.FloorToInt(GlobalVariables.instance.timer - hours * 60 - (days * 8 * 60));
        hours = 16 - hours;
        if (hours > 12) { hours -= 12; }
        string dayString = "Saturday";
        if (days == 1) { dayString = "Sunday"; }
        string hourString = hours.ToString();
        if (hours < 10) { hourString = "0" + hourString; }
        string minuteString = minutes.ToString();
        if (minutes < 10) { minuteString = "0" + minuteString; }
        return dayString + '\n' + hourString + ":" + minuteString;
    }
    private string GetTextForTimeMil()
    {
        int days = 0;
        if (GlobalVariables.instance.timer > 8 * 60) { days = 1; }
        int hours = ((int)Mathf.FloorToInt(GlobalVariables.instance.timer - (days * 8 * 60)) / 60);
        int minutes = 60 - (int)Mathf.FloorToInt(GlobalVariables.instance.timer - hours * 60 - (days * 8 * 60));
        hours = 16 - hours;
        string dayString = "Sunday";
        if (days == 1) { dayString = "Saturday"; }
        string hourString = hours.ToString();
        if (hours < 10) { hourString = "0" + hourString; }
        string minuteString = minutes.ToString();
        if (minutes < 10) { minuteString = "0" + minuteString; }
        return dayString + '\n' + hourString + ":" + minuteString;

    }
    private string GetTextForDHM()
    {
        int days = 0;
        if (GlobalVariables.instance.timer > 8 * 60) { days = 1; }
        int hours = ((int)Mathf.FloorToInt(GlobalVariables.instance.timer - (days * 8 * 60)) / 60);
        int minutes = (int)Mathf.FloorToInt(GlobalVariables.instance.timer - hours * 60 - (days * 8 * 60));
        return days.ToString() + " day" + '\n' + hours.ToString() + " Hours" + '\n' + minutes.ToString() + " Minutes";
    }
    private string GetTextForHM()
    {
        int hours = (int)Mathf.FloorToInt(GlobalVariables.instance.timer / 60);
        int minutes = (int)Mathf.FloorToInt(GlobalVariables.instance.timer - hours * 60);
        return hours.ToString() + " Hours" + '\n' + minutes.ToString() + " Minutes";
    }
    private string GetTextForM()
    {
        int minutes = (int)Mathf.Round(GlobalVariables.instance.timer);
        return minutes.ToString() + " Minutes";
    }
}
