using UnityEngine;

namespace Assets.Scripts
{
	public class LandMarkObject : MonoBehaviour
	{
		public LandMark info;
		private TextMesh text;

		// Start is called before the first frame update
		void Awake()
		{
			text = this.GetComponentInChildren<TextMesh>();
		}

		void Start()
		{
			text.text = info.name;
		}

		// Update is called once per frame
		void Update()
		{
			var camera = Camera.main;

			text.transform.LookAt(camera.transform);
			
			
		}

		public void MouseEnter()
		{

		}

		public void MouseExit()
		{

		}
	}
}