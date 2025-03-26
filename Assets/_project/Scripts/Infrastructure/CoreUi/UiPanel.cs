using UnityEngine;

namespace CodeBase.Core.Ui
{
    public abstract class UiPanel : MonoBehaviour
    {
        protected UiPanelState state;

        public UiPanelState State
        {
            get => state;
            set
            {
                if (state != value)
                {
                    state = value;
                    OnStateChanged();
                }
            }
        }
        
        public virtual void Open()
        {
            SetWindowState(UiPanelState.Opened);
            gameObject.SetActive(true);
        }
        
        public virtual void Close()
        {
            SetWindowState(UiPanelState.Closed);
        }
        
        public bool IsClosed => state == UiPanelState.Closed;
        public bool IsOpened => state == UiPanelState.Opened;

        protected abstract void OnStateChanged();

        protected abstract void OnBackButton();
        
        
        private void SetWindowState(UiPanelState newState)
        {
            if (state == newState)
            {
                return;
            }
            State = newState;
        }

    }
}