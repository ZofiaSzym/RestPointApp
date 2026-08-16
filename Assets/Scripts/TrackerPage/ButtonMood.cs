using System;
using UnityEngine;
using UnityEngine.UI;

namespace TrackerPage
{
    [Serializable]
    public class ButtonMood
    {
        [SerializeField] public Button button;
        [SerializeField] public MoodsEnum mood;
        [SerializeField] public Color color;
    }
}