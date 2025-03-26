using UnityEngine;

namespace CodeBase.Servises.Input
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis => SimleInputAxis();
    }
}


