using UnityEngine;
using System.Collections;

public class IntroStripeAnimation : MonoBehaviour {

    public float scale, time, inDelay, outDelay;

    void Start()
    {
        iTween.ScaleTo(gameObject, iTween.Hash("y", scale,
                                               "time", time,
                                               "delay", inDelay,
                                               "easeType", iTween.EaseType.easeOutCubic));

        iTween.ScaleTo(gameObject, iTween.Hash("y", 0,
                                               "time", time,
                                               "delay", inDelay + outDelay,
                                               "easeType", iTween.EaseType.easeInCubic,
                                               "oncomplete", "End",
                                               "oncompletetarget", gameObject));
    }

    void End()
    {
        Destroy(gameObject);
    }
}
