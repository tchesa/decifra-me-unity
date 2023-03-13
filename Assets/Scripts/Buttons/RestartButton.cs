using UnityEngine;
using System.Collections;

public class RestartButton : MonoBehaviour {

    public FadeInstantiate fade;

    void OnMouseDown()
    {
        if(SoundArchive.Instance.restart)
			AudioSource.PlayClipAtPoint(SoundArchive.Instance.restart, Camera.main.transform.position, 0.5f);
		else
			Debug.LogWarning("Null Sound: restart");

        FadeInstantiate fadeInstance = (FadeInstantiate)Instantiate(fade, new Vector3(29.36547f, -4.742213f, 8.186385f), transform.rotation);
        fadeInstance.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        fadeInstance.levelIndex = Application.loadedLevel;
    }
}
