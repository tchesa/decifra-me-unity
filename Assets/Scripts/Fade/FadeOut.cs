using UnityEngine;
using System.Collections;

public class FadeOut : MonoBehaviour {

    public GameObject elementPrefab;

    public float delay, time, elementSize;

    public int gridSize;

    FadeOutElement[,] elements;

    IEnumerator Start()
    {
        elements = new FadeOutElement[gridSize, gridSize];

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                GameObject obj = Instantiate(elementPrefab, transform.position, transform.rotation) as GameObject;
                obj.transform.parent = transform;
                obj.transform.localScale = Vector3.one * elementSize;
                obj.transform.Translate(new Vector3(i, j, 0) * elementSize, Space.Self);
                obj.transform.Rotate(new Vector3(270, 0, 0));
                obj.GetComponent<FadeOutElement>().time = time;
                elements[i, j] = obj.GetComponent<FadeOutElement>();
            }
        }

        for (int i = 1; i < (gridSize * 2); i++)
        {
            for (int j = (int)Mathf.Clamp(i - gridSize, 0, i); j < i - (int)Mathf.Clamp(i - gridSize, 0, gridSize - 1); j++)
            {
                elements[i - 1 - j, j].Activate();
            }
            yield return new WaitForSeconds(delay);
        }
    }

    void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.one * (gridSize / 2) * elementSize + new Vector3(0, 0, -1) * (gridSize / 2) * elementSize + new Vector3(-elementSize/2, -elementSize/2, 0), new Vector3(1, 1, 0) * gridSize * elementSize);
    }
}