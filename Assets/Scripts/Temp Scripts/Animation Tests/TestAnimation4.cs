using UnityEngine;
using System.Collections;

public class TestAnimation4 : MonoBehaviour {
	
	public enum State { ON, OFF };
	public State state = State.OFF;
	
    public AnimatedMesh center;
	
    public float onSpeed, offSpeed, transSpeed;
	float speed;
	
    void Update()
    {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(state == State.ON) state = State.OFF;
			else state = State.ON;
		}
		
        switch(state)
		{
			case State.ON :
			{
				speed = Mathf.MoveTowards(speed, onSpeed, Time.deltaTime * transSpeed);
				center.transform.Rotate(Vector3.up * speed * Time.deltaTime);
				center.inGroup.transform.localScale = Vector3.MoveTowards(center.inGroup.transform.localScale, Vector3.one * 0.42f, Time.deltaTime/50);
				center.outGroup.transform.localScale = Vector3.MoveTowards(center.outGroup.transform.localScale, Vector3.one * 0.57f, Time.deltaTime/50);
				break;
			}
			case State.OFF :
			{
				speed = Mathf.MoveTowards(speed, offSpeed, Time.deltaTime * transSpeed);
				center.transform.Rotate(Vector3.up * speed * Time.deltaTime);
				center.inGroup.transform.localScale = Vector3.MoveTowards(center.inGroup.transform.localScale, Vector3.one * 0.49f, Time.deltaTime/50);
				center.outGroup.transform.localScale = Vector3.MoveTowards(center.outGroup.transform.localScale, Vector3.one * 0.51f, Time.deltaTime/50);
				break;
			}
			default : break;
		}			
    }
}
