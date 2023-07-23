using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamCatcher.Graphics
{
    public static class TransformExtensions
    {
        public static int GetSortingOrder(this Transform transform, float yOffset = 0)
        {
            return -(int)((transform.position.y + yOffset) * 100);
        }
    }
}
