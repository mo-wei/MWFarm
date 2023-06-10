using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggleItemFade : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemFade[] itemFades = collision.GetComponentsInChildren<ItemFade>();
        if(itemFades.Length > 0)
        {
            foreach (var item in itemFades)
            {
                item.FadeOut();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ItemFade[] itemFades = collision.GetComponentsInChildren<ItemFade>();
        if (itemFades.Length > 0)
        {
            foreach (var item in itemFades)
            {
                item.FadeIn();
            }
        }
    }
}
