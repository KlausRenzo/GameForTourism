using UnityEngine;

namespace Assets.Scripts
{
	public class ActiveCamera : MonoBehaviour
	{
		[HideInInspector] public Camera camera;

		void Awake()
		{
			camera = this.GetComponent<Camera>();
		}

		void Start()
		{
			GameManager.Instance.activeCamera = this;
		}
	}
}