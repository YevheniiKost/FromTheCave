using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GamePersist : MonoBehaviour
{
    private IEnumerable<ISaveState> _savableObjects;

    private void Awake()
    {
        GameEvents.OnSaveGame += Save;
        GameEvents.OnLoadGame += Load;
    }

    private void OnDestroy()
    {
        GameEvents.OnSaveGame -= Save;
        GameEvents.OnLoadGame -= Load;
    }

    private void Load()
    {
        foreach (ISaveState persist in _savableObjects)
        {
            persist.Load();
        }
    }

    private void Save()
    {
        _savableObjects = FindObjectsOfType<MonoBehaviour>().OfType<ISaveState>();
        foreach (var persist in _savableObjects)
        {
            persist.Save();
        }
    }
}

internal interface ISaveState
{
    void Save();
    void Load();
}
