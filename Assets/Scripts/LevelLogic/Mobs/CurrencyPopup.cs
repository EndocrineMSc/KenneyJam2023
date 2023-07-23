using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    internal class CurrencyPopup : MonoBehaviour
    {
        private void Awake()
        {
            transform.DOBlendableLocalMoveBy(new(0, 1.5f, 0), 0.5f);
            StartCoroutine(DestroyAfterDelay());
        }

        private IEnumerator DestroyAfterDelay()
        {
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
    }
}
