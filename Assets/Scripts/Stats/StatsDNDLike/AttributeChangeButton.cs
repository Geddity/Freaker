using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttributeChangeButton : MonoBehaviour, IPointerClickHandler
{
    AttributesPanel attributesPanel;
    [SerializeField] CharacterAttribute attribute;
    [SerializeField] bool negative;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (negative == false) 
        {
            attributesPanel.AddAtrScore(attribute);
        }
        else
        {
            attributesPanel.RemoveAtrScore(attribute);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        attributesPanel = GetComponentInParent<AttributesPanel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
