using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrainButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] CharacterStats charStats;

    StatsPanel statsPanel;

    void Start()
    {
        statsPanel = GetComponentInParent<StatsPanel>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        statsPanel.TrainStat(charStats);
    }

}
