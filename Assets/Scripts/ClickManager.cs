using UnityEngine;

namespace Assets.Scripts
{
	public class ClickManager : MonoBehaviour
	{
		public Camera camera;
		public Player player;


		void Start()
		{
		}

		// Update is called once per frame
		void Update()
		{
			ViewRay();
			if (Input.GetMouseButtonDown(0))
				Click(Input.mousePosition);
		}

		private Vector3 hitpoint;

		private void ViewRay()
		{
			var position = Input.mousePosition;
			Ray ray = camera.ScreenPointToRay(position);

			if (Physics.Raycast(ray, out RaycastHit hit))
			{
				LandMarkObject obj = hit.rigidbody?.gameObject?.GetComponent<LandMarkObject>();
				if (obj == null)
					return;

				hitpoint = hit.point;
			}

			Debug.DrawRay(ray.origin, ray.direction * 1000);
		}

		private void Click(Vector3 mousePosition)
		{
			Ray ray = camera.ScreenPointToRay(mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hit))
			{
				LandMarkObject obj = hit.rigidbody?.gameObject?.GetComponent<LandMarkObject>();
				if (obj == null)
					return;

				player.MoveTo(hit.point);
			}
		}

		public void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(hitpoint, 0.3f);
		}
	}
}