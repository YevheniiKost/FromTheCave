using System.Collections;
using UnityEngine;

[RequireComponent (typeof(CapsuleCollider2D))]
public class PickupableItem : MonoBehaviour, ISaveState
{
    [Header("Up-down animation settings")]
    [SerializeField] private bool _enableUpDownAnimation = true;
    [SerializeField] private float _yValueChangeForTick = .02f;
    [SerializeField] private float _tickTime = .08f;

    public string SaveKey => $"Item-{gameObject.name}-PickedUp";

    protected bool isActive = true;

    public void Save() => PlayerPrefs.SetInt(SaveKey, isActive ? 0 : 1);
    public void Load() => DisableItem(PlayerPrefs.GetInt(SaveKey) == 0);

    private void Start()
    {
        if (_enableUpDownAnimation)
            StartCoroutine(UpDownAnimation());
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

    private IEnumerator UpDownAnimation()
    {
        float number = 0;
        while (number < 1)
        {
            transform.position += Vector3.up * _yValueChangeForTick;
            number += .1f;
            yield return new WaitForSeconds(_tickTime);
        }
        number = 0;
        while (number < 1)
        {
            transform.position += Vector3.down * _yValueChangeForTick;
            number += .1f;
            yield return new WaitForSeconds(_tickTime);
        }

        StartCoroutine(UpDownAnimation());
        yield return null;
    }
}
