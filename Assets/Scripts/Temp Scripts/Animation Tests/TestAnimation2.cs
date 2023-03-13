using UnityEngine;
using System.Collections;

public class TestAnimation2 : MonoBehaviour {

    public AnimatedMesh center;

    public float rotationFactor = 0.5f;
	
    public float time = 1f;
	
    void Start()
    {
        iTween.RotateBy(center.gameObject, iTween.Hash("y", rotationFactor,
                                                        "time", time,
                                                        "easeType", iTween.EaseType.easeInOutQuad,
                                                        "oncomplete", "BackAnimation",
                                                        "oncompletetarget", this.gameObject,
                                                        "loopType", "loop",
                                                        "delay", 0f));

        iTween.ScaleTo(center.inGroup, iTween.Hash("scale", new Vector3(1f, 0f, 0f),
                                                        "time", time/2,
                                                        "easeType", iTween.EaseType.easeInOutQuad,
                                                        "loopType", "pingpong",
                                                        "delay", 0f));

        iTween.ScaleTo(center.outGroup, iTween.Hash("scale", new Vector3(1f, 0.4f, 0.4f),
                                                        "time", time / 2,
                                                        "easeType", iTween.EaseType.easeInOutQuad,
                                                        "loopType", "pingpong",
                                                        "delay", 0f));
    }
	
    void BeginAnimation()
    {
        iTween.RotateBy(center.gameObject, iTween.Hash("y", .25, "easeType", "easeInBack", "loopType", "pingPong", "delay", 0f));
        /*
		*/
	}
}
