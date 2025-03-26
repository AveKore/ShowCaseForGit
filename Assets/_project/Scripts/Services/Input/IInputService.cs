using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Servises.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
        bool IsAttackButtonUp();
    }
}


