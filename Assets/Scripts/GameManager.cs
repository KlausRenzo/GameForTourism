using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Puzzles;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
	public class GameManager : MonoBehaviour
	{
		public Player player;
		public List<LandMarkObject> landmarks = new List<LandMarkObject>();
		public PuzzleDefinition puzzle;
		public GameUiManager gameUiManager;
		public ActiveCamera activeCamera;

		private Vector3 playerPosition;
		public static GameManager Instance { get; private set; }

		void Awake()
		{
			if (!Instance)
			{
				Instance = this;
			}
			else
			{
				Destroy(gameObject);
			}

			DontDestroyOnLoad(gameObject);
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.P))
				LoadPuzzle(puzzle);

			if (Input.GetKeyDown(KeyCode.S))
				PuzzleSuccess(puzzle);
		}

		public void LoadPuzzle(PuzzleDefinition infoPuzzle)
		{
			if (infoPuzzle == null)
				return;

			playerPosition = player.transform.position;

			SceneManager.LoadScene(infoPuzzle.sceneName);
		}

		public void PuzzleSuccess(PuzzleDefinition infoPuzzle)
		{
			StartCoroutine(PuzzleSuccessCoroutine(infoPuzzle));
		}

		private IEnumerator PuzzleSuccessCoroutine(PuzzleDefinition infoPuzzle)
		{
			var asyncOperation = SceneManager.LoadSceneAsync("Map");
			yield return new WaitUntil(() => asyncOperation.isDone);

			var landmark = landmarks.First(x => x.info == infoPuzzle.landMark);
			var landmarkPosition = landmark.transform.position;
			player.transform.position = landmarkPosition + (playerPosition - landmarkPosition) * 2;

			landmark.SetComplete();
			ShowReward(infoPuzzle.reward);
		}

		private void ShowReward(RewardDefinition infoPuzzleReward)
		{
			gameUiManager.ShowReward(infoPuzzleReward);
		}

		public void RegisterLandmark(LandMarkObject landmark)
		{
			if (landmarks.Contains(landmark))
				return;

			landmarks.Add(landmark);
		}
	}
}