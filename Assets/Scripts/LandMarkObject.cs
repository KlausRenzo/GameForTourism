using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
	public class LandMarkObject : MonoBehaviour
	{
		private TextMesh text;
		private Animator animator;

		public LandMark info;
		public Material completeMaterial;

		// Start is called before the first frame update
		void Awake()
		{
			text = this.GetComponentInChildren<TextMesh>();
			animator = this.GetComponent<Animator>();
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

			
			//Debug
			if(Input.GetKeyDown(KeyCode.H))
				SetComplete();
		}

		public void OnMouseEnter()
		{
			animator.SetBool("Highlight", true);
		}
		
		public void OnMouseExit()
		{
			animator.SetBool("Highlight", false);
		}

		public void OnTriggerEnter(Collider collider)
		{
			if (isComplete)
			{
				return;
			}

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