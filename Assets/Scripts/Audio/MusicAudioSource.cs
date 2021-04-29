using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAudioSource : MonoBehaviour
{
    public static MusicAudioSource Instance;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (Instance != null)
            Destroy(this.gameObject);
        else
            Instance = this;
    }
}
