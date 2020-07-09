using System;
using System.Collections.Generic;
using System.Linq;

namespace Balls
{
    public sealed class SubChainMovement : List<SubChain>
    {
        private float _maxSpeed = 1f;

        public float MaxSpeed
        {
            get => _maxSpeed;
            set => _maxSpeed = value > 0.1f ? value : 0.1f;
        }

        public SubChainMovement(float maxSpeed)
        {
            MaxSpeed = maxSpeed;
        }

        public void IncreaseSpeed(int id, float speed, int count)
        {
            var movement = this.FirstOrDefault(i => i.Id == id);
            if (movement == null)
            {
                movement = new SubChain(id, speed, count);
                Add(movement);
            }

            if (Math.Abs(movement.Speed + speed) < MaxSpeed)
            {
                movement.Speed += speed;
            }
        }

        public void UpdateAfterRemoving(int id, int count)
        {
            for (var i = id; i < id + count; i++)
            {
                Dispose(i);
            }

            foreach (var group in this.Where(i=> i.Id >= id+count))
            {
                group.Id -= count;
            }
        }

        public bool ContainsId(int id)
        {
            return this.Any(i => i.Id == id);
        }
        
        public void Dispose(int id)
        {
            RemoveAll(i => i.Id == id);
        }
    }
}