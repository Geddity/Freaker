using Spine.Unity.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;
    // Start is called before the first frame update
    void Start()
    {
        cam1.SetActive(true);
        cam2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateCam2()
    {
        cam2.SetActive(true);
        cam1.SetActive(false);
    }

    public void ActivateCam1()
    {
        cam1.SetActive(true);
        cam2.SetActive(false);
    }
}
