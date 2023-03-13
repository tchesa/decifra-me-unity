using UnityEngine;
using System.Collections;

public class FadeInstantiate : MonoBehaviour {

	public GameObject elementPrefab;
	
	public float delay, time, elementSize;

    public int gridSize;

    public int levelIndex;
	
	IEnumerator Start()
	{
        for (int i = 1; i < (gridSize * 2); i++)
        {
            //for (int j = Clamp(i - gridSize, 0, i); j < i - Clamp(i - gridSize, 0, gridSize - 1); j++)
            for (int j = (int)Mathf.Clamp(i-gridSize, 0, i); j < i - (int)Mathf.Clamp(i - gridSize, 0, gridSize - 1); j++)
            {
                //Console.Write("[" + matrix[i - 1 - j, j] + "]");
                GameObject obj = Instantiate(elementPrefab, transform.position, transform.rotation) as GameObject;
                obj.transform.parent = transform;
                obj.transform.localScale = Vector3.one * elementSize;
                obj.transform.Translate(new Vector3(i - 1 - j, j, 0) * elementSize, Space.Self);
                obj.transform.Rotate(new Vector3(270, 0, 0));
                obj.GetComponent<FadeInElement>().time = time;
            }
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(0.5f);

        Application.LoadLevel(levelIndex);
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + Vector3.one * (gridSize / 2) * elementSize + new Vector3(0, 0, -1) * (gridSize / 2) * elementSize + new Vector3(-elementSize/2, -elementSize/2, 0), new Vector3(1, 1, 0) * gridSize * elementSize);
    }
}
