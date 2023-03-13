using UnityEngine;
using System.Collections;

public class ParticlePlayer : MonoBehaviour {

    void Update()
    {
        if (Time.time > Constants.EndAnimation)
        {
            particleSystem.Play();
            Destroy(this);
        }
    }
}
