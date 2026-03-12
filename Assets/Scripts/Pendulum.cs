using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public float speed = 1f;
    public float limit_x = 0;
    public float limit_y = 0;
    public float limit_z = 0;
    //public bool randomStart = false;
    //private float random = 0;

    public float rotation_x;
    public float rotation_y;
    public float rotation_z;

    Vector3 currentEulerAngles;
    Quaternion currentRotation;

    void Awake()
    {
        //if (randomStart)
        //{
        //    random = Random.Range(0f, 1f);
        //}

        rotation_x = transform.eulerAngles.x;
        rotation_y = transform.eulerAngles.y;
        rotation_z = transform.eulerAngles.z;
    }

    void Update()
    {
        float angle_x = limit_x * Mathf.Sin(Time.time * speed);
        float angle_y = limit_y * Mathf.Sin(Time.time * speed);
        float angle_z = limit_z * Mathf.Sin(Time.time * speed);
        transform.localRotation = Quaternion.Euler(rotation_x + angle_x, rotation_y + angle_y, rotation_z + +angle_z);
    }
}
