using UnityEngine;

namespace Assets.Scripts.Puzzles.Amatrice
{
	[CreateAssetMenu(fileName = "New Pot", menuName = "GameForTourism/Pot", order = 2)]
	public class PotDefinition : ScriptableObject
	{
		public PotType type;
	}
}