using System.Linq;
using UnityEngine;

public class GamePersist : MonoBehaviour
{
    //private void OnDisable() => Save();

   // private void Start() => Load();

    private void Awake()
    {
        EventAggregator.OnSaveGame += Save;
        EventAggregator.OnLoadGame += Load;
    }

    private void OnDestroy()
    {
        EventAggregator.OnSaveGame -= Save;
        EventAggregator.OnLoadGame -= Load;
    }

    [ContextMenu("Load")]
    private void Load()
    {
        foreach (ISaveState persist in FindObjectsOfType<MonoBehaviour>().OfType<ISaveState>())
        {
            persist.Load();
        }
    }
    [ContextMenu("Save")]
    private void Save()
    {
        foreach (var persist in FindObjectsOfType<MonoBehaviour>().OfType<ISaveState>())
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
