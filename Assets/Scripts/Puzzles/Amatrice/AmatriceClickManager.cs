using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Amatrice
{
	public class AmatriceClickManager : MonoBehaviour
	{
		public Camera camera;
		private Vector3 hitpoint;

		int tableLayer;

		[SerializeField] private Draggable activeDraggable;

		void Awake()
		{
			tableLayer = 1 << LayerMask.NameToLayer("Map");
		}
	
		void Update()
		{
			MouseMovement();
		}

		public Vector3 tablePoint;

		private void MouseMovement()
		{
			if (Input.GetMouseButtonUp(0) && activeDraggable != null)
			{
				Debug.Log("StopDrag");
				activeDraggable.StopDrag();
				activeDraggable = null;
			}

			var position = Input.mousePosition;
			Ray ray = camera.ScreenPointToRay(position);

			if (Physics.Raycast(ray, out RaycastHit tableHit, tableLayer))
			{
				tablePoint = tableHit.point;
			}

			// Draggable
			if (Physics.Raycast(ray, out RaycastHit hit))
			{
				Draggable obj = hit.rigidbody?.gameObject?.GetComponent<Draggable>();
				if (obj == null)
					return;

				hitpoint = hit.point;

				if (Input.GetMouseButtonDown(0))
				{
					activeDraggable = obj;
					activeDraggable.StartDrag();
				}
			}

			
		}

		public void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(hitpoint, 0.1f);

			Gizmos.color = Color.green;
			Gizmos.DrawSphere(tablePoint, 0.1f);

			Gizmos.color = Color.white;
			Gizmos.DrawLine(camera.transform.position, hitpoint);
		}
	}
}