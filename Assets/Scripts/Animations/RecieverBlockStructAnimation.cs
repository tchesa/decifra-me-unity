using UnityEngine;
using System.Collections;

public class RecieverBlockStructAnimation : MonoBehaviour {

    public AnimatedMesh center;
    public GameObject prefab;
	public Color emitterColor;
	
	public Vector3 inSmallScale = new Vector3(1f, 0.129f, 0.129f);
	public Vector3 outSmallScale = new Vector3(1f, 0.22f, 0.22f);

    public Vector3 inEmitterScale, outEmitterScale;
	
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
        if (transform.parent.GetComponent<Block>().electrified)
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

            GameObject obj = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
            obj.transform.localScale = transform.localScale;

            AnimatedMesh emitter = obj.GetComponent<AnimatedMesh>();
            emitter.transform.parent = transform;
            emitter.mesh.GetComponent<Renderer>().material.color = Color.red;

            emitter.inGroup.transform.localScale = center.inGroup.transform.localScale;
            emitter.outGroup.transform.localScale = center.outGroup.transform.localScale;
            emitter.mesh.GetComponent<Renderer>().material.color = emitterColor;

            iTween.ScaleTo(emitter.outGroup, iTween.Hash("scale", outEmitterScale,
                                                            "time", time * 2 - 0.2f,
                                                            "easeType", iTween.EaseType.easeOutQuad));

            iTween.ScaleTo(emitter.inGroup, iTween.Hash("scale", inEmitterScale,
                                                            "time", time * 2,
                                                            "easeType", iTween.EaseType.easeOutQuad,
                                                            "delay", 0.2f,
                                                            "oncomplete", "DestroyOnComplete",
                                                            "oncompletetarget", gameObject,
                                                            "oncompleteparams", emitter.gameObject));
        }
        else
        {
            // outGroup
            iTween.ScaleTo(center.outGroup, iTween.Hash("scale", outSmallScale,
                                                            "time", time*3,
                                                            "easeType", iTween.EaseType.easeOutQuad,
                                                            "oncomplete", "BackAnimation",
                                                            "oncompletetarget", this.gameObject,
                                                            "delay", 0f));
            // inGroup
            iTween.ScaleTo(center.inGroup, iTween.Hash("scale", inSmallScale,
                                                            "time", time*3,
                                                            "easeType", iTween.EaseType.easeOutQuad));
        }
    }

    void BackAnimation()
    {
        if (transform.parent.GetComponent<Block>().electrified)
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
        else
        {
            iTween.ScaleTo(center.outGroup, iTween.Hash("scale", outGroupScaleStart,
                                                            "time", time*3,
                                                            "easeType", iTween.EaseType.easeInQuad,
                                                            "oncomplete", "BeginAnimation",
                                                            "oncompletetarget", this.gameObject));

            iTween.ScaleTo(center.inGroup, iTween.Hash("scale", inGroupScaleStart,
                                                            "time", time*3,
                                                            "easeType", iTween.EaseType.easeInQuad));
        }
    }

    void DestroyOnComplete(Object obj)
    {
        Destroy(obj);
    }
}
