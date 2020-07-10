using System.Collections.Generic;
using UnityEngine;

namespace Path
{
    public interface IPath
    {
        IPath SetPoints(ICollection<IPoint> points);
        IPath SetInterval(float i);
        IPath Build();
        ICollection<Vector3> GetVectors();
    }
}