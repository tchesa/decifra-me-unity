using UnityEngine;
using System.Collections;

public class LevelToSelect : MonoBehaviour {

    public GameObject cube;
    public FadeInstantiate fade;

    public Vector3 position;
    public float distance;

    public GameObject onIconPrefab, offIconPrefab;
    public Transform indexText;

    public int level;

    public float time, nRotate;

    public Color mainColor;

    public Color targetColor;

    void Start()
    {
        targetColor = mainColor;
		
        if(level <= 9)
        {
            int classification = ClassificationDataBase.Instance.GetLevelClassification(level - 1);

            if (classification >= 0)
            {
                indexText.localPosition = new Vector3(indexText.localPosition.x, 0.23f, indexText.localPosition.z);
                for (int i = 0; i < 3; i++)
                {
                    GameObject obj;
                    if (classification >= i) obj = Instantiate(onIconPrefab, new Vector3(transform.position.x + position.x + distance - (distance * i), transform.position.y + position.y, transform.position.z), transform.rotation) as GameObject;
                    else obj = Instantiate(offIconPrefab, new Vector3(transform.position.x + position.x + distance - (distance * i), transform.position.y + position.y, transform.position.z), transform.rotation) as GameObject;

                    obj.transform.parent = transform;
                    obj.transform.localEulerAngles = new Vector3(0, 180, 45);
                    obj.transform.localScale = Vector3.one * 0.28f;
                }
            }
            else
            {
                indexText.localPosition = new Vector3(indexText.localPosition.x, 0, indexText.localPosition.z);
            }
        }

        transform.localScale = new Vector3(0, 0, 0);
    }

    void Update()
    {
        SkinnedMeshRenderer[] meshes = GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer mesh in meshes)
        {
            mesh.renderer.material.color = Color.Lerp(mesh.renderer.material.color, targetColor, Time.deltaTime * 5);
        }
    }

    void OnMouseDown()
    {
		targetColor = Color.red;
		
		if(SoundArchive.Instance.menuButton)
			AudioSource.PlayClipAtPoint(SoundArchive.Instance.menuButton, Camera.main.transform.position, 0.5f);
		else
			Debug.LogWarning("Null Sound: menuButton");

        if (level <= 9)
        {
            FadeInstantiate fadeInstance = (FadeInstantiate)Instantiate(fade, new Vector3(29.36547f, -4.742213f, 8.186385f), transform.rotation);
            fadeInstance.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);

            switch (level)
            {
                case 1: fadeInstance.levelIndex = Scenes.Game1; break;
                case 2: fadeInstance.levelIndex = Scenes.Game2; break;
                case 3: fadeInstance.levelIndex = Scenes.Game3; break;
                case 4: fadeInstance.levelIndex = Scenes.Game4; break;
                case 5: fadeInstance.levelIndex = Scenes.Game5; break;
                case 6: fadeInstance.levelIndex = Scenes.Game6; break;
                case 7: fadeInstance.levelIndex = Scenes.Game7; break;
                case 8: fadeInstance.levelIndex = Scenes.Game8; break;
                case 9: fadeInstance.levelIndex = Scenes.Game9; break;
                default: Destroy(fadeInstance.gameObject); break;
            }
        }
    }

    public void Show()
    {
        if (cube.GetComponent<iTween>()) Destroy(cube.GetComponent<iTween>());
        cube.transform.localEulerAngles = new Vector3(0, 0, 0);

        iTween.RotateBy(cube, iTween.Hash("x", nRotate,
                                                "time", time,
                                                "easetype", iTween.EaseType.easeOutQuint));

        iTween.ScaleTo(gameObject, iTween.Hash("scale", new Vector3(1, 1, 1),
                                               "time", time,
                                               "easetype", iTween.EaseType.easeOutQuint));
    }

    public void Hide()
    {
        if (cube.GetComponent<iTween>()) Destroy(cube.GetComponent<iTween>());
        cube.transform.localEulerAngles = new Vector3(0, 0, 0);

        iTween.RotateBy(cube, iTween.Hash("x", nRotate,
                                                "time", time,
                                                "easetype", iTween.EaseType.easeInQuint));

        iTween.ScaleTo(gameObject, iTween.Hash("scale", new Vector3(0, 0, 0),
                                               "time", time,
                                               "delay", time/1.5,
                                               "easetype", iTween.EaseType.easeOutQuint));
    }
    /*
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int i = 0; i < 3; i++)
        {
            Vector3 sphereposition = new Vector3(transform.position.x + position.x - distance + (distance * i), transform.position.y + position.y, transform.position.z);
            Gizmos.DrawSphere(sphereposition, 0.1f);
        }
    }*/
}
