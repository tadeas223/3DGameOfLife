using UnityEngine;
using UnityEngine.UI;

public class SliderSpeedScript : MonoBehaviour
{
    public MovementController movementController;

    private void Start()
    {
        SetSpeed();
    }

    public void SetSpeed()
    {
        if (movementController != null)
        {
            Slider slider = GetComponent<Slider>();

            if (slider != null)
            {
                movementController.speed = slider.value;
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