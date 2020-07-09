using UnityEngine;

namespace Path
{
    public sealed class PathDrawer
    {
        public IPath Path { get; set; }
        public bool VisiblePoints { get; set; }

        public void Draw()
        {
            using (var iterator = Path.GetVectors().GetEnumerator())
            {
                if (!iterator.MoveNext()) return;
                while (true)
                {
                    var p = iterator.Current;
                    if (VisiblePoints) Gizmos.DrawCube(p,Vector3.one*0.15f);
                    if (!iterator.MoveNext()) return;
                    Gizmos.DrawLine(p, iterator.Current);
                }
            }
        }
    }
}