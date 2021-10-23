using UnityEngine;

namespace Assets.Scripts
{
	[CreateAssetMenu(fileName = "New Reward", menuName = "GameForTourism/Reward", order = 6)]
	public class RewardDefinition : ScriptableObject
	{
		public string name;
		public string description;
		public Sprite sprite;
	}
}