using UnityEngine;

namespace Assets.Scripts.Puzzles.Amatrice
{
	[CreateAssetMenu(fileName = "New Recipe", menuName = "GameForTourism/Recipe", order = 3)]
	public class RecipeDefinition : ScriptableObject
	{
		public string name;
		public int timeInSecond;
	}
}