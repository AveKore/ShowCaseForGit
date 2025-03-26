namespace CodeBase.Core.Ui
{
    public struct WindowState<T> where T : UiWindow
    {
        public readonly T window;
        public readonly UiPanelState state;

        public WindowState(T window)
        {
            this.window = window;
            state = window.State;
        }
    }
}