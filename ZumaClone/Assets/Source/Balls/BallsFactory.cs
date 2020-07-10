using System;
using Balls.Scripts;
using Share;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Balls
{
    public sealed class BallsFactory : IFactory
    {
        private ICloneable _prototype;
        private int _count;
        private int _types;
        
        public IFactory SetPrototype(ICloneable obj)
        {
            _prototype = obj;
            return this;
        }

        public IFactory SetCount(int count)
        {
            _count = count;
            return this;
        }

        public IFactory SetTypes(int types)
        {
            _types = types;
            return this;
        }

        public bool Next()
        {
            _count -= 1;
            return _count >= 0;
        }

        public object Instance()
        {
            if (_count < 0) return null;
            
            var obj = _prototype.Clone() as Ball;
            obj.Type = Random.Range(0, _types);
            return obj;
        }
    }
}