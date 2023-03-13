using UnityEngine;
using System.Collections;

public class LevelSelectBackButton : MonoBehaviour 
{
    public GameObject mesh;

    public GameObject collider;

    public LevelSelectGroup levelSelectGroup;

    bool click = false;

    public TitleTopAnimation title;

    IEnumerator OnMouseDown()
    {
        if (!click)
        {
            click = true;

            levelSelectGroup.Hide();

            yield return new WaitForSeconds(1.5f);

            iTween.ScaleTo(mesh, iTween.Hash("scale", Vector3.one * 1.091071f,
                                         "time", 1));

            collider.GetComponent<Collider>().enabled = false;

            click = false;
			
			GetComponent<LevelToSelect>().targetColor = GetComponent<LevelToSelect>().mainColor;

            title.ChangeName("decifra.me");
        }
    }
}
