using System;
using System.Collections.Generic;
using CodeBase.Extencions;
using UnityEngine;
using Zenject;

namespace CodeBase.Core.Ui
{
    public class UiSceneWindowsProvider : MonoBehaviour
    {
        public Canvas GetRootCanvas() => transform.parent.GetComponent<Canvas>();

        private readonly SortedDictionary<int, Transform> _layersTransforms = new();
        
        private readonly List<UiWindow> _createdWindows = new();

        private bool _initialized;
        
        public T GetWindow<T>() where T : UiWindow
        {
            T bestMatch = null;
            foreach (var w in _createdWindows)
            {
                if (w is T window)
                {
                    if (w.GetType() == typeof(T))
                    {
                        bestMatch = window;
                        break;
                    }

                    if (bestMatch == null)
                    {
                        bestMatch = window;
                    }
                }
            }

            return bestMatch;
        }

        private void SingleInitialize(DiContainer diContainer)
        {
            if (_initialized) return;

            _initialized = true;
            CollectWindows(diContainer);
            InitGameSpaceLayersTransforms();
        }
        

        private void CollectWindows(DiContainer diContainer)
        {
            List<UiWindow> windowsToShutdown = new List<UiWindow>();
            for (int i = 0; i < transform.childCount; i++)
            {
                var layer = transform.GetChild(i);

                for (int j = 0; j < layer.childCount; j++)
                {
                    var obj = layer.GetChild(j);
                    var wnd = obj.GetComponent<UiWindow>();
                    diContainer.Inject(wnd);
                    if (wnd)
                    {
                        windowsToShutdown.Add(wnd);
                        _createdWindows.Add(wnd);
                    }
                }
            }
            foreach (var uiWindow in windowsToShutdown)
            {
                uiWindow.Close();
            }
        }
        
        private void InitGameSpaceLayersTransforms()
        {
            var layers = Enum.GetValues(typeof(UiWindowLayer));
            var layerNames = Enum.GetNames(typeof(UiWindowLayer));

            for (int i = 0; i < layers.Length; i++)
            {
                var layer = (int)layers.GetValue(i);
                var layerName = layerNames[i];
                var existedLayerTransform = transform.Find(layerName);
                var layerTransform = existedLayerTransform ? existedLayerTransform : new GameObject(layerName, typeof(RectTransform)).RectTransform();
                layerTransform.SetParent(transform);
                _layersTransforms.Add(layer, layerTransform);
            }
        }

        public void InjectWindows(DiContainer diContainer)
        {
            SingleInitialize(diContainer);
        }
    }
}
