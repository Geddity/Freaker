using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeUIElement : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI attributeScore;
    [SerializeField] TMPro.TextMeshProUGUI attributeMod;

    internal void Set(Attribute attribute)
    {
        attributeScore.text = attribute.AttributeScore.ToString();
        attributeMod.text = attribute.Mod.ToString();
    }
}
