using UnityEngine;

namespace Assets.Scripts.Puzzles.Amatrice
{
	public class UiManager : MonoBehaviour
	{
		// Start is called before the first frame update
		void Start()
		{
			animator = GetComponent<Animator>();
		}

		// Update is called once per frame
		void Update()
		{
		}

		public GameObject victoryPannel;
		public PuzzleDefinition puzzle;
		private Animator animator;

		public void ShowSuccess()
		{
			animator.SetTrigger("Victory");
		}

		public void GoToMap()
		{
			GameManager.Instance.PuzzleSuccess(puzzle);
		}
	}
}