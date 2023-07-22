using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Towers
{
    internal class Projectile : MonoBehaviour
    {
        protected Vector2 Target;
        protected float TravelSpeed;

        protected Vector2 StartingPositon;

        internal void DefineTarget(Vector2 Target, float TravelSpeed)
        {
            this.Target = Target;
            this.TravelSpeed = TravelSpeed;
        }

        public void Awake()
        {
            StartingPositon = transform.position;
            GetComponent<SpriteRenderer>().enabled = true;
        }

        public void Update()
        {
            transform.position = Vector3.MoveTowards(this.transform.position, Target, TravelSpeed * Time.deltaTime);

            if(transform.position.Equals(Target))
            {
                Destroy(this.gameObject);
            }
        }
    }
}