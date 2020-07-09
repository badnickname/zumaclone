using Share;
using UnityEngine;

namespace Player.Scripts
{
    public sealed class Player : MonoBehaviour
    {
        [SerializeField] private Bullet _prototype = null;
        [SerializeField] private int _types = 4;

        private BulletsFactory _bulletsFactory = new BulletsFactory();

        private void Start()
        {
            _prototype.transform.position = transform.position;
            _bulletsFactory.SetPrototype(_prototype)
                .SetTypes(_types);
        }

        public void Fire(Vector3 to)
        {
            var dir = transform.position.Direction2dTo(to);
            var bullet = _bulletsFactory.Instance() as Bullet;
            bullet.Direction = dir;
            _bulletsFactory.Next();
        }
    }
}