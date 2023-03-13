using UnityEngine;
using System.Collections;

public class TransictionButton : MonoBehaviour {

    public GameObject fade;

    public Vector3 instantiatePosition = new Vector3(29.36547f, -4.742213f, 8.186385f);

    public float delay;

    IEnumerator OnMouseDown()
    {
        yield return new WaitForSeconds(delay);
        Instantiate(fade, instantiatePosition, transform.rotation);
    }
}