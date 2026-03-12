using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomizerDisabler : MonoBehaviour
{
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
}
