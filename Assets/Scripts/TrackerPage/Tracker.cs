using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using TrackerPage;
using UnityEngine;
using UnityEngine.UI;

public class Tracker : MonoBehaviour
{
    [SerializeField] private ButtonMood[] moodButtons;
    [SerializeField] private Calendar _calendar;

    private void Start()
    {
        foreach (var buttonMood in moodButtons)
        {
            buttonMood.button.GetComponent<Image>().color = buttonMood.color;
            buttonMood.button.transform
                .GetChild(0)
                .GetComponent<TextMeshProUGUI>()
                .text = buttonMood.mood.ToString();
            buttonMood.button.onClick.AddListener(() => OnMoodButtonClicked(buttonMood.mood));
        }
    }

    public void OnMoodButtonClicked(MoodsEnum mood)
    {
        var trackedDays = Path.Combine(Application.persistentDataPath, "TrackedDays.txt");
        var today = DateTime.Now.Day;

        if (File.Exists(trackedDays))
        {
            var lines = File.ReadAllLines(trackedDays);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 2 && int.Parse(parts[0]) == today)
                {
                    parts[1] = mood.ToString();
                    var updatedLine = string.Join(",", parts);
                    var updatedLines = new List<string>(lines);
                    var index = Array.IndexOf(lines, line);
                    updatedLines[index] = updatedLine;
                    File.WriteAllLines(trackedDays, updatedLines);
                    _calendar.ChangeColor(today, mood);
                    return;
                }
            }
        }


        var moodEntry = $"{today},{mood}\n";
        File.AppendAllText(trackedDays, moodEntry);
        _calendar.ChangeColor(today, mood);
    }
}