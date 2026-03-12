using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class AddAtrButton : MonoBehaviour, IPointerClickHandler
{
    LevelUpManager levelUpManager;
    [SerializeField] CharacterAttributeEnum attribute;

    public void OnPointerClick(PointerEventData eventData)
    {
        levelUpManager.AddAttributePoint(attribute);
    }

    void Start()
    {
        levelUpManager = FindObjectOfType<LevelUpManager>();
    }

}
