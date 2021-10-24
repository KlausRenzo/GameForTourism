using UnityEngine;

namespace Assets.Scripts.Puzzles.Amatrice
{
	public class Draggable : MonoBehaviour
	{
		private bool isDragged;
		public new Rigidbody rigidbody;
		private AmatriceClickManager clickManager;

		private float hoverY;
		private Vector3 originalPosition;

		[Range(0, 2)] [SerializeField] private float hoverHeight = 0.5f;

		[Range(0, 20)] [SerializeField] private float yeetSpeed = 5;

		protected virtual void Start()
		{
			clickManager = FindObjectOfType<AmatriceClickManager>();
			rigidbody = this.GetComponent<Rigidbody>();
			originalPosition = this.transform.position;
			hoverY = originalPosition.y + hoverHeight;
		}

		protected virtual void Update()
		{
			if (!isDragged)
				return;

			var tablePosition = clickManager.tablePoint;
			tablePosition.y = hoverY;

			this.transform.position = tablePosition;

			velocity = (tablePosition - previousPosition) * Time.deltaTime;
			previousPosition = tablePosition;
		}

		public virtual void StartDrag()
		{
			isDragged = true;
			rigidbody.useGravity = false;
		}

		public Vector3 previousPosition;
		public Vector3 velocity;

		public virtual void StopDrag()
		{
			isDragged = false;
			rigidbody.useGravity = true;

			Debug.Log(velocity * yeetSpeed);
			rigidbody.velocity = velocity * yeetSpeed * 10;
		}
	}
}