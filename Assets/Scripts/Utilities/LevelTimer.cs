using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public float TimeFromLevelStart;

    private void Start()
    {
        TimeFromLevelStart = 0;
    }

    private void Update()
    {
        TimeFromLevelStart += Time.deltaTime;
    }
}
