using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Extencions
{
	public static class TransformExtension 
	{
		public static void RemoveAllChildren(this Transform transform)
		{
			RemoveChildren(transform, 0);
		}

		public static void RemoveChildren(this Transform transform, int fromIndex)
		{
            if (transform == null)
            {
                return;
            }
            
			while (transform.childCount > fromIndex)
			{
				var ch = transform.GetChild(transform.childCount - 1);
				ch.SetParent(null);
				Object.Destroy(ch.gameObject);
			}
		}

		public static RectTransform RectTransform(this GameObject obj)
		{
			return obj.transform as RectTransform;
		}

		public static void RemoveAllChildren(this GameObject obj)
		{
			RemoveAllChildren(obj.transform);
		}

		public static void DeactivateChildren(this GameObject obj, int fromIndex)
		{
			DeactivateChildren(obj.transform, fromIndex);
		}

        public static void ActivateChildren(this GameObject obj, int fromIndex)
        {
            ActivateChildren(obj.transform, fromIndex);
        }

        public static void DeactivateChildren<TComponent>(this Transform transform, int fromIndex) where TComponent : Component
        {
            if(fromIndex < 0)
            {
                fromIndex = 0;
            }

            if(fromIndex >= transform.childCount)
            {
                return;
            }

            for (int i = fromIndex; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                if (child.GetComponent<TComponent>() != null)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
        
        public static void DeactivateAllChildren<TComponent>(this Transform transform) where TComponent : Component
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                if (child.GetComponent<TComponent>() != null)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
        
		public static void DeactivateChildren(this Transform transform, int fromIndex)
		{
			if(fromIndex < 0)
			{
				fromIndex = 0;
			}

			if(fromIndex >= transform.childCount)
			{
				return;
			}

			for (int i = fromIndex; i < transform.childCount; i++)
			{
				transform.GetChild(i).gameObject.SetActive(false);
			}
		}

        public static void ActivateChildren(this Transform transform, int fromIndex)
        {
            if (fromIndex < 0)
            {
                fromIndex = 0;
            }

            if (fromIndex >= transform.childCount)
            {
                return;
            }

            for (int i = fromIndex; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        public static TComponent GetOrAddNextChild<TComponent>(this Transform transform, TComponent prefab, ref int childIdx) where TComponent : Component
        {
            TComponent result = null;
            do
            {
                result = transform.ActivateOrAddNewChild(prefab, childIdx++) as TComponent;
            }
            while (!result);

            return result;
        }
		public static TComponent ActivateOrAddNewChild<TComponent>(this Transform transform, TComponent componentPrefab, int index) where TComponent : Component
		{
			if(transform.childCount > index)
			{
				Transform child = transform.GetChild(index);

                var result =  child.GetComponent<TComponent>();

                if (result)
                {
                    child.gameObject.SetActive(true);
                }

                return result;
            }
			
			var obj = Object.Instantiate(componentPrefab, transform);
			obj.gameObject.SetActive(true);

			return obj;
		}

		public static void Reset(this Transform transform)
		{
			transform.localPosition = Vector3.zero;
			transform.localScale = Vector3.one;
			transform.localRotation = Quaternion.identity;
		}

		public static void MoveBackward(this Transform transform, float distance)
		{
			transform.position -= transform.forward * distance;
		}

		public static void MoveForward(this Transform transform, float distance)
		{
			transform.position += transform.forward * distance;
		}

		public static Bounds CalcBounds(this GameObject gameObject)
		{
			var renderers = gameObject.GetComponentsInChildren<Renderer>();
			if(renderers.Length == 0)
			{
				return default;
			}
			var bounds = renderers[0].bounds;
			for (var i = 1; i < renderers.Length; ++i)
			{
				bounds.Encapsulate(renderers[i].bounds);
			}

			return bounds;
		}

        public static Bounds CalcLocalBounds(this GameObject gameObject)
        {
            var renderers = gameObject.GetComponentsInChildren<Renderer>();
            if(renderers.Length == 0)
            {
                return default;
            }
            var bounds = renderers[0].localBounds;
            for (var i = 1; i < renderers.Length; ++i)
            {
                bounds.Encapsulate(renderers[i].localBounds);
            }

            return bounds;
        }

		public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
		{
			T component = gameObject.GetComponent<T>();

			if(component == null)
			{
				component = gameObject.AddComponent<T>();
			}

			return component;
		}

		public static Component GetOrAddComponent(this GameObject gameObject, Type componentType)
		{
			Component component = gameObject.GetComponent(componentType);

			if(component == null)
			{
				component = gameObject.AddComponent(componentType);
			}

			return component;
		}
		
		public static bool IsScreenPointInside(this RectTransform rt, Vector2 point)
		{
			return RectTransformUtility.RectangleContainsScreenPoint(rt, point);
		}

        public static List<GameObject> GetAllChildren(this GameObject gameObject, bool includeInactive = false)
        {
            List<GameObject> result = new List<GameObject>();
            var children = gameObject.transform.GetComponentsInChildren<Transform>(includeInactive);

            foreach (var child in children)
            {
                result.Add(child.gameObject);
            }

            return result;
        }

        // transforms vector from local space (e.g. vertex or child object) to world space according to transform matrix
        public static Vector4 FromLocalToWorldSpace(this Transform transform, Vector4 position)
        {
            position.w = 1;
            return transform.localToWorldMatrix * position;
        }
    }
}
