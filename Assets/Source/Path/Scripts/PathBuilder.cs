using UnityEngine;

namespace Path.Scripts
{
    public sealed class PathBuilder : MonoBehaviour
    {
        private IPath _path = new StrongPath();
        public IPath Path => _path;

        [SerializeField] private bool _curve = false;
        [SerializeField] private int _interval = 5;

        public void BuildPath()
        {
            if (_curve)
            {
                if(!(_path is CurvePath)) _path = new CurvePath();
            }
            else
            {
                if(!(_path is StrongPath)) _path = new StrongPath();
            }
            _path.SetPoints(GetComponentsInChildren<ObjectPoint>())
                .SetInterval(_interval)
                .Build();
        }
        private void Start()
        {
            BuildPath();
        }
    }
}