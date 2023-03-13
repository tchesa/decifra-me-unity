using UnityEngine;
using System.Collections;

public class EndStripeAnimation : MonoBehaviour
{

    public float scale, time, inDelay;

    public void WakeUp()
    {
        iTween.ScaleTo(gameObject, iTween.Hash("y", scale,
                                               "time", time,
                                               "delay", inDelay,
                                               "easeType", iTween.EaseType.easeOutCubic));
        Destroy(this);
    }
}
