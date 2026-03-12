using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositionOnScreen : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI positionOnScreen;
    MouseInput mouseInput;

    private void Awake()
    {
        mouseInput = GetComponent<MouseInput>();
    }

    void Update()
    {
        if (mouseInput.active == true)
        {
            positionOnScreen.text = "Position: " + mouseInput.positionOnGrid.x.ToString() + ":" + mouseInput.positionOnGrid.y.ToString();
        }
        else
        {
            positionOnScreen.text = "Outside The Map";
        }
    }
}
