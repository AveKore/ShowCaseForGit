using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeBase.Servises.Input
{
    public class StandaloneInputService : InputService
    {
        private InputAction moveAction;
        private InputAction attackAction;
        
        public override Vector2 Axis {
            get
            {
                Vector2 axis = SimpleInputAxis();

                if (axis == Vector2.zero)
                    axis = GetUnityAxis();

                return axis;
            }
        }
        
        public override bool IsAttackButtonUp()
        {
            attackAction = InputSystem.actions.FindAction(Fire);
            var result = attackAction.ReadValue<float>();
            return result != 0;
        }

        private Vector2 GetUnityAxis()
        {
            moveAction ??= InputSystem.actions.FindAction("Move");
            return moveAction.ReadValue<Vector2>();
        }
    }
}


