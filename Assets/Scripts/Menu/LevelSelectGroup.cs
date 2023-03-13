using UnityEngine;
using System.Collections;

public class LevelSelectGroup : MonoBehaviour {

    public LevelToSelect[] cubes;
    public float delay;

    public void Show()
    {
        StartCoroutine(IShow());
    }

    IEnumerator IShow()
    {
        foreach (LevelToSelect cube in cubes)
        {
            cube.Show();
            yield return new WaitForSeconds(delay);
        }
    }

    public void Hide()
    {
        StartCoroutine(IHide());
    }

    IEnumerator IHide()
    {
        foreach (LevelToSelect cube in cubes)
        {
            cube.Hide();
            yield return new WaitForSeconds(delay);
        }
    }
}
