using UnityEngine;

namespace Assets.Scripts.Puzzles.Amatrice
{
	[CreateAssetMenu(fileName = "New Ingredient", menuName = "GameForTourism/Ingredient", order = 1)]
	public class Ingredient : ScriptableObject
	{
		public IngredientType type;
	}
}