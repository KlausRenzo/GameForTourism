using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts
{
	public class Player : MonoBehaviour
	{
		[Range(1f, 25f)] public float speed = 10f;

		private new Rigidbody rigidbody;
		private NavMeshAgent agent;

		// Start is called before the first frame update
		void Awake()
		{
			rigidbody = this.GetComponent<Rigidbody>();
			agent = this.GetComponent<NavMeshAgent>();
		}

		// Update is called once per frame
		void Update()
		{
			//Movement();
		}

		//private void Movement()
		//{
		//	float horizontal = Input.GetAxis("Horizontal");
		//	float vertical = Input.GetAxis("Vertical");

		//	Vector3 delta = new Vector3(vertical, 0, -horizontal) * speed;
		//	Vector3 newPosition = rigidbody.position + delta;

		//	rigidbody.velocity = delta;
		//}

		public void MoveTo(Vector3 position)
		{
			agent.SetDestination(position);
		}
	}
}