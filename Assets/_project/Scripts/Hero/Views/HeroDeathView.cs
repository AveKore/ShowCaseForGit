using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroDeathView : MonoBehaviour, IDisposable
    {
        private static readonly int DissolveOffest = Shader.PropertyToID("_Dissolve");
        private const float DISSOLVE_DOWN_VALUE = 0;
        private const float DISSOLVE_UP_VALUE = 1;
        
        [SerializeField] private ParticleSystem _deathFx;
        [SerializeField] private Renderer _dissolveTarget;
        private Material _material;
        
        private readonly List<IDisposable> _disposables = new();

        private void Awake()
        {
            _material = _dissolveTarget.material;
        }

        public void Init(HeroHealthModel healthModelModel)
        {
            healthModelModel.IsDead.Subscribe(RunDieFx).AddTo(_disposables);
        }

        private async void RunDieFx(bool isDead )
        {
            if (!isDead)
            {
                return;
            }

            PlayDissolveEffect();
            PlayDeathFx();
            await UniTask.WaitForSeconds(_deathFx.main.duration)
                .AttachExternalCancellation(destroyCancellationToken)
                .SuppressCancellationThrow();
            Destroy(gameObject); 
        }

        private void PlayDeathFx()
        {
            _deathFx.gameObject.SetActive(true);
            _deathFx.Play();
        }

        private async void PlayDissolveEffect()
        {
            var dissolveValue = DISSOLVE_DOWN_VALUE;
            var duration = DISSOLVE_UP_VALUE * _deathFx.main.duration;
            while (dissolveValue < DISSOLVE_UP_VALUE)
            {
                var step = duration * Time.deltaTime;
                dissolveValue += step;
                _material.SetFloat(DissolveOffest, dissolveValue);
                await UniTask.Yield(destroyCancellationToken, true).SuppressCancellationThrow();
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