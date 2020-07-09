using System;
using System.Collections.Generic;
using UnityEngine;

namespace Path.Scripts
{
    public sealed class PathMovement
    {
        private IPath _path;
        private float _traveled;
        private readonly float _length;
        private readonly List<Vector3> _points;
        private Vector3 _position;
        private int _pEnum = 0;

        public float Traveled
        {
            get => _traveled/_length;
            set => SetTraveledDistance(value);
        }

        public Vector3 Position => _position;

        public float Length => _length;

        public PathMovement(IPath path)
        {
            _path = path;
            _length = path.Length();
            _traveled = 0;
            _points = new List<Vector3>(path.GetVectors());
            _position = _points[0];
        }

        public PathMovement Reset()
        {
            _traveled = 0;
            _position = _points[0];
            _pEnum = 0;
            return this;
        }

        public Vector3 Move(float speed)
        {
            // TODO what i've done...
            var sign = Math.Sign(speed);

            if (sign > 0)
            {
                var distance = _points[_pEnum+1] - _points[_pEnum];
                var moved = distance - (_points[_pEnum + 1] - _position);

                var move = speed * (distance / distance.magnitude);
                var leftToGo = distance.magnitude - (moved + move).magnitude;
            
                if (leftToGo <= 0)
                {
                    if (_pEnum + 2 >= _points.Count) return _points[_points.Count - 1];
                
                    _pEnum += 1;
                    _position = _points[_pEnum];

                    _traveled += move.magnitude - Mathf.Abs(leftToGo);
                
                    Move(Mathf.Abs(leftToGo));
                }
                else
                {
                    _traveled += move.magnitude;
                    _position += move;
                }
            }
            else
            {
                var target = _points[_pEnum];
                var distance = target - _position;
                var move = speed * (distance / (distance.magnitude+Mathf.Epsilon));
                
                var leftToGo = distance.magnitude - move.magnitude;
                if (leftToGo <= 0)
                {
                    if (_pEnum - 1 < 0)
                    {
                        _traveled = 0;
                        return _points[0];
                    }
                    
                    _pEnum -= 1;
                    _position = _points[_pEnum+1];
                    
                    _traveled -= move.magnitude - Mathf.Abs(leftToGo);
                    Move(-Mathf.Abs(leftToGo));
                }
                else
                {
                    _traveled -= move.magnitude;
                    _position -= move;
                }
            }


            return _position;
        }
        
        private Vector3 SetTraveledDistance(float procent)
        {
            if (procent < 0 || procent > 1) return _position;
            var speed = _length * procent;
            return Reset().Move(speed);
        }
    }
}