using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXAudioSource : MonoBehaviour
{
    public static SFXAudioSource Instance;
    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (Instance != null)
        {
            Destroy(this.gameObject);
        }

        Instance = this;
    }

}
