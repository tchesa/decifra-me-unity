using UnityEngine;
using System.Collections;

public class LightIntroAnimation : MonoBehaviour {

    public float delay = 8.5f;
    public float idle = 0.5f;
    public float time = 1.8f;

    float _time;

    bool sound = false;

    void Start() 
    {
        _time = 0f;
    }

    void Update()
    {
        Light light = GetComponent<Light>();

        if (_time > delay)
        {
            if (_time < delay + time)
            {
                light.intensity += Time.deltaTime / time;
                if (!sound)
                {
					if(SoundArchive.Instance.menuButton)
						AudioSource.PlayClipAtPoint(SoundArchive.Instance.menuButton, Camera.main.transform.position);
						else
							Debug.LogWarning("Null Sound: menuButton");
				
                    sound = true;
                }
            }
            else if (_time > delay + time + idle + time)
            {
                Destroy(this);
            }
            else if (_time > delay + time + idle)
            {
                light.intensity -= Time.deltaTime / time;
            }
        }

        if (light.intensity < 0.5f) light.intensity = 0.5f;
        
        _time += Time.deltaTime;
    }
}
