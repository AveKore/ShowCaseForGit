using UnityEngine;

namespace CodeBase.Servises.Input
{
    public abstract class InputService : IInputService
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";
        protected const string Fire = "Attack";
        public abstract Vector2 Axis {get;}

        public abstract bool IsAttackButtonUp();

        protected Vector2 SimpleInputAxis()
        {
            return new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
        }
    }
}


