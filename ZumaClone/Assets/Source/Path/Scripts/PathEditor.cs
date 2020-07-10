using UnityEngine;

namespace Path.Scripts
{
    public sealed class PathEditor : MonoBehaviour
    {
        private PathBuilder _builder;
        private readonly PathDrawer _drawer = new PathDrawer();
        [SerializeField] private bool _drawUi = false;

        private bool _needUpdate = true;

        private void OnDrawGizmos()
        {
            // TODO refactor
            
            if (!_drawUi)
            {
                _needUpdate = true;
                return;
            }

            if (_needUpdate)
            {
                _builder = GetComponent<PathBuilder>();
                _builder.BuildPath();
                _drawer.Path = _builder.Path;
                _needUpdate = false;
            }

            try
            {
                _drawer.Path.Build();
            }
            catch
            {
                _needUpdate = true;
                return;
            }
            _drawer.Draw();
        }
    }

}