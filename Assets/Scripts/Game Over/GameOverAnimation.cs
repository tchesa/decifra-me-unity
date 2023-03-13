using UnityEngine;
using System.Collections;

public class GameOverAnimation : MonoBehaviour {

    public LevelCubeGameOver[] LevelCubes;

    public MenuLabel[] labels;

    public float yPosition, distance;

    public float delay = 0.1f;

    public ContinueGameOverButton continuar;

    void Start()
    {
        ClassificationDataBase.Instance.ResetDataBase();
        StartCoroutine(IAnimation1());
    }

    IEnumerator IAnimation1()
    {
        foreach (LevelCubeGameOver cube in LevelCubes)
        {
            cube.transform.localScale = Vector3.zero;
        }

        for (int i = 0; i < LevelCubes.Length; i++)
        {
            iTween.RotateBy(LevelCubes[i].gameObject, iTween.Hash("x", 5,
                                                                 "time", 1,
                                                                 "easetype", iTween.EaseType.easeOutQuint));

            iTween.ScaleTo(LevelCubes[i].gameObject, iTween.Hash("scale", new Vector3(1, 1, 1) * 1.144142f,
                                                                 "time", 1,
                                                                 "easetype", iTween.EaseType.easeOutQuint));
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(IAnimation2());
    }

    IEnumerator IAnimation2()
    {
        for (int i = 0; i < LevelCubes.Length; i++)
        {
            Vector3 position = new Vector3(transform.position.x - (distance * i) + (distance * (LevelCubes.Length / 2)), transform.position.y + yPosition, transform.position.z);
            iTween.MoveTo(LevelCubes[i].gameObject, iTween.Hash("position", position,
                                                                "time", 0.5f,
                                                                "easetype", iTween.EaseType.easeOutQuint));

            iTween.ScaleTo(LevelCubes[i].gameObject, iTween.Hash("scale", new Vector3(1, 1, 1) * 0.6f,
                                                                 "time", 1,
                                                                 "easetype", iTween.EaseType.easeOutQuint));

            yield return new WaitForSeconds(delay);
        }

        //yield return new WaitForSeconds(1f);
        StartCoroutine(IAnimation3());
    }

    IEnumerator IAnimation3()
    {
        for (int i = 0; i < LevelCubes.Length; i++)
        {
            labels[i].WakeUp();

            iTween.MoveTo(LevelCubes[i].gameObject, iTween.Hash("position", LevelCubes[i].endPosition,
                                                                "time", 0.5f,
                                                                "easetype", iTween.EaseType.easeInQuint));

            iTween.ScaleTo(LevelCubes[i].gameObject, iTween.Hash("scale", Vector3.zero,
                                                                 "time", 0.5f,
                                                                 "delay", 0.5f,
                                                                 "easetype", iTween.EaseType.easeOutQuint));

            yield return new WaitForSeconds(0.1f);
        }

        continuar.WakeUp();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        for (int i = 0; i < LevelCubes.Length; i++)
        {
            Vector3 position = new Vector3(transform.position.x + (distance * i) - (distance * (LevelCubes.Length/2)), transform.position.y + yPosition, transform.position.z);
            Gizmos.DrawSphere(position, 0.1f);
        }
    }
}
