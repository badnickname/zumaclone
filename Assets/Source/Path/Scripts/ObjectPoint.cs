using UnityEngine;

namespace Path.Scripts
{
    public sealed class ObjectPoint : MonoBehaviour, IPoint
    {
        public Vector3 GetVector()
        {
            return transform.position;
        }
    }
}