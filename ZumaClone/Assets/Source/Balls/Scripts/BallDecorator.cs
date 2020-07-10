using System;
using Share;
using UnityEngine;

namespace Balls.Scripts
{
    public class BallDecorator : MonoBehaviour
    {
        private Ball _ball = null;
        [SerializeField] private Renderer[] _sprites = null;
        private Renderer _image = null;
        private ParticleSystem _particle;
        public int Types => _sprites.Length;
        private float _rotationAngle = 0f;

        private void Start()
        {
            _ball = GetComponent<Ball>();
            var func = new Func<int, int>(i => i % _sprites.Length);
            _image = Instantiate(_sprites[func(_ball.Type)]);
            _particle = _image.GetComponent<ParticleSystem>();

            _image.transform.localScale = Vector3.one * (_ball.Size/1.5f); // TODO Find depend of 1.5
        }

        private void Update()
        {
            _rotationAngle -= Time.deltaTime * 60f;
            
            _image.transform.position = _ball.transform.position;
            var angle = _ball.Direction.ToAngle();
            var rotation = Quaternion.Euler(0, 0, angle);
            _image.transform.rotation = Quaternion.Slerp(_image.transform.rotation, rotation,  Time.deltaTime * 5f);
        }

        private void OnDestroy()
        {
            if (_particle != null)
            {
                _particle.Play();
            }
            Destroy(_image);
        }
    }
}