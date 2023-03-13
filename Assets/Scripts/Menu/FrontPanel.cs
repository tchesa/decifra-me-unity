using UnityEngine;
using System.Collections;

public class FrontPanel : MonoBehaviour {

	public bool on;
	bool _on;
	
	public GameObject collider, plane, button;
	
	void Update()
	{
		if(on!=_on)
		{
			if(on)
			{
				StartCoroutine(IShow());
			}
			else
			{
				StartCoroutine(IHide());
			}
		}
		_on = on;
	}
	
	IEnumerator IShow()
	{
		yield return new WaitForSeconds(0);
		collider.GetComponent<Collider>().enabled = true;
		iTween.ScaleTo(plane, iTween.Hash("z", 0.5389027f
										  ));
	}
	
	IEnumerator IHide()
	{
		yield return new WaitForSeconds(0);
		collider.GetComponent<Collider>().enabled = false;
		iTween.ScaleTo(plane, iTween.Hash("z", 0
										  ));
	}
}
