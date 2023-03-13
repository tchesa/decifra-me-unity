using UnityEngine;
using System.Collections;

public class MainMenuLevelSelectInit : MonoBehaviour {

    public GameObject collider, panelMesh;

    public LevelSelectGroup levelSelectGroup;
    
    public MenuLabel newGame, levelSelect, credits, exit;

    public TitleTopAnimation title;

    IEnumerator Start()
    {
        panelMesh.transform.localScale = Vector3.zero;

        yield return new WaitForSeconds(0.5f);

        collider.collider.enabled = true;

        levelSelectGroup.Show();

        newGame.WakeUp();
        levelSelect.WakeUp();
        credits.WakeUp();
        exit.WakeUp();
        title.WakeUp();
    }
}
