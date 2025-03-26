using UnityEngine;

namespace CodeBase.Servises.Input
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis {
            get
            {
                Vector2 axis = SimleInputAxis();

                if (axis == Vector2.zero)
                    axis = UnityAxis();

                return axis;
            }
        }
    }
}


