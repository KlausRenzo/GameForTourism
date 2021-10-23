using System.Threading;
using Assets.Scripts;
using UnityEngine;

public class GameUiManager : MonoBehaviour
{
	public GameObject rewardPrefab;
	public GameObject rewardPanel;

	public void Start()
	{
		GameManager.Instance.gameUiManager = this;
	}

	public void ShowReward(RewardDefinition infoPuzzleReward)
	{
		var newReward = Instantiate(rewardPrefab, rewardPanel.transform);

		newReward.GetComponent<Reward>().Show(infoPuzzleReward);
	}
}