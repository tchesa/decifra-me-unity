using UnityEngine;
using System.Collections;

public class SetFullScreen : MonoBehaviour {

    void Awake()
    {
        Screen.SetResolution(1280, 854, true);
    }
}
