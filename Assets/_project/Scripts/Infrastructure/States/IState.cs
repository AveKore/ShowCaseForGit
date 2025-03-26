namespace CodeBase.States
{
    public interface IExitableState
    {
        void Exit();
    }

    public interface IState : IExitableState
    {
        void Enter();

    }

    public interface IPayLoadedState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);

    }
}