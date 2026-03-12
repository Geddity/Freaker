using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreationCameraScript : MonoBehaviour
{
    public Camera orthoCamera;
    public Slider slider;
    public float offset = 0f;
    public bool zoomedIn = false;
    public GameObject croper;
    Vector3 newPos;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Creation_Arena")
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    public void ZoomIn()
    {
        orthoCamera.orthographicSize = 6.0f * slider.value;
        orthoCamera.transform.position = new Vector3(2f, 28.4f, -57f);

        zoomedIn = true;
        croper.SetActive(true);
    }

    public void ZoomOut()
    {
        orthoCamera.orthographicSize = 17.0f;
        orthoCamera.transform.position = new Vector3(6f, 23f, -57f);

        zoomedIn = false;
        croper.SetActive(false);
    }

    private void Update()
    {
        var pos_y = orthoCamera.transform.position.y;
        pos_y = 22 * slider.value + offset;

        var pos_x = orthoCamera.transform.position.x;
        pos_x = 2 * slider.value;

        newPos = new Vector3(pos_x, pos_y, -57f);

        if (zoomedIn)
        {
            orthoCamera.transform.position = newPos;
            orthoCamera.orthographicSize = 6.0f * slider.value;
        }
    }
}
