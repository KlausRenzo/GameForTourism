using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class Reward : MonoBehaviour
	{
		public TMP_Text title;
		public TMP_Text description;
		public Image image;

		public void Show(RewardDefinition definition)
		{
			title.text = definition.name;
			description.text = definition.description;

			image.sprite = definition.sprite;
		}

		public void Hide()
		{
			Destroy(this.gameObject);
		}
	}
}