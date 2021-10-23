using UnityEngine;

namespace Assets.Scripts.Puzzles.Amatrice
{
	public class Draggable : MonoBehaviour
	{
		private bool isDragged;
		private new Rigidbody rigidbody;

		private AmatriceClickManager clickManager;

		private float hoverY;
		private Vector3 originalPosition;

		public Ingredient ingredient;
		[Range(0, 2)] [SerializeField] private float hoverHeight = 0.5f;

		// Start is called before the first frame update
		void Start()
		{
			clickManager = FindObjectOfType<AmatriceClickManager>();
			rigidbody = this.GetComponent<Rigidbody>();
			originalPosition = this.transform.position;
			hoverY = originalPosition.y + hoverHeight;
		}

		// Update is called once per frame
		void Update()
		{
			if (!isDragged)
				return;


			var tablePosition = clickManager.tablePoint;
			tablePosition.y = hoverY;

			this.transform.position = tablePosition;
		}

		//public void OnMouseEnter()
		//{
		//	transform.position += Vector3.up;
		//}


		public void StartDrag()
		{
			isDragged = true;

			rigidbody.isKinematic = true;
		}

		public void StopDrag()
		{
			isDragged = false;
			rigidbody.isKinematic = false;
		}
	}
}