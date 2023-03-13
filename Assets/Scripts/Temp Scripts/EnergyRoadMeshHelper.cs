using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class EnergyRoadMeshHelper : MonoBehaviour {

	public Mesh curve;
	public Mesh straight;
	public Mesh cross;
    public Mesh t;
	
	void Update(){
		if(transform.parent.GetComponent<Block>().GetType() == typeof(UnlockableMovementBlock) && !transform.parent.GetComponent<UnlockableMovementBlock>().unblocked){
			GetComponent<MeshFilter>().mesh = null;
		}else{
			if(transform.parent.GetComponent<Block>().portDivision == Block.PortDivision.TOP_RIGHT){
				GetComponent<MeshFilter>().mesh = curve;
				transform.localEulerAngles = new Vector3(0f,0f,0f);
			}else if(transform.parent.GetComponent<Block>().portDivision == Block.PortDivision.RIGHT_BOT){
				GetComponent<MeshFilter>().mesh = curve;
				transform.localEulerAngles = new Vector3(0f,0f,90f);
			}else if(transform.parent.GetComponent<Block>().portDivision == Block.PortDivision.BOT_LEFT){
				GetComponent<MeshFilter>().mesh = curve;
				transform.localEulerAngles = new Vector3(0f,0f,180f);
			}else if(transform.parent.GetComponent<Block>().portDivision == Block.PortDivision.LEFT_TOP){
				GetComponent<MeshFilter>().mesh = curve;
				transform.localEulerAngles = new Vector3(0f,0f,270f);
			}else if(transform.parent.GetComponent<Block>().portDivision == Block.PortDivision.LEFT_RIGHT){
				GetComponent<MeshFilter>().mesh = straight;
				transform.localEulerAngles = new Vector3(0f,0f,90f);
            }
            else if (transform.parent.GetComponent<Block>().portDivision == Block.PortDivision.TOP_BOT){
                GetComponent<MeshFilter>().mesh = straight;
                transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (transform.parent.GetComponent<Block>().portDivision == Block.PortDivision.BOT_LEFT_TOP)
            {
                GetComponent<MeshFilter>().mesh = t;
                transform.localEulerAngles = new Vector3(0f, 0f, 180f);
            }
            else if (transform.parent.GetComponent<Block>().portDivision == Block.PortDivision.LEFT_TOP_RIGHT)
            {
                GetComponent<MeshFilter>().mesh = t;
                transform.localEulerAngles = new Vector3(0f, 0f, 270f);
            }
            else if (transform.parent.GetComponent<Block>().portDivision == Block.PortDivision.TOP_RIGHT_BOT)
            {
                GetComponent<MeshFilter>().mesh = t;
                transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (transform.parent.GetComponent<Block>().portDivision == Block.PortDivision.RIGHT_BOT_LEFT)
            {
                GetComponent<MeshFilter>().mesh = t;
                transform.localEulerAngles = new Vector3(0f, 0f, 90f);
            }
            else if (transform.parent.GetComponent<Block>().portDivision == Block.PortDivision.TOP_BOT_LEFT_RIGHT)
            {
                GetComponent<MeshFilter>().mesh = cross;
            }
            else
            {
                GetComponent<MeshFilter>().mesh = null;
            }
		}
	}
}
