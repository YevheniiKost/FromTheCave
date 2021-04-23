using System.Collections;
using UnityEngine;

public class YouWinWindowUI : MonoBehaviour
{
    public static event UIWindowEvent OnYouWinWindowActive;

    private void Start()
    {
        OnYouWinWindowActive?.Invoke(gameObject);
    }
}
