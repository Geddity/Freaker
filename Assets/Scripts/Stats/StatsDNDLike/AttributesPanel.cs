using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesPanel : MonoBehaviour
{
    [SerializeField] List<AttributeUIElement> attributeUI;

    CharacterPanel characterPanel;

    private void Start()
    {
        characterPanel = GetComponentInParent<CharacterPanel>();
    }

    public void UpdatePanel(List<Attribute> attributes)
    {
        for(int i =0; i < attributeUI.Count; i++) 
        {
            attributeUI[i].Set(attributes[i]);
        }
    }

    public void AddAtrScore(CharacterAttribute attribute)
    {
        characterPanel.character.ChangeAtrScore(1, attribute);
    }

    public void RemoveAtrScore(CharacterAttribute attribute)
    {
        characterPanel.character.ChangeAtrScore(-1, attribute);
    }

}
