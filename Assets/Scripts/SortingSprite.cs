using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SortingSprite : MonoBehaviour
{
    public enum ESortingType
    {
        Static, Updata
    }

    [SerializeField] private ESortingType sortingType;

    private SpriteSorter sorter;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        sorter = FindObjectOfType<SpriteSorter>();    
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sortingOrder = sorter.GetSortingOrder(gameObject);
    }

    private void Update()
    {
        if(sortingType == ESortingType.Updata)
        {
            spriteRenderer.sortingOrder = sorter.GetSortingOrder(gameObject);
        }
    }
}
