using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPanel : MonoBehaviour
{
    [SerializeField] List<StatUIElement> statUIElements;

    CharacterPanel characterPanel;

    private void Awake()
    {
        characterPanel = GetComponentInParent<CharacterPanel>();
    }

    public void UpdatePanel(Character character)
    {
        for(int i = 0; i < statUIElements.Count; i++)
        {
            CharacterStats cc = (CharacterStats)i;

            if (i == 0 || i ==1)
            {
                statUIElements[i].Set2(character.GetCharStat(cc), character);
            }
            else if (i == 2 || i == 12 || i == 14)
            {
                statUIElements[i].Set3(character.GetCharStat(cc), character);
            }
            else if (i == 10 || i == 11)
            {
                statUIElements[i].Set4(character.GetCharStat(cc), character);
            }
            else if (i == 13 || i == 15)
            {
                statUIElements[i].Set5(character.GetCharStat(cc), character);
            }
            else
            {
                statUIElements[i].Set(character.GetCharStat(cc), character);
            }
            
        }
    }
    internal void TrainStat(CharacterStats charStats)
    {
        characterPanel.character.TrainCharStat(charStats);
    }
}
