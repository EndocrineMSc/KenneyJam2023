using UnityEngine;
using Pathing;
using System.Collections.Generic;

namespace Characters
{
    internal class CharacterMovement : MonoBehaviour
    {
        #region Fields and Properties

        private Character _character;
        private List<Vector2> _waypoints;
        private int _targetWaypointIndex = 0;

        private float _speed = 5;

        #endregion

        #region Functions

        private void Awake()
        {
            _character = GetComponent<Character>();
        }

        private void Start()
        {
            _waypoints = WaypointContainer.Instance.Waypoints;
        }

        private void Update()
        {
            if(_targetWaypointIndex < _waypoints.Count)
            {
                SetSpeed();
                MoveToNextWaypoint(_waypoints[_targetWaypointIndex]);
                SetRotation();
                SetWaypointTargetIndex();
            }
            else
            {
                CharacterEvents.RaiseReachedGoal();
                Destroy(gameObject);
            }
        }

        private void MoveToNextWaypoint(Vector2 waypoint)
        {
            _character.transform.position = Vector2.MoveTowards(_character.transform.position, waypoint, _speed * Time.deltaTime);
        }

        //Can be deleted or commented out if it looks weird in the final thing
        //Don't forget to remove in update in this case, too
        private void SetRotation()
        {
            var direction = _waypoints[_targetWaypointIndex] - (Vector2)_character.transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _character.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        private void SetWaypointTargetIndex()
        {
            _targetWaypointIndex += Vector2.Distance(_character.transform.position, _waypoints[_targetWaypointIndex]) < 0.1f ? 1 : 0;
        }

        private void SetSpeed()
        {
            _speed = _character.MovementSpeed;
        }


        #endregion
    }
}
