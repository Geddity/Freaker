using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PoseChange;

public class CropTarget : MonoBehaviour
{
    public EquipmentManager equipmentManager;
    public int activeSkeleton;
    public Transform[] target;
    public Vector3[] offset;

    private void Update()
    {
        activeSkeleton = equipmentManager.activeSkeleton;

        transform.position = target[activeSkeleton].position + offset[activeSkeleton];

        CropScaler();
    }

    private void CropScaler()
    {
        switch (activeSkeleton)
        {
            case 0:
                transform.localScale = new Vector3(1f, 1f, 1f);
                break;
            case 1:
                transform.localScale = new Vector3(0.94f, 0.94f, 0.94f);
                break;
            default:
                break;
        }
    }
}
