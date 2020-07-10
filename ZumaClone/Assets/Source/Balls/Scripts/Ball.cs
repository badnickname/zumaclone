using System;
using Path.Scripts;
using Player.Scripts;
using Share;
using UnityEngine;

namespace Balls.Scripts
{
    public sealed class Ball : MonoBehaviour, ICloneable
    {
        [SerializeField] private PathBuilder _builder = null;
        [SerializeField] private SphereCollider _collider = null;
        private PathMovement _movement;
        private Vector3 _direction = Vector3.zero;

        public float Size => transform.localScale.x * _collider.radius * 2;
        public float Traveled => _movement.Traveled * _movement.Length;

        public Vector3 Direction => _direction;

        public int Type { get; set; } = 0;

        public object Clone()
        {
            var obj = Instantiate(this);
            obj._builder.BuildPath();
            obj._movement = new PathMovement(_builder.Path);
            return obj;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<Bullet>() != null)
            {
                Destroy(other.gameObject);
            }
        }

        public Vector3 Move(float speed)
        {
            _movement.Move(speed);
            var position = new Vector3(_movement.Position.x, _movement.Position.y, transform.position.z);
            
            _direction = position - transform.position;
            var angle = Direction.ToAngle();
            transform.rotation = Quaternion.Euler(0, 0, angle);
            
            transform.position = position;
            return transform.position;
        }
    }
}