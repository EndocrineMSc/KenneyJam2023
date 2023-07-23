using UnityEngine;
using Pathing;
using System.Collections.Generic;
using System;
using System.Transactions;

namespace Characters
{
    internal class CharacterMovement : MonoBehaviour, IComparable<CharacterMovement>
    {
        #region Fields and Properties

        private Character _character;
        private List<Vector2> _waypoints;
        internal int TargetWaypointIndex { get; private set; } = 0;

        private float _speed = 5;
        private bool _hasReachedGoal;

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
            if (!_hasReachedGoal)
            {
                if (TargetWaypointIndex < _waypoints.Count)
                {
                    SetSpeed();
                    MoveToNextWaypoint(_waypoints[TargetWaypointIndex]);
                    //SetRotation();
                    FlipSpriteInDirection();
                    UpdateWaypointTargetIndex();
                }
                else
                {
                    _hasReachedGoal = true;
                    CharacterEvents.RaiseReachedGoal(gameObject);
                    StartCoroutine(_character.Die(_hasReachedGoal));
                }
            }
        }

        private void MoveToNextWaypoint(Vector2 waypoint)
        {
            _character.transform.position = Vector2.MoveTowards(_character.transform.position, waypoint, _speed * Time.deltaTime);

            // Raise event if we want to use the start waitpoints as a "checkpoint" or do something there
        }

        private void FlipSpriteInDirection()
        {
            _character.GetComponent<SpriteRenderer>().flipX = (_waypoints[TargetWaypointIndex].x - transform.position.x > 0);
        }

        //Can be deleted or commented out if it looks weird in the final thing
        //Don't forget to remove in update in this case, too
        /*
        private void SetRotation()
        {
            var direction = _waypoints[TargetWaypointIndex] - (Vector2)_character.transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _character.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        */

        private void UpdateWaypointTargetIndex()
        {
            TargetWaypointIndex += Vector2.Distance(_character.transform.position, _waypoints[TargetWaypointIndex]) < 0.1f ? 1 : 0;
        }

        private void SetSpeed()
        {
            _speed = _character.MovementSpeed;
        }

        internal void SetWaypointIndex(int index)
        {
            TargetWaypointIndex = index;
        }

        public int CompareTo(CharacterMovement other)
        {
            if(other == null || this.GetComponent<Character>() == null) return 1;

            float myDistance = Vector2.Distance(this.transform.position, _waypoints[TargetWaypointIndex]);
            float otherDistance = Vector2.Distance(other.transform.position, other._waypoints[other.TargetWaypointIndex]);
            
            if(this.TargetWaypointIndex > other.TargetWaypointIndex || 
                this.TargetWaypointIndex == other.TargetWaypointIndex && myDistance > otherDistance)
            {
                return 1;
            } else if (this.TargetWaypointIndex < other.TargetWaypointIndex ||
                this.TargetWaypointIndex == other.TargetWaypointIndex && myDistance < otherDistance)
            {
                return -1;
            } else { return 0; }
        }

        #endregion
    }
}
