using UnityEngine;
using System.Collections;

public class AudioButton : MonoBehaviour {

    MenuLabel label;

    void Awake()
    {
        label = GetComponent<MenuLabel>();
        /*
        if (AudioControl.Instance.audio)
        {
            label.SetFinalText("audio:on");
        }
        else
        {
            label.SetFinalText("audio:off");
        }*/
        
        TextMesh textMesh = GetComponent<TextMesh>();

        if (AudioControl.Instance.audio)
        {
            textMesh.text = "audio:on";
        }
        else
        {
            textMesh.text = "audio:off";
        }
    }

    void OnMouseDown()
    {
        AudioControl.Instance.audio = !AudioControl.Instance.audio;

        if (AudioControl.Instance.audio)
        {
            label.SetFinalText("audio:on");
            GetComponent<TextMesh>().text = "audio:on";
        }
        else 
        {
            label.SetFinalText("audio:off");
            GetComponent<TextMesh>().text = "audio:off";
        }
    }
}
