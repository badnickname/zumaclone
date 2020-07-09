using System;
using System.Collections.Generic;
using Balls.Scripts;
using UnityEngine;

namespace Balls
{
    public sealed class ChainMovement
    {
        private IList<Ball> _balls;
        private SubChainMovement _subChain = new SubChainMovement(0.2f);

        private float _speed = 0.2f;
        private float _friction = 0.05f;
        private float _selfFriction = 0.05f;
        private float _maxSpeed = 0.2f;

        public event SubChainCollisionsHandler CollisionAccepted;
        public float Speed
        {
            get => _speed;
            set => _speed = Mathf.Abs(value);
        }
        
        public float Friction
        {
            get => _friction;
            set => _friction = Mathf.Abs(value);
        }
        
        public float SelfFriction
        {
            get => _selfFriction;
            set => _selfFriction = value > 0.001f ? value : 0.001f;
        }

        public float MaxSpeed
        {
            get => _maxSpeed;
            set
            {
                _maxSpeed = Mathf.Abs(value);
                _subChain.MaxSpeed = value;
            }
        }

        // Constructor. Set listeners
        public ChainMovement(IList<Ball> balls)
        {
            _balls = balls;
            
            CollisionAccepted += args =>
            {
                _subChain.UpdateAfterRemoving(args.Id, args.Count);
            };
        }

        public void AddMotion(int id, float speed, int count)
        {
            _subChain.IncreaseSpeed(id, speed, count);
        }

        public bool ContainsMotion(int id)
        {
            return _subChain.ContainsId(id);
        }

        public void DoMotions(float dt)
        {
            if (_balls.Count < 1) return;
            var closingId = _balls.Count - 1;
            MoveBall(closingId, Speed*dt);

            foreach (var motion in _subChain)
            {
                var lastId = MoveBall(motion.Id, motion.Speed*dt);

                if (lastId <= motion.Id + motion.Count)
                {
                    _subChain.IncreaseSpeed(motion.Id, -Mathf.Sign(motion.Speed) * SelfFriction * dt, motion.Count);
                    continue;
                }
                
                if (Mathf.Abs(motion.Speed) - Friction*dt < 0)
                {
                    CollisionAccepted(new SubChainCollisionsArgs(motion.Id, motion.Count));
                    break;
                }

                _subChain.IncreaseSpeed(motion.Id, -Mathf.Sign(motion.Speed) * Friction * dt, motion.Count);
            }
        }

        private int PushChain(int id, int lastId, Func<int, int> increment)
        {
            var sign = Math.Sign(id-lastId);
            for (var i = id; i != lastId; i = increment(i))
            {
                var dist = _balls[increment(i)].Traveled - _balls[i].Traveled;
                if (dist * sign < _balls[i].Size)
                {
                    var translation = sign * _balls[i].Size - dist;
                    _balls[increment(i)].Move(translation);
                    continue;
                }
                return i;
            }

            return lastId;
        }
        
        private int MoveBall(int id, float speed)
        {
            var sign = Math.Sign(speed);
            _balls[id].Move(speed);
            if (sign > 0)
            {
                return PushChain(id, 0, i => i - 1);
            }
            return PushChain(id, _balls.Count-1, i => i + 1);
        }
    }
}