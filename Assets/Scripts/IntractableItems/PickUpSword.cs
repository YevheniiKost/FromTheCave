using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSword : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(UpDownAnimation());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerCombat player))
        {
            player.GetSword();
            //TODO particles
            Destroy(this.gameObject);
        }
    }
     private IEnumerator UpDownAnimation()
    {
        float number = 0;
        while(number < 1)
        {
            transform.position += Vector3.up * .02f;
            number += .1f;
            yield return new WaitForSeconds(.08f);
        }
        number = 0;
        while(number < 1)
        {
            transform.position += Vector3.down * .02f;
            number += .1f;
            yield return new WaitForSeconds(.08f);
        }

        StartCoroutine(UpDownAnimation());
        yield return null;

    }
}
