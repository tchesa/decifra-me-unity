using UnityEngine;
using System.Collections;

public class EnergyRoad : MonoBehaviour {

	void Update(){
		if (transform.parent.GetComponent<Block>().electrified){
	        renderer.material.color = Color.Lerp(renderer.material.color, Color.red, 10f * Time.deltaTime);
	    }else{
	        renderer.material.color = Color.Lerp(renderer.material.color, Color.grey, 10f * Time.deltaTime);
	    }
	}
}
