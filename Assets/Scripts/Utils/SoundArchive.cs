using UnityEngine;
using System.Collections;

public class SoundArchive : MonoBehaviour {

    #region Singleton Design Pattern
    private static SoundArchive instance = null;
    public static SoundArchive Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (SoundArchive)FindObjectOfType(typeof(SoundArchive));
                if (instance == null)
                    instance = (new GameObject("_SoundArchive")).AddComponent<SoundArchive>();
            }
            return instance;
        }
    }
    #endregion

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public AudioClip[] blockEffects;

    public AudioClip menuButton, pause, restart, exit;

    public AudioClip GetRandomEffect()
    {
		if(blockEffects == null || blockEffects.Length == 0)
		{
			Debug.LogWarning("blockEffects Empty");
			return null;
		}
        return blockEffects[(int)(Random.value * blockEffects.Length)];
    }
}
