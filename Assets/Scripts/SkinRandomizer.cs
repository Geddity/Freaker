using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity.Examples;

public class SkinRandomizer : MonoBehaviour
{
    SkinChange skinChange;
    void Start()
    {
        skinChange = GetComponent<SkinChange>();
        skinChange.RandomSkin();
    }
}
