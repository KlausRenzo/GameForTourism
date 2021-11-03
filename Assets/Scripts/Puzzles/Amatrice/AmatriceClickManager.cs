using System.Linq;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Amatrice
{
	public class AmatriceClickManager : MonoBehaviour
	{
		public Camera camera;
		private Vector3 hitpoint;
		public LayerMask tableLayerMask;
		public Timer timer;

		[SerializeField] private Draggable activeIngredientObject;

		void Awake()
		{
		}

		void Update()
		{
			MouseMovement();
		}

		public Vector3 tablePoint;

		private void MouseMovement()
		{
			if (Input.GetMouseButtonUp(0) && activeIngredientObject != null)
			{
				activeIngredientObject.StopDrag();
				activeIngredientObject = null;
			}

			var position = Input.mousePosition;
			Ray ray = camera.ScreenPointToRay(position);

			if (Physics.Raycast(ray, out RaycastHit tableHit, Mathf.Infinity, tableLayerMask.value))
			{
				tablePoint = tableHit.point;
			}

			// IngredientObject
			if (Physics.Raycast(ray, out RaycastHit hit))
			{
				Draggable obj = hit.transform.gameObject?.GetComponent<Draggable>();
				var pot = hit.transform.gameObject.gameObject?.GetComponent<Pot>();
				if (pot != null)
				{
					Debug.Log(pot);
					if (Input.GetMouseButtonDown(0))
					{
						if (!pot.canGetIngredient || pot.ingredient == null)
						{
							return;
						}

						pot.canGetIngredient = false;

						var go = Instantiate(pot.ingredient, pot.transform.position, Quaternion.identity);
						activeIngredientObject = go.GetComponent<IngredientObject>();
						
						activeIngredientObject.StartDrag();
						pot.GetComponent<Animator>().SetTrigger("Next");
					}
				}
				else if (obj != null)
				{
					hitpoint = hit.point;

					if (Input.GetMouseButtonDown(0))
					{
						activeIngredientObject = obj;
						activeIngredientObject.StartDrag();
					}
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