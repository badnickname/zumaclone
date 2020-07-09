using System;
using UnityEngine;

namespace Player.Scripts
{
    public sealed class Bullet : MonoBehaviour, ICloneable
    {
        [SerializeField] private MeshRenderer[] _meshes = null;
        [SerializeField] private float _speed = 10f;
        private MeshRenderer _image = null;
        private ParticleSystem _particles;
        private float _actualSpeed = 0f;
        
        public int Type { get; set; }
        public Vector3 Direction { get; set; }

        private void Start()
        {
            var func = new Func<int, int>(i => i % _meshes.Length);
            _image = Instantiate(_meshes[func(Type)]);
            _image.transform.localScale = transform.localScale/2;
        }

        private void DestroySelf()
        {
            Destroy(gameObject);
        }

        private void Update()
        {
            var time = Time.deltaTime;
            transform.position += _actualSpeed * time * Direction;
            _image.transform.position = transform.position;
        }

        private void OnDestroy()
        {
            if(_image != null) Destroy(_image.gameObject);
        }

        public object Clone()
        {
            // TODO Remove time
            
            var obj = Instantiate(this);
            obj.Type = Type;
            obj._actualSpeed = _speed;
            obj.Invoke(nameof(DestroySelf), 5f);
            return obj;
        }
    }
}