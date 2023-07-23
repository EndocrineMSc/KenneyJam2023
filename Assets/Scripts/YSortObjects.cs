using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamCatcher.Graphics
{
    public class YSortObjects : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        // Start is called before the first frame update
        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            _spriteRenderer.sortingOrder = transform.GetSortingOrder();          
        }
    }
}
