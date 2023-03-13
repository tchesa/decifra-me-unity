using UnityEngine;
using System.Collections;

public class ExitButton : MonoBehaviour {

    void OnMouseDown()
    {
        Application.Quit();
        AudioSource.PlayClipAtPoint(SoundArchive.Instance.menuButton, Camera.main.transform.position, 0.5f);
    }
}
