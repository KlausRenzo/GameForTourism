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

		public void LoadPuzzle(PuzzleDefinition infoPuzzle)
		{
			if (infoPuzzle == null)
				return;

			playerPosition = player.transform.position;

			SceneManager.LoadScene(infoPuzzle.sceneName);
		}

		public void PuzzleEnded(PuzzleDefinition infoPuzzle, bool status)
		{
			StartCoroutine(PuzzleEndedCoroutine(infoPuzzle, status));
		}

		private IEnumerator PuzzleEndedCoroutine(PuzzleDefinition infoPuzzle, bool status)
		{
			var asyncOperation = SceneManager.LoadSceneAsync("Map");
			yield return new WaitUntil(() => asyncOperation.isDone);

			var landmark = landmarks.First(x => x.info == infoPuzzle.landMark);
			var landmarkPosition = landmark.transform.position;
			player.transform.position = landmarkPosition + (playerPosition - landmarkPosition) * 2;

			if (status)
			{
				landmark.SetComplete();
				ShowReward(infoPuzzle.reward);
			}
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

		public void LoadComingSoon(LandMark landMark)
		{
			gameUiManager.ShowComingSoon(landMark);
		}
	}
}