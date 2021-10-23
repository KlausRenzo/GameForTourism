using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
	public Camera camera;
	public LayerMask mapLayerMask;
	public Player player;

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

		if (Physics.Raycast(ray, out RaycastHit hit, mapLayerMask))
		{
			hitpoint = hit.point;
		}

		Debug.DrawRay(ray.origin, ray.direction * 1000);
	}

	private void Click(Vector3 mousePosition)
	{
		Ray ray = camera.ScreenPointToRay(mousePosition);
		if (Physics.Raycast(ray, out RaycastHit hit, mapLayerMask))
		{
			player.MoveTo(hit.point);
		}
	}

	public void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(hitpoint, 0.3f);
	}
}