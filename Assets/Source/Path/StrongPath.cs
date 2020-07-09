using System.Collections.Generic;
using UnityEngine;

namespace Path
{
    public sealed class StrongPath : IPath
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
            _points.Clear();
            foreach (var o in _objects)
            {
                _points.Add(o.GetVector());
            }
            
            return this;
        }

        public ICollection<Vector3> GetVectors()
        {
            return _points;
        }
    }
}