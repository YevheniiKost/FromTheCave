using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField] private float _parallaxFactor;

    public void Move(float delta)
    {
        Vector3 newPos = transform.localPosition;
        newPos.x -= delta * _parallaxFactor;
        transform.localPosition = newPos;
    }
}
