using UnityEngine;
using System.Collections;

public class LightEndAnimation : MonoBehaviour {

    public float delay = 8.5f;
    public float time = 1.8f;
    public TextMesh[] texts;

    float _time;
    bool wakeUp = false;

    public void WakeUp() 
    {
        if (!wakeUp)
        {
            _time = 0f;
            wakeUp = true;
        } 
    }

    void Update()
    {

        if (wakeUp)
        {
            Light light = GetComponent<Light>();

            if (_time > delay)
            {
                if (_time < delay + time)
                {
                    light.intensity += Time.deltaTime / time;

                    foreach (TextMesh text in texts) text.GetComponent<Renderer>().material.color = new Color(light.intensity, light.intensity, light.intensity, 1);
                }
                else
                {
                    Destroy(this);
                }
            }

            if (light.intensity < 0.5f) light.intensity = 0.5f;

            _time += Time.deltaTime;
        }
    }
}
