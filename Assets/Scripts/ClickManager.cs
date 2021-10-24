using UnityEngine;

namespace Assets.Scripts
{
	public class ClickManager : MonoBehaviour
	{
		private Player player;
		private Vector3 hitpoint;
		private Camera camera;

	

		void Update()
		{
			MouseMovement(Input.mousePosition);
		}

		private void MouseMovement(Vector3 mousePosition)
		{
			Ray ray = GameManager.Instance.activeCamera.camera.ScreenPointToRay(mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hit))
			{
				LandMarkObject obj = hit.rigidbody?.gameObject?.GetComponent<LandMarkObject>();
				if (obj == null)
					return;

				if (Input.GetMouseButtonDown(0))
					GameManager.Instance.player.MoveTo(hit.point);
			}
		}

		public void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(hitpoint, 0.3f);
		}
	}
}