using UnityEngine;
using System.Collections;

public class StartColliderWithoutAnim : MonoBehaviour
{
    public float timeWaiting;
    public TitleTopAnimation titleTopAnimation;
    public CubeMenu cubeMenu;

    public MenuLabel newGame, levelSelect, credits, exit;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);

        titleTopAnimation.WakeUp();
        cubeMenu.WakeUp();

        newGame.WakeUp();
        levelSelect.WakeUp();
        credits.WakeUp();
        exit.WakeUp();

        Destroy(gameObject, timeWaiting);
    }
}
