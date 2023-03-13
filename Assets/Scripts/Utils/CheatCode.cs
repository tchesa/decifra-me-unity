using UnityEngine;
using System.Collections;

public class CheatCode : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) Application.LoadLevel(Scenes.Game1);
        if (Input.GetKeyDown(KeyCode.F2)) Application.LoadLevel(Scenes.Game2);
        if (Input.GetKeyDown(KeyCode.F3)) Application.LoadLevel(Scenes.Game3);
        if (Input.GetKeyDown(KeyCode.F4)) Application.LoadLevel(Scenes.Game4);
        if (Input.GetKeyDown(KeyCode.F5)) Application.LoadLevel(Scenes.Game5);
        if (Input.GetKeyDown(KeyCode.F6)) Application.LoadLevel(Scenes.Game6);
        if (Input.GetKeyDown(KeyCode.F7)) Application.LoadLevel(Scenes.Game7);
        if (Input.GetKeyDown(KeyCode.F8)) Application.LoadLevel(Scenes.Game8);
        if (Input.GetKeyDown(KeyCode.F9)) Application.LoadLevel(Scenes.Game9);
        if (Input.GetKeyDown(KeyCode.F10)) Application.LoadLevel(Scenes.MainMenuWithoutIntro);
        if (Input.GetKeyDown(KeyCode.F11)) Application.LoadLevel(Scenes.MainMenuLevelSelect);
        if (Input.GetKeyDown(KeyCode.F12)) Application.LoadLevel(Scenes.GameOver);
    }
}
