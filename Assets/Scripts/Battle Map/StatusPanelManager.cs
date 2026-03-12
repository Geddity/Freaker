using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusPanelManager : MonoBehaviour
{
    SelectCharacter selectCharacter;

    bool isActive;
    [SerializeField] bool fixedCharacter;

    [SerializeField] GameObject statusPanelGO;
    [SerializeField] StatusPanel statusPanel;

    [SerializeField] CharacterObject currentCharacterStatus;

    private void Awake()
    {
        selectCharacter = GetComponent<SelectCharacter>();
    }

    private void Start()
    {
        StageManager stageManager = FindObjectOfType<StageManager>();
        currentCharacterStatus = stageManager.stageCharacter;
    }

    private void Update()
    {
        if(fixedCharacter == true)
        {
            statusPanel.UpdateStatus(currentCharacterStatus);
        }
        else
        {
            MouseHoverOverObject();
        } 
    }

    private void MouseHoverOverObject()
    {
        if (isActive == true)
        {
            statusPanel.UpdateStatus(currentCharacterStatus);
            if (selectCharacter.hoverOverCharacter == null)
            {
                HideStatusPanel();
                return;
            }

            if (selectCharacter.hoverOverCharacter != currentCharacterStatus)
            {
                currentCharacterStatus = selectCharacter.hoverOverCharacter;
                statusPanel.UpdateStatus(currentCharacterStatus);
                return;
            }
        }
        else
        {
            if (selectCharacter.hoverOverCharacter != null)
            {
                currentCharacterStatus = selectCharacter.hoverOverCharacter;
                ShowStatusPanel();
                return;
            }
        }
    }

    private void HideStatusPanel()
    {
        statusPanelGO.SetActive(false);
        isActive = false;
    }

    private void ShowStatusPanel()
    {
        statusPanelGO.SetActive(true);
        isActive = true;
        statusPanel.UpdateStatus(selectCharacter.hoverOverCharacter);
    }
}
