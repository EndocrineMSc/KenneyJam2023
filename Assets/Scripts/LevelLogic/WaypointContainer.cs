using System.Collections.Generic;
using UnityEngine;

namespace Pathing
{
    internal class WaypointContainer : MonoBehaviour
    {
        #region Fields and Properties

        internal static WaypointContainer Instance { get; private set; }

        [SerializeField] private GameObject[] _waypointObjects;

        internal List<Vector2> Waypoints { get; private set; } = new();

        #endregion

        #region Functions

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            foreach (var obj in _waypointObjects)
            {
                Waypoints.Add(obj.transform.position);
            }
        }

        #endregion
    }
}
