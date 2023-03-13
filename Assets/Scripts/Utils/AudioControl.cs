using UnityEngine;
using System.Collections;

public class AudioControl : MonoBehaviour {

    private static AudioControl instance = null;
    public static AudioControl Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (AudioControl)FindObjectOfType(typeof(AudioControl));
                if (instance == null)
                    instance = (new GameObject("_AudioControl")).AddComponent<AudioControl>();
            }
            return instance;
        }
    }

    public bool audio = true;
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    void Update()
    {
        if (audio) AudioListener.volume = 1;
        else AudioListener.volume = 0;
    }
}
