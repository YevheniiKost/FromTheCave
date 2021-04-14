using UnityEngine;

public class PickupableItem : MonoBehaviour, ISaveState
{
    public string SaveKey => $"Item-{gameObject.name}-PickedUp";

    protected bool isActive = true;

    public void Save() => PlayerPrefs.SetInt(SaveKey, isActive ? 0 : 1);
    public void Load()
    {
        int isActive = PlayerPrefs.GetInt(SaveKey);
        DisableItem(isActive == 0);
    }

    protected void DisableItem(bool isOn)
    {
        if (!isOn)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        else
        {
            GetComponent<CapsuleCollider2D>().enabled = true;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
