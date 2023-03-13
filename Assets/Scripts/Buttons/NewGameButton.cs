using UnityEngine;
using System.Collections;

public class NewGameButton : MonoBehaviour {

    public FadeInstantiate fade;

    void OnMouseDown()
    {
		ClassificationDataBase.Instance.ResetDataBase();
		
        FadeInstantiate fadeInstance = (FadeInstantiate)Instantiate(fade, new Vector3(29.36547f, -4.742213f, 8.186385f), transform.rotation);
        fadeInstance.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        fadeInstance.levelIndex = Scenes.Game1;
    }
}
