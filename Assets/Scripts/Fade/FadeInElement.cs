using UnityEngine;
using System.Collections;

public class FadeInElement : MonoBehaviour {

	public GameObject inMesh;
	
	public float time;
	
	void Start()
	{
		iTween.ScaleTo(inMesh, iTween.Hash("scale", new Vector3(1, 0, 0),
                                                        "time", time,
                                                        "easeType", iTween.EaseType.easeOutQuad));
	}
}
 