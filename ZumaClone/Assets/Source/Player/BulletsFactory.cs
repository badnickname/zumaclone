using System;
using Player.Scripts;
using Share;
using Random = UnityEngine.Random;

namespace Player
{
    public sealed class BulletsFactory : IFactory
    {
        private Bullet _prototype;
        private int _types = 0;
        private int _exptected = 0;
        private int _current = 0;

        public int Exptected => _exptected;
        public int Current => _current;

        public IFactory SetPrototype(ICloneable obj)
        {
            _prototype = obj as Bullet;
            return this;
        }

        public IFactory SetCount(int count)
        {
            throw new NotImplementedException();
        }

        public IFactory SetTypes(int types)
        {
            _types = types;
            return this;
        }

        public bool Next()
        {
            _current = _exptected;
            _exptected = Random.Range(0, _types);
            return true;
        }

        public object Instance()
        {
            var bullet = _prototype.Clone() as Bullet;
            bullet.Type = _current;
            return bullet;
        }
    }
}