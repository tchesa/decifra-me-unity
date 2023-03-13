using UnityEngine;
using System.Collections;

public class TextInstantiate : MonoBehaviour {

    public float delay, range;

    public GameObject prefab;

    IEnumerator Start()
    {
        while (true)
        {
            GameObject go = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
            go.transform.Translate(new Vector3((Random.value * range) - (range / 2), (Random.value * range) - (range / 2), 0));

            yield return new WaitForSeconds(delay);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(range, range, 0));
    }
}
