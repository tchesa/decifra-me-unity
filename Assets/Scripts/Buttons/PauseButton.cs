using UnityEngine;
using System.Collections;

public class PauseButton : MonoBehaviour
{
    public GameObject border, icon;

    public GameObject panel, stripe, panelCollider;

    Color targetColor = Color.gray;

    public MenuLabel title, mainMenu, sound, restart;

    float locked = 0f;

    void Update()
    {
        if(locked > 0) locked -= Time.deltaTime;

        //if (!general.paused)
        if(!General.Instance.paused)
        {
            border.GetComponent<Renderer>().material.color = Color.Lerp(border.GetComponent<Renderer>().material.color, targetColor, Time.deltaTime * 5);
            icon.GetComponent<Renderer>().material.color = Color.Lerp(icon.GetComponent<Renderer>().material.color, targetColor, Time.deltaTime * 5);
        }
        else
        {
            border.GetComponent<Renderer>().material.color = Color.Lerp(border.GetComponent<Renderer>().material.color, Color.red, Time.deltaTime * 5);
            icon.GetComponent<Renderer>().material.color = Color.Lerp(icon.GetComponent<Renderer>().material.color, Color.red, Time.deltaTime * 5);
        }
    }

    void OnMouseEnter()
    {
        targetColor = Color.red;
    }

    void OnMouseExit()
    {
        targetColor = Color.gray;
    }

    void OnMouseDown()
    {
        if (locked <= 0)
        {
			if(SoundArchive.Instance.pause)
				AudioSource.PlayClipAtPoint(SoundArchive.Instance.pause, Camera.main.transform.position, 2f);
			else
				Debug.LogWarning("Null Sound: pause");

            locked = Constants.UnpauseDelay;
            if (!General.Instance.paused)
            {
                iTween.ScaleTo(panel, iTween.Hash("scale", new Vector3(1, 0, 0), "easetype", iTween.EaseType.easeOutCirc, "time", 0.5f));
                iTween.ScaleTo(stripe, iTween.Hash("y", 0.9f, "easetype", iTween.EaseType.easeOutCirc, "time", 0.5f));
                title.WakeUp();
                mainMenu.WakeUp();
                sound.WakeUp();
                restart.WakeUp();
                General.Instance.Pause();
                panelCollider.GetComponent<Collider>().enabled = true;
            }
            else
            {
                iTween.ScaleTo(panel, iTween.Hash("scale", Vector3.one * 2.6f, "easetype", iTween.EaseType.easeOutCirc, "time", 0.5f, "delay", 1.4f));
                iTween.ScaleTo(stripe, iTween.Hash("y", 0, "easetype", iTween.EaseType.easeOutCirc, "time", 0.5f, "delay", 1.4f));
                title.Sleep();
                mainMenu.Sleep();
                sound.Sleep();
                restart.Sleep();
                General.Instance.Unpause();
                panelCollider.GetComponent<Collider>().enabled = false;
            }
        }
    }
}
