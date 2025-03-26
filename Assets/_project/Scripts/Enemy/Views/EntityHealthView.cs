using System;
using System.Collections.Generic;
using CodeBase.Entity;
using UniRx;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EntityHealthView : MonoBehaviour, IDisposable
    {
        private static readonly int DamagedTriggerHash = Animator.StringToHash("DamagedTrigger");

        [SerializeField] private Animator _animator;

        private int _curHealth;
        private readonly List<IDisposable> _disposables = new();

        public void Init(EntityHealthModel healthModel)
        {
            _curHealth = healthModel.Health.Value;
            healthModel.Health.Subscribe(OnDead).AddTo(_disposables);
        }

        private void OnDead(int health)
        {
            if (_curHealth > health)
            {
                _animator.SetTrigger(DamagedTriggerHash);
            }
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }

            _disposables.Clear();
        }
    }
}