using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public float speed = 1f;
    Light spotlight;
    public float midIntensity = 10f;
    public float amplitude = 10f;

    private void Start()
    {
        spotlight = GetComponent<Light>();
    }

    void Update()
    {
        spotlight.intensity = amplitude * Mathf.Sin(Time.time * speed) + midIntensity;
    }
}
