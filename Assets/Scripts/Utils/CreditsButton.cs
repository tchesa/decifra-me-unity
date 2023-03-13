using UnityEngine;
using System.Collections;

public class CreditsButton : MonoBehaviour {

    public FadeInstantiate fade;

    void OnMouseDown()
    {
        Destroy(GetComponent<Collider>());

        AudioSource.PlayClipAtPoint(SoundArchive.Instance.menuButton, Camera.main.transform.position, 0.5f);

        FadeInstantiate fadeInstance = (FadeInstantiate)Instantiate(fade, new Vector3(29.36547f, -4.742213f, 8.186385f), transform.rotation);
        fadeInstance.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);

        fadeInstance.levelIndex = Scenes.Credits;
    }
}
