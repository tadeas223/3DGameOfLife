using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsOpenScript : MonoBehaviour
{
    public GameObject settings;
    public MovementController movementController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settings.activeSelf)
            {
                movementController.SetLockState();
            }
            else
            {
                movementController.UnsetLockState();
            }
            settings.SetActive(!settings.activeSelf);

        }
    }
}
