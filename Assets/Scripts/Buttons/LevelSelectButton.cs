using UnityEngine;
using System.Collections;

public class LevelSelectButton : MonoBehaviour {

    public GameObject mesh;

    public GameObject collider;

    public LevelSelectGroup levelSelectGroup;

    public TitleTopAnimation title;

    IEnumerator OnMouseDown()
    {
        if(SoundArchive.Instance.menuButton) 
			AudioSource.PlayClipAtPoint(SoundArchive.Instance.menuButton, Camera.main.transform.position, 0.5f);
		else
			Debug.LogWarning("Null Sound: menuButton");

        collider.GetComponent<Collider>().enabled = true;

        iTween.ScaleTo(mesh, iTween.Hash("scale", Vector3.zero,
                                         "time", 1));

        yield return new WaitForSeconds(0.5f);

        levelSelectGroup.Show();

        title.ChangeName("selecionar nivel");
    }
}
