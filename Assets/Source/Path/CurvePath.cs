using System.Collections.Generic;
using UnityEngine;

namespace Path
{
    public sealed class CurvePath : IPath
    {
        private List<IPoint> _objects = new List<IPoint>();
        private List<Vector3> _points = new List<Vector3>();
        private float _interval = 1;
        
        public IPath SetPoints(ICollection<IPoint> points)
        {
            _objects.Clear();
            foreach (var o in points)
            {
                _objects.Add(o);
            }

            return this;
        }

        public IPath SetInterval(float i)
        {
            _interval = i;
            if (_interval < 1) _interval = 1;

            return this;
        }
        
        public IPath Build()
        {
            if (_objects.Count < 1) return this;
            _points.Clear();
            var deltaInterval = 1f / _interval;

            var p0 = _objects[0].GetVector();
            for (var i = 0; i < _objects.Count - 3; i+=3)
            {
                var p1 = _objects[i + 1].GetVector();
                var p2 = _objects[i + 2].GetVector();
                var p3 = _objects[i + 3].GetVector();

                for (var t = 0f; t < 1f; t += deltaInterval)
                {
                    var step = CubeBezierPoint(p0, p1, p2, p3, t);
                    _points.Add(step);
                }
                p0 = CubeBezierPoint(p0, p1, p2, p3, 1f);
            }
            
            return this;
        }

        public ICollection<Vector3> GetVectors()
        {
            return _points;
        }

        private Vector3 SqrBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            return (1 - t) * (1 - t) * p0 + 2 * (1 - t) * t * p1 + t * t * p2;
        }
        
        private Vector3 CubeBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            return Mathf.Pow(1 - t, 3) * p0 + 3 * Mathf.Pow(1 - t, 2) * t * p1 + 3 * (1 - t) * Mathf.Pow(t, 2) * p2 +
                   Mathf.Pow(t, 3) * p3;
        }
    }
}