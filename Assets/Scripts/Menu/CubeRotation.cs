using UnityEngine;
using System.Collections;

public class CubeRotation : MonoBehaviour {

	public GameObject cube;
	public float time;
	public int nRotation;
	
	void Start()
	{
        iTween.RotateTo(cube, iTween.Hash("y", 180f * nRotation * 2, 
                                          //"x", 180f * nRotation * 2, 
                                          "z", 180f * nRotation * 2,
										  "time", time,
										  "easetype", iTween.EaseType.easeInOutQuad
											));
	}
}
