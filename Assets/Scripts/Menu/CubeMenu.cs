using UnityEngine;
using System.Collections;

public class CubeMenu : MonoBehaviour {
	
	public float rotateSpeed;
	public GameObject mesh;

    public void WakeUp()
    {
        StartCoroutine(IWakeUp());
    }

	IEnumerator IWakeUp()
	{
		yield return new WaitForSeconds(2.1f);
		
		iTween.ScaleTo(mesh, iTween.Hash("scale", Vector3.one * 1.091071f,
										 "speed", 0.05f
										));
	}
}

/*
public class CubeMenu : MonoBehaviour
{

    public float rotateSpeed;
    public GameObject mesh;

    public FadeInstantiate FadeIn;

    int state; // 0:start; 1:level select; 2:options; 3:about; 4:exit;
    string[] labelText = { "novo jogo", "selecionar nivel", "audio", "sobre", "sair" };

    public MenuLabel label;

    public void WakeUp()
    {
        StartCoroutine(IWakeUp());
    }

    void OnMouseDown()
    {
        //switch (state)
        //{ 
        //    case 1: 
        //}
    }

    IEnumerator IWakeUp()
    {
        yield return new WaitForSeconds(2.1f);

        iTween.ScaleTo(mesh, iTween.Hash("scale", Vector3.one * 1.091071f,
                                         "speed", 0.05f
                                        ));
        label.ChangeString(labelText[state]);
    }

    public void TurnLeft()
    {
        if (!GetComponent<iTween>())
        {
            state = (state + 1) % 5;
            label.ChangeString(labelText[state]);
            iTween.RotateBy(gameObject, iTween.Hash("y", 0.5f,
                                                    "delay", 0.2f,
                                                    "speed", rotateSpeed,
                                                    "easetype", iTween.EaseType.easeInOutSine
                                                    ));
        }
    }

    public void TurnRight()
    {
        if (!GetComponent<iTween>())
        {
            state = (state + 5 - 1) % 5;
            label.ChangeString(labelText[state]);
            iTween.RotateBy(gameObject, iTween.Hash("y", -0.5f,
                                                    "delay", 0.2f,
                                                    "speed", rotateSpeed,
                                                    "easetype", iTween.EaseType.easeInOutSine
                                                    ));
        }
    }
}
*/