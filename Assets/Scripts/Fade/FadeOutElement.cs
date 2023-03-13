using UnityEngine;
using System.Collections;

public class FadeOutElement : MonoBehaviour {

	public GameObject inMesh;
	
	public float time;
	
	public void Activate()
	{
		iTween.ScaleTo(inMesh, iTween.Hash("scale", Vector3.one * 1.14f,
                                                        "time", time,
                                                        "easeType", iTween.EaseType.easeOutQuad,
                                                        "oncompletetarget", this.gameObject,
                                                        "oncomplete", "EndAnimation"));
	}

    void EndAnimation()
    {
        Destroy(this.gameObject);
    }
}
 