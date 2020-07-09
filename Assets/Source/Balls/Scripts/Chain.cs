using System;
using System.Collections.Generic;
using Share;
using UnityEngine;

namespace Balls.Scripts
{
    public delegate void SubChainCollisionsHandler(SubChainCollisionsArgs args);

    public sealed class Chain : MonoBehaviour
    {
        [SerializeField] private Ball _prototype = null;
        [SerializeField] private int _ballsCount = 5;
        [SerializeField] private float _speed = 0.02f;
        [SerializeField] private float _friction = 0.01f;
        [SerializeField] private float _maxSpeed = 0.1f;
        [SerializeField] private float _increase = 0.001f;
        [SerializeField] private float _selfFriction = 0.001f;
        [SerializeField] private UnityIntEvent OnCreateBall = null;
            
        private List<Ball> _balls = new List<Ball>();
        private ChainMovement _movement;
        private IFactory _ballsFactory = new BallsFactory();

        private void Start()
        {
            //Application.targetFrameRate = 30;
            
            _movement = new ChainMovement(_balls)
            {
                Speed = _speed,
                Friction = _friction,
                MaxSpeed = _maxSpeed,
                SelfFriction = _selfFriction
            };
            
            _movement.CollisionAccepted += args =>
            {
                var groupCount = GroupCount(args.Id, _balls.Count-1,
                    j => j + 1,
                    (k, i) => _balls[k].Type == _balls[i].Type);
                if (groupCount > 2)
                {
                    RemoveGroup(args.Id, args.Id + groupCount - 1);
                }
            };

            var types = _prototype.GetComponent<BallDecorator>().Types;
            _ballsFactory.SetCount(_ballsCount)
                .SetPrototype(_prototype)
                .SetTypes(types);

            //TODO remove this
            var testobj = _prototype.Clone() as Ball;
            testobj.Move(_prototype.Size * _ballsCount * 3);
            _balls.Add(testobj);
            testobj = _prototype.Clone() as Ball;
            testobj.Move(_prototype.Size * (_ballsCount * 3 + 1));
            _balls.Add(testobj);
            testobj = _prototype.Clone() as Ball;
            testobj.Move(_prototype.Size * (_ballsCount * 3 + 2));
            _balls.Add(testobj);
            testobj = _prototype.Clone() as Ball;
            testobj.Move(_prototype.Size * (_ballsCount * 4));
            testobj.Type = 0;
            _balls.Add(testobj);
            
            _balls.Reverse();
        }

        private int GroupCount(int from, int to, Func<int, int> increment, Func<int, int, bool> filter = null)
        {
            // TODO remove magic number
            const float eps = 0.001f;
            
            var count = 0;
            for (var i = from; i != to; i = increment(i))
            {
                count++;
                var nextI = increment(i);
                if (nextI > _balls.Count || nextI < 0) break;
                var dist = _balls[nextI].Traveled - _balls[i].Traveled;
                if (Mathf.Abs(dist) > _balls[i].Size + eps) break;

                if (filter != null && !filter(i, nextI)) break;
            }

            return count;
        }

        private void RemoveGroup(int from, int to)
        {
            for (var i = to; i >= from; i--)
            {
                Destroy(_balls[i].gameObject);
                _balls.RemoveAt(i);
            }
        }

        private bool PlaceFree(float len)
        {
            return _balls[_balls.Count - 1].Traveled > len;
        }
        
        private void Update()
        {
            // TODO remove magic number
            const float eps = 0.01f;
            
            // Movements
            _movement.DoMotions(Time.deltaTime);
            
            // Attraction of one-type balls
            for (var i = 0; i < _balls.Count - 1; i++)
            {
                if(_balls[i].Type != _balls[i+1].Type) continue;
                
                var dist = _balls[i].Traveled - _balls[i+1].Traveled;
                if (dist > _balls[i].Size + eps)
                {
                    var count = GroupCount(i, -1, j => j - 1);
                    var id = i - count + 1;
                    _movement.AddMotion(id, -_increase*Time.deltaTime, count);
                }
            }

            // Generating chain
            if (PlaceFree(_prototype.Size) && _ballsFactory.Next())
            {
                var ball = _ballsFactory.Instance() as Ball;
                OnCreateBall.Invoke(ball.Type);
                _balls.Add(ball);
            }
        }
        
    }
}