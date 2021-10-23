using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Assets.Scripts;
using Assets.Scripts.Puzzles;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public Player player;
	public List<LandMarkObject> landmarks = new List<LandMarkObject>();

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

	public PuzzleDefinition puzzle;

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.P))
			LoadPuzzle(puzzle);

		if (Input.GetKeyDown(KeyCode.S))
			PuzzleSuccess(puzzle);
	}

	private Vector3 playerPosition;

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
		SceneManager.LoadScene("Map");
		yield return new WaitForSeconds(0.2f);

		player.transform.position = playerPosition;

		var landmark = landmarks.First(x => x.info == infoPuzzle.landMark);
		landmark.SetComplete();

		ShowReward(infoPuzzle.reward);
	}

	private void ShowReward(object infoPuzzleReward)
	{
		
	}

	public void RegisterLandmark(LandMarkObject landmark)
	{
		if (landmarks.Contains(landmark))
			return;

		landmarks.Add(landmark);
	}
}