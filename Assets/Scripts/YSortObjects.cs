using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamCatcher.Graphics
{
    public class YSortObjects : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            //spriteRenderer.sortingOrder = transform.GetSortingOrder();
        }
    }
}
