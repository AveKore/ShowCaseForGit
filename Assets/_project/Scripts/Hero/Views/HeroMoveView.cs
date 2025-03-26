using System;
using System.Collections.Generic;
using CodeBase.Servises.Input;
using UnityEngine;
using Zenject;
using UniRx;

namespace CodeBase.Hero
{
    public class HeroMoveView : MonoBehaviour, IDisposable
    {
        private const float EPSILON = 0.001f;
        
        [SerializeField] private CharacterController _characterController;
        
        [Inject] private IInputService InputService { get; }
        
        private readonly List<IDisposable> _disposables = new();
        private float _movementSpeed;

        public void Init(HeroSpeedModel speedModelModel, HeroHealthModel heroModelHealthModel)
        {
            _movementSpeed = speedModelModel.Speed.Value;
            speedModelModel.Speed.Subscribe(UpdatePlayerSpeed).AddTo(_disposables);
            heroModelHealthModel.IsDead.Subscribe(PlayerDead).AddTo(_disposables);
        }
        
        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
            _disposables.Clear();
        }
        
        private void PlayerDead(bool isDead)
        {
            if (!isDead) return;

            enabled = false;
        }
        
        private void UpdatePlayerSpeed(float speed)
        {
            _movementSpeed = speed;
        }
        
        private void Update()
        {
            Vector3 movementVector = Vector3.zero;
            
            if (InputService.Axis.sqrMagnitude > EPSILON)
            {
                if (Camera.main != null)
                {
                    movementVector = Camera.main.transform.TransformDirection(InputService.Axis);
                }

                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;
            _characterController.Move(_movementSpeed*movementVector*Time.deltaTime);
        }
    }
}