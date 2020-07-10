using UnityEngine;

namespace Player.Scripts
{
    internal delegate void MouseHandler(Vector3 pos);
    public sealed class InputController : MonoBehaviour
    {
        [SerializeField] private Player _player = null;
        [SerializeField] private Camera _camera = null;
        private event MouseHandler MousePressed;

        public InputController()
        {
            MousePressed += OnTouchDown;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) MousePressed(Input.mousePosition);
        }

        private void OnTouchDown(Vector3 touch)
        {
            var point = _camera.ScreenToWorldPoint(touch);
            _player.Fire(point);
        }
    }
}