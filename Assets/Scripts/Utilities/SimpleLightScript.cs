using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SimpleLightScript : MonoBehaviour
{
    void Start()
    {
        GetComponent<Light2D>().intensity = .5f;
    }
}
