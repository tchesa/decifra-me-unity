using UnityEngine;
using System.Collections;

public class MainMenuButton : MonoBehaviour
{
    public FadeInstantiate fade;

    void OnMouseDown()
    {
		if(SoundArchive.Instance.exit)
			AudioSource.PlayClipAtPoint(SoundArchive.Instance.exit, Camera.main.transform.position, 0.5f);
		else
			Debug.LogWarning("Null Sound: exit");

        FadeInstantiate fadeInstance = (FadeInstantiate)Instantiate(fade, new Vector3(29.36547f, -4.742213f, 8.186385f), transform.rotation);
        fadeInstance.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        fadeInstance.levelIndex = Scenes.MainMenuWithoutIntro;
    }
}
