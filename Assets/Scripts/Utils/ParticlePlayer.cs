﻿using UnityEngine;
using System.Collections;

public class ParticlePlayer : MonoBehaviour {

    void Update()
    {
        if (Time.time > Constants.EndAnimation)
        {
            GetComponent<ParticleSystem>().Play();
            Destroy(this);
        }
    }
}
