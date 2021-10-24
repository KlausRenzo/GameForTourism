using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class GameUiManager : MonoBehaviour
	{
		public GameObject rewardPrefab;
		public GameObject rewardPanel;
		public GameObject comingSoonPanel;
		public Image comingSoonIcon;
		public TMP_Text comingSoonText;

		public void Start()
		{
			GameManager.Instance.gameUiManager = this;
		}

		public void ShowReward(RewardDefinition infoPuzzleReward)
		{
			var newReward = Instantiate(rewardPrefab, rewardPanel.transform);

			newReward.GetComponent<Reward>().Show(infoPuzzleReward);
		}

		public void ShowComingSoon(LandMark landMark)
		{
			comingSoonIcon.sprite = landMark.icon;

			comingSoonText.text = landMark.name;
			comingSoonPanel.SetActive(true);
		}

		public void CloseComingSoon()
		{
			comingSoonPanel.SetActive(false);
		}
	}
}