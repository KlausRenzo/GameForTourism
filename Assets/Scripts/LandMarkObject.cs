using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
	public class LandMarkObject : MonoBehaviour
	{
		public LandMark info;
		private TextMesh text;
		public Material completeMaterial;

		// Start is called before the first frame update
		void Awake()
		{
			text = this.GetComponentInChildren<TextMesh>();
		}

		void Start()
		{
			text.text = info.name;

			GameManager.Instance.RegisterLandmark(this);
		}

		// Update is called once per frame
		void Update()
		{
			var camera = Camera.main;

			text.transform.LookAt(camera.transform);

			if(Input.GetKeyDown(KeyCode.H))
				SetComplete();
		}

		public void MouseEnter()
		{

		}

		public void MouseExit()
		{

		}

		public void OnTriggerEnter(Collider collider)
		{
			if (collider.gameObject.GetComponent<Player>())
			{
				GameManager.Instance.LoadPuzzle(info.puzzle);
			}

		}

		public bool isComplete;
		
		public void SetComplete()
		{
			isComplete = true;

			this.GetComponent<MeshRenderer>().material = completeMaterial;
		}
	}
}