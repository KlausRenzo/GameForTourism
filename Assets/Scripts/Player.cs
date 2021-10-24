using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts
{
	public class Player : MonoBehaviour
	{
		private new Rigidbody rigidbody;
		private NavMeshAgent agent;

		void Awake()
		{
			rigidbody = this.GetComponent<Rigidbody>();
			agent = this.GetComponent<NavMeshAgent>();
		}

		void Start()
		{
			GameManager.Instance.player = this;
		}

		public void MoveTo(Vector3 position)
		{
			agent.SetDestination(position);
		}
	}
}