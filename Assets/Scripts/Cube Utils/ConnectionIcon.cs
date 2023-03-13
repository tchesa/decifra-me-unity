using UnityEngine;
using System.Collections;

public class ConnectionIcon : MonoBehaviour {

	public enum IconState { OFF, ON, HIDDEN };
	public IconState iconState = IconState.HIDDEN;
    public Transform inMesh, outMesh;
    public SkinnedMeshRenderer meshRenderer;
	float realScale;
	
	void Start()
	{
		realScale = transform.localScale.x;
	}
	
	void Update()
	{			
		switch(iconState)
		{
			case IconState.HIDDEN :
			{
                meshRenderer.material.color = Color.Lerp(meshRenderer.material.color, Color.black, 10f * Time.deltaTime);
				transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 10f * Time.deltaTime);
				break;
			}
			case IconState.ON :
			{
                meshRenderer.material.color = Color.Lerp(meshRenderer.material.color, Color.red, 10f * Time.deltaTime);
				transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * realScale, 10f * Time.deltaTime);
                inMesh.localScale = Vector3.Lerp(inMesh.localScale, new Vector3(1, 0, 0), 10f * Time.deltaTime);
				break;
			}
			case IconState.OFF :
			{
                meshRenderer.material.color = Color.Lerp(meshRenderer.material.color, Color.grey, 10f * Time.deltaTime);
				transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * realScale, 10f * Time.deltaTime);
                inMesh.localScale = Vector3.Lerp(inMesh.localScale, Vector3.one * 0.7f, 10f * Time.deltaTime);
				break;
			}
			default : break;
		}
	}
}
