using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreditsInit : MonoBehaviour {

    public List<MenuLabel> labels;

    void Start()
    {
        foreach (MenuLabel label in labels) label.WakeUp();
    }

    public void Exit()
    {
        StartCoroutine(IExit());
    }

    IEnumerator IExit()
    {
        foreach (MenuLabel label in labels) label.Sleep();

        yield return new WaitForSeconds(    2);

        Application.LoadLevel(Scenes.MainMenuWithoutIntro);
    }
}
