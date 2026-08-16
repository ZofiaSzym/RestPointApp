using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using TrackerPage;
using UnityEngine;
using UnityEngine.UI;

public class Calendar : MonoBehaviour
{
    private readonly List<DayMood> dayMoods = new();

    private void Start()
    {
        var trackedDays = Path.Combine(Application.persistentDataPath, "TrackedDays.txt");

        if (File.Exists(trackedDays))
        {
            var lines = File.ReadAllLines(trackedDays);

            if (DateTime.Now.Day == 1)
                File.WriteAllText(trackedDays, string.Empty);

            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                var parts = line.Split(',');
                if (parts.Length == 2)
                {
                    var day = int.Parse(parts[0]);
                    var mood = (MoodsEnum)Enum.Parse(typeof(MoodsEnum), parts[1]);
                    dayMoods.Add(new DayMood(day, mood));
                }
            }
        }

        var now = DateTime.Now;
        var year = now.Year;
        var month = now.Month;
        var daysInMonth = DateTime.DaysInMonth(year, month);

        var grid = GetComponent<GridLayoutGroup>();
        if (grid == null)
            grid = gameObject.AddComponent<GridLayoutGroup>();

        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = 7;
        grid.cellSize = new Vector2(100, 80);
        grid.spacing = new Vector2(10, 10);
        grid.padding = new RectOffset(20, 20, 20, 20);

        var firstDay = new DateTime(year, month, 1);
        var firstDayOffset = ((int)firstDay.DayOfWeek + 6) % 7;

        for (var i = 0; i < firstDayOffset; i++)
        {
            var empty = new GameObject("Empty");
            empty.transform.SetParent(transform, false);
            var rt = empty.AddComponent<RectTransform>();
            var image = empty.AddComponent<Image>();
            image.color = new Color(1, 1, 1, 0);
        }

        for (var i = 1; i <= daysInMonth; i++)
        {
            var day = new GameObject("Day" + i);
            day.transform.SetParent(transform, false);
            day.AddComponent<RectTransform>();
            day.AddComponent<CanvasRenderer>();

            var image = day.AddComponent<Image>();
            image.color = GetMoodColor(i);

            var textObj = new GameObject("Text");
            textObj.transform.SetParent(day.transform, false);

            var textRect = textObj.AddComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;

            var tmp = textObj.AddComponent<TextMeshProUGUI>();
            tmp.text = i.ToString();
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.color = Color.black;
            tmp.fontSize = 24;
        }
    }

    private Color GetMoodColor(int day)
    {
        if (!dayMoods.Exists(dm => dm.day == day))
            return Color.white;

        var mood = dayMoods.Find(dm => dm.day == day).mood;

        return mood switch
        {
            MoodsEnum.happy => new Color(0.9411765f, 0.7254902f, 0.24313725f),
            MoodsEnum.calm => new Color(0.56078434f, 0.73333335f, 0.6666667f),
            MoodsEnum.sad => new Color(0.4862745f, 0.56078434f, 0.6509804f),
            MoodsEnum.angry => new Color(0.6901961f, 0.36078432f, 0.32941177f),
            MoodsEnum.anxious => new Color(0.72156864f, 0.6392157f, 0.27058825f),
            MoodsEnum.excited => new Color(0.8901961f, 0.6039216f, 0.41960785f),
            MoodsEnum.bored => new Color(0.7176471f, 0.6745098f, 0.6117647f),
            MoodsEnum.tired => new Color(0.6117647f, 0.56078434f, 0.627451f),
            _ => Color.white
        };
    }

    public void ChangeColor(int day, MoodsEnum mood)
    {
        if (!dayMoods.Exists(dm => dm.day == day))
            dayMoods.Add(new DayMood(day, mood));
        else
            dayMoods.Find(dm => dm.day == day).mood = mood;

        var dayTransform = transform.Find("Day" + day);
        if (dayTransform != null)
        {
            var image = dayTransform.GetComponent<Image>();
            if (image != null)
                image.color = GetMoodColor(day);
        }
    }
}

internal class DayMood
{
    public int day;
    public MoodsEnum mood;

    public DayMood(int i, MoodsEnum moodsEnum)
    {
        day = i;
        mood = moodsEnum;
    }
}