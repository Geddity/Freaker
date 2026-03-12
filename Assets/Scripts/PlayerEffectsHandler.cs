using Spine.Unity.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEffectsHandler : MonoBehaviour
{
    public PlatformerController eventSource;
    public UnityEvent OnJump, OnLand, OnHardLand;

    public void Awake()
    {
        if (eventSource == null)
            return;

        eventSource.OnLand += OnLand.Invoke;
        eventSource.OnJump += OnJump.Invoke;
        eventSource.OnHardLand += OnHardLand.Invoke;
    }
}
