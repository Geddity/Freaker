using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanel : MonoBehaviour
{
    public Character character;

    [SerializeField] AttributesPanel attributesPanel;
    [SerializeField] StatsPanel statsPanel;
    [SerializeField] StatisticPanel statisticPanel;

    private void Update()
    {
        attributesPanel.UpdatePanel(character.attributes);
        statsPanel.UpdatePanel(character);
        statisticPanel.UpdatePanel(character);
    }
}
