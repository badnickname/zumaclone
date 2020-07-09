using System;
using UnityEngine;

namespace Sender.Scripts
{
    public sealed class SenderAnimations : MonoBehaviour
    {
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void Roll(int type)
        {
            _animator.SetInteger("Roll", 1);
        }
    }
}