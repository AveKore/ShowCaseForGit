using UnityEngine;

namespace CodeBase.Servises.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        protected const string Fire = "Fire";
        public abstract Vector2 Axis {get;}

        public  bool IsAttackButtonUp() =>
            SimpleInput.GetButtonUp(Fire);

        protected Vector2 SimleInputAxis()
        {
            return new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
        }

        protected Vector2 UnityAxis()
        {
            return new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));
        }

    }
}


