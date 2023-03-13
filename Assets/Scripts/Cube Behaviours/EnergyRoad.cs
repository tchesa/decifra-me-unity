using UnityEngine;
using System.Collections;

public class EnergyRoad : MonoBehaviour {

	void Update(){
		if (transform.parent.GetComponent<Block>().electrified){
	        GetComponent<Renderer>().material.color = Color.Lerp(GetComponent<Renderer>().material.color, Color.red, 10f * Time.deltaTime);
	    }else{
	        GetComponent<Renderer>().material.color = Color.Lerp(GetComponent<Renderer>().material.color, Color.grey, 10f * Time.deltaTime);
	    }
	}
}
