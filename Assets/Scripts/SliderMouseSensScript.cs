using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderMouseSensScript : MonoBehaviour
{
    public MovementController movementController;

    private void Start()
    {
        SetSensitivty();
    }

    public void SetSensitivty()
    {
        if (movementController != null)
        {
            Slider slider = GetComponent<Slider>();

            if (slider != null)
            {
                movementController.sensitivity = slider.value;
            }
            else
            {
                Debug.LogError("Slider component not found on the GameObject.");
            }
        }
        else
        {
            Debug.LogError("MovementController not assigned.");
        }
    }
}
