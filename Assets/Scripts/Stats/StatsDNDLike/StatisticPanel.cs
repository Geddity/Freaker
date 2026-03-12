using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticPanel : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI pointsText;
    public void UpdatePanel(Character character)
    {
        Statistic aPointStat = character.GetStats(CharacterOtherStat.AttributePoints);
        pointsText.text = aPointStat.GetStatisticValue(character).ToString();
    }
}
