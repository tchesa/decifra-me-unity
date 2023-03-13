using UnityEngine;
using System.Collections;

public class continueCredits : MonoBehaviour {

    public CreditsInit init;

    void OnMouseDown()
    {
        Destroy(collider);
        init.Exit();
    }
}
