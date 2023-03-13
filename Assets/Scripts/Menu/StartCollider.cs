using UnityEngine;
using System.Collections;

public class StartCollider : MonoBehaviour {

    public float timeLocked;
    public float timeWaiting;
    float _time;
    bool unlocked = false;

    public TitleIntroAnimation titleIntroAnimation;
    public PressMessageAnimation pressMessageAnimation;
    public TitleTopAnimation titleTopAnimation;
    public CubeMenu cubeMenu;

    public MenuLabel newGame, levelSelect, credits, exit;

    void Start()
    {
        _time = 0f;
    }

    void OnMouseDown() 
    {
        if (timeLocked < _time && !unlocked)
        {
            AudioSource.PlayClipAtPoint(SoundArchive.Instance.pause, Camera.main.transform.position, 2);

            unlocked = true;
            titleIntroAnimation.WakeUp();
            pressMessageAnimation.WakeUp();
            titleTopAnimation.WakeUp();
            cubeMenu.WakeUp();

            newGame.WakeUp();
            levelSelect.WakeUp();
            credits.WakeUp();
            exit.WakeUp();

            Destroy(gameObject, timeWaiting);
        }
    }

    void Update()
    {
        _time += Time.deltaTime;
    }
}
