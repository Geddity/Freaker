using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatUIElement : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI statValue;
    [SerializeField] TMPro.TextMeshProUGUI trainedText;

    public void Set(Stat stat, Character character)
    {
        statValue.text = stat.GetStatValue(character).ToString();
        trainedText.text = stat.trained ? "Trained" : "Untrained";
    }

    public void Set2(Stat stat, Character character)
    {
        statValue.text = stat.GetStatValue2(character).ToString();
        trainedText.text = stat.trained ? "Trained" : "Untrained";
    }

    public void Set3(Stat stat, Character character)
    {
        statValue.text = stat.GetStatValue3(character).ToString() + "%";
        trainedText.text = stat.trained ? "Trained" : "Untrained";
    }

    public void Set4(Stat stat, Character character)
    {
        statValue.text = stat.GetStatValue4(character).ToString() + "%";
        trainedText.text = stat.trained ? "Trained" : "Untrained";
    }

    public void Set5(Stat stat, Character character)
    {
        statValue.text = stat.GetStatValue5(character).ToString() + "%";
        trainedText.text = stat.trained ? "Trained" : "Untrained";
    }
}
