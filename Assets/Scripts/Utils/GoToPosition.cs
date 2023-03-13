using UnityEngine;
using System.Collections;

public class GoToPosition : MonoBehaviour {

    public float delay;

    public Vector3 localPosition;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(delay);

        transform.localPosition = localPosition;

        Destroy(this);
    }
}
