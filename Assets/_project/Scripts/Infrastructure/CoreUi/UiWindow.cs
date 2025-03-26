using System.Collections.Generic;

namespace CodeBase.Core.Ui
{
    public class UiWindow : UiPanel
    {
        public int Layer => _data?.layer ?? (int)UiWindowLayer.Base;

        protected UiWindowData _data;

        private UiWindow _parent;
        private List<UiWindow> _children;


        public class UiWindowData
        {
            public int layer;

            public UiWindowData()
            {
                layer = (int)UiWindowLayer.Base;
            }
        }

        public void Init(UiWindowData winData = null)
        {
            _data = winData;
            Open();
        }

        public void SetParent(UiWindow parent)
        {
            if (_parent != null && _parent != parent)
            {
                _parent.RemoveChild(this);
            }

            _parent = parent;
        }

        private void RemoveChild(UiWindow child)
        {
            if (_children != null && _children.Contains(child))
            {
                _children.Remove(child);
            }
        }

        public void AddChild(UiWindow child)
        {
            if (_children == null)
            {
                _children = new List<UiWindow>();
            }

            if (!_children.Contains(child))
            {
                _children.Add(child);
                child.SetParent(this);
            }
        }

        public override void Close()
        {
            CloseChildren();
            base.Close();
        }

        private void CloseChildren()
        {
            if (_children != null)
            {
                foreach (var w in _children)
                {
                    w.Close();
                }
            }
        }

        protected override void OnBackButton()
        {
            if (_children != null && _children.Count > 0)
            {
                _children[^1].Close();
            }
            else if (_parent != null)
            {
                _parent.OnBackButton();
            }
        }

        protected override void OnStateChanged()
        {
            if (state == UiPanelState.Closed)
            {
                gameObject.SetActive(false);
            }
        }

    }

    public abstract class UiWindow<TData> : UiWindow where TData : UiWindow.UiWindowData
    {
        protected TData data => _data as TData;
    }

}