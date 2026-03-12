using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundChange : MonoBehaviour
{
    public GameObject curtainR;
    public GameObject curtainL;
    Vector3 targetR;
    Vector3 targetL;
    Vector3 startR;
    Vector3 startL;
    [SerializeField] private float smoothTime;
    public Vector3 _currentVelocityR = Vector3.zero;
    public Vector3 _currentVelocityL = Vector3.zero;
    public bool buttonPressed = false;
    public bool curtainsOffed = false;

    [SerializeField] private GameObject[] buttons;
    public Material[] skyboxes;

    public void Start()
    {
        curtainR = GameObject.Find("curtain"); 
        targetR = new Vector3(80, 0, 20);
        startR = new Vector3(26, 0, 20);
        curtainL = GameObject.Find("curtain (1)");
        targetL = new Vector3(-80, 0, 20.4f);
        startL = new Vector3(-26, 0, 20.4f);
    }

    public void SkyboxChange(int skyIndex)
    {
        if(curtainsOffed == false)
        {
            RenderSettings.skybox = skyboxes[skyIndex];
        }
        else
        {
            StartCoroutine(SkyDelay(skyIndex));
        }
    }

    IEnumerator SkyDelay(int skyIndex)
    {
        yield return new WaitForSeconds(2);
        RenderSettings.skybox = skyboxes[skyIndex];
    }

    public void CurtainsOff()
    {
        curtainR.transform.position = Vector3.SmoothDamp(curtainR.transform.position, targetR, ref _currentVelocityR, smoothTime);
        curtainL.transform.position = Vector3.SmoothDamp(curtainL.transform.position, targetL, ref _currentVelocityL, smoothTime);
    }

    public void CurtainsOn()
    {
        curtainR.transform.position = Vector3.SmoothDamp(curtainR.transform.position, startR, ref _currentVelocityR, smoothTime);
        curtainL.transform.position = Vector3.SmoothDamp(curtainL.transform.position, startL, ref _currentVelocityL, smoothTime);
    }

    public void ButtonPressed()
    {
        buttonPressed = true;
    }

    private void Update()
    {
        if (buttonPressed)
        {
            if (curtainsOffed == false)
            {
                CurtainsOff();
                ButtonDisable();
                StartCoroutine(WaitToUnpress());
                StartCoroutine(WaitToButton());
            }
            else
            {
                CurtainsOn();
                ButtonDisable();
                StartCoroutine(WaitToUnpress());
                StartCoroutine(WaitToOffAgain());
                StartCoroutine(WaitToButton());
            }
        }
    }

    private IEnumerator WaitToUnpress()
    {
        yield return new WaitForSeconds(1);
        buttonPressed = false;

        if(curtainR.transform.position.x >= targetR.x - 10f)
        {
            curtainsOffed = true;
        }
        else
        {
            curtainsOffed = false;
        }

        _currentVelocityR = Vector3.zero;
        _currentVelocityL = Vector3.zero;
    }
    private IEnumerator WaitToOffAgain()
    {
        yield return new WaitForSeconds(2);
        CurtainsOff();
        StartCoroutine(WaitToUnpress());
    }

    private IEnumerator WaitToButton()
    {
        yield return new WaitForSeconds(3);
        ButtonEnable();
    }

    void ButtonDisable()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].transform.GetComponent<Button>().interactable = false;
        }
    }

    void ButtonEnable()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].transform.GetComponent<Button>().interactable = true;
        }
    }
}
