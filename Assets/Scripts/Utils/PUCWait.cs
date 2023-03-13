using UnityEngine;
using System.Collections;

public class PUCWait : MonoBehaviour {

    public float waiting;

    public FadeInstantiate fade;

    float time;

    void Start()
    {
        time = 0f;
    }

    void Update()
    {
        if (time > waiting)
        {
            FadeInstantiate fadeInstance = (FadeInstantiate)Instantiate(fade, new Vector3(29.36547f, -4.742213f, 8.186385f), transform.rotation);
            fadeInstance.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);

            fadeInstance.levelIndex = Scenes.MainMenu;

            Destroy(this);
        }

        time += Time.deltaTime;
    }
}
