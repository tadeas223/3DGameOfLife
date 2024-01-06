using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowMode : MonoBehaviour
{
    public void UpdateScreenState()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
