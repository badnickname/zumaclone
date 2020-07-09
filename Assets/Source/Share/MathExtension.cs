using UnityEngine;

namespace Share
{
    public static class MathExtension
    {
        public static Vector3 Direction2dTo(this Vector3 from, Vector3 to)
        {
            var dir = to - from;
            dir.z = 0;
            dir /= dir.magnitude + Mathf.Epsilon;
            return dir;
        }

        public static float ToAngle(this Vector3 to)
        {
            var angle = Vector3.Angle(Vector3.up, to);
            if (to.x > 0) angle = -angle;
            return angle;
        }
    }
}