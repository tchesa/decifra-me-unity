using UnityEngine;
using System.Collections;

public class TestAnimation1 : MonoBehaviour {

    public AnimatedMesh center, emitter;
	
	public Vector3 inSmallScale = new Vector3(1f, 0.129f, 0.129f);
	public Vector3 outSmallScale = new Vector3(1f, 0.22f, 0.22f);
	
    public float time = 1f;
	
	Vector3 inGroupScaleStart, outGroupScaleStart;
    void Start()
    {
		inGroupScaleStart = center.inGroup.transform.localScale;
		outGroupScaleStart = center.outGroup.transform.localScale;
		
        BeginAnimation();
    }
	
    void BeginAnimation()
    {
		// outGroup
		iTween.ScaleTo(center.outGroup, iTween.Hash("scale", outSmallScale,
                                                        "time", time,
                                                        "easeType", iTween.EaseType.easeOutQuad,
                                                        "oncomplete", "BackAnimation",
                                                        "oncompletetarget", this.gameObject,
                                                        "delay", 0f));
		// inGroup
		iTween.ScaleTo(center.inGroup, iTween.Hash("scale", inSmallScale,
                                                        "time", time,
                                                        "easeType", iTween.EaseType.easeOutQuad));
		
		emitter.inGroup.transform.localScale = center.inGroup.transform.localScale;
		emitter.outGroup.transform.localScale = center.outGroup.transform.localScale;
		
		iTween.ScaleTo(emitter.inGroup, iTween.Hash("scale", Vector3.one,
                                                        "time", time * 2,
                                                        "easeType", iTween.EaseType.easeOutQuad));
		
        iTween.ScaleTo(emitter.outGroup, iTween.Hash("scale", Vector3.one,
                                                        "time", time * 2,
                                                        "easeType", iTween.EaseType.easeOutQuad));
    }

    void BackAnimation()
    {
		iTween.ScaleTo(center.outGroup, iTween.Hash("scale", outGroupScaleStart,
                                                        "time", time,
                                                        "easeType", iTween.EaseType.easeInQuad,
                                                        "oncomplete", "BeginAnimation",
                                                        "oncompletetarget", this.gameObject));
		
        iTween.ScaleTo(center.inGroup, iTween.Hash("scale", inGroupScaleStart,
                                                        "time", time,
                                                        "easeType", iTween.EaseType.easeInQuad));
    }
}
