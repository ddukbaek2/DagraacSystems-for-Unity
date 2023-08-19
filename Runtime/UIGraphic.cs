using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace DagraacSystems.Unity
{
	[RequireComponent(typeof(CanvasRenderer))]
	public class UIGraphic : Graphic, IPointerDownHandler, IPointerUpHandler
	{
		/// <summary>
		/// 메쉬 셋팅.
		/// </summary>
		protected override void OnPopulateMesh(VertexHelper vertexHelper)
		{
			// 메시를 그리지 않음.
			vertexHelper.Clear();
		}

		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			Debug.Log("OnPointerDown()");
		}

		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			Debug.Log("OnPointerUp()");
		}
	}
}