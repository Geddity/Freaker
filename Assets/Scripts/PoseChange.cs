using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoseChange : MonoBehaviour
{
    public EquipmentManager equipmentManager;
    public int activeSkeleton = 0;
    public int previousSkeleton;
    public AnimationHandle[] animationHandle;
    public Toggle[] toggles;

    public enum PoseState
    {
        None,
        Pose1,
        Pose2,
        Pose3
    }

    PoseState previousState, currentState;

    private void Update()
    {
        activeSkeleton = equipmentManager.activeSkeleton;

        bool skeletonChanged = previousSkeleton != activeSkeleton;
        previousSkeleton = activeSkeleton;

        if (skeletonChanged)
        {
            //PoseStateChanged();
        }

        bool stateChanged = previousState != currentState;
        previousState = currentState;

        if (stateChanged)
        {
            PoseStateChanged();
        }
    }

    public void TogglePose(int state)
    {
        if (GetComponent<Toggle>().isOn)
        {
            currentState = (PoseState)state;
        }
        else
        {
            foreach (Toggle toggle in toggles)
            {
                if (toggle.isOn == false)
                {
                    currentState = PoseState.None;
                    PoseStateChanged();
                    previousState = currentState;
                }
            }
        }
    }

    void PoseStateChanged()
    {
        // When the state changes, notify the animation handle of the new state.
        string stateName = null;
        switch (currentState)
        {
            case PoseState.Pose1:
                stateName = "pose1";
                break;
            case PoseState.Pose2:
                stateName = "pose2";
                break;
            case PoseState.Pose3:
                stateName = "pose3";
                break;
            default:
                stateName = "none";
                break;
        }

        for (int i = 0; i < animationHandle.Length;  i++)
        {
            animationHandle[i].PlayAnimationForState(stateName, 0);
            Debug.Log(animationHandle[i] + stateName);
        }
    }
}
