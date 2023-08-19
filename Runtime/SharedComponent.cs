using System;
using UnityEngine;


namespace DagraacSystems.Unity
{
	/// <summary>
	/// 싱글톤 컴포넌트.
	///	 - 생성 : Create()를 호출 (혹은 Instance).
	///	 - 생성여부확인 : HasInstance()를 호출.
	///  - 삭제 : Dispose()를 호출.
	///  ※ AssetPathAttribute를 사용하면 리소스에 기반한 매니저 생성 가능.
	/// </summary>
	public class SharedComponent<TComponent> : MonoBehaviour, IDisposable where TComponent : SharedComponent<TComponent>
	{
		private static TComponent instance = null;

		public static TComponent Instance => Create();
		

		/// <summary>
		/// 인스턴스 존재 유무.
		/// </summary>
		public static bool HasInstance()
		{
			return instance != null;
		}

		/// <summary>
		/// 생성됨.
		/// </summary>
		private void Awake()
		{
			if (instance != null && instance != this)
			{
				GameObject.DestroyImmediate(gameObject);
				return;
			}
		}

		/// <summary>
		/// 파괴됨.
		/// </summary>
		private void OnDestroy()
		{
			if (instance != null && instance == this)
			{
				OnDispose();
				instance = null;
			}
		}

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected virtual void OnCreate(params object[] _args)
		{
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected virtual void OnDispose()
		{
		}

		/// <summary>
		/// 생성.
		/// </summary>
		public static TComponent Create(params object[] _args)
		{
			if (HasInstance())
				return instance;

			instance = GameObject.FindObjectOfType<TComponent>();
			if (instance != null)
			{
				instance.OnCreate(_args);
				return instance;
			}

			var assetPathInfo = AssetPathAttribute.GetAssetPathInfo<TComponent>();

			var gameObject = default(GameObject);
			//if (assetPathInfo.IsValid() && ResourceManager.HasInstance())
			//{
			//	var assetPath = ResourceManager.GetAssetPath(assetPathInfo.assetPath, true);
			//	if (assetPathInfo.isBuiltin)
			//	{
			//		var asset = Resources.Load<GameObject>(assetPath);
			//		gameObject = GameObject.Instantiate<GameObject>(asset);
			//		gameObject.name = typeof(TComponent).Name;
			//	}
			//	else
			//	{
			//		Debug.LogError($"[SharedComponent] Not Support Addressables GameObject Path. '{assetPath}'");
			//		gameObject = new GameObject(typeof(TComponent).Name);
			//	}
			//}
			//else
			//{
				gameObject = new GameObject(typeof(TComponent).Name);
			//}

#if UNITY_EDITOR
			Debug.Log($"[SharedComponent] Create({gameObject.name})");
#endif

			instance = gameObject.GetOrAddComponent<TComponent>();
			GameObject.DontDestroyOnLoad(gameObject);
			instance.OnCreate(_args);
			return instance;
		}

		/// <summary>
		/// 해제.
		/// </summary>
		public void Dispose()
		{
			if (instance != null && instance == this)
			{
				if (instance.gameObject != null)
				{
					GameObject.Destroy(instance.gameObject);
				}

				if (instance != null)
				{
					instance.OnDispose();
					instance = null;
				}
			}
		}
	}
}