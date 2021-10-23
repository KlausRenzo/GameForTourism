using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Amatrice
{
	public class RecipeManager : MonoBehaviour
	{
		public Recipe recipe;

		public List<Ingredient> ingredients = new List<Ingredient>();

		public void AddIngredient(Ingredient ingredient, Pot pot)
		{
			ingredients.Add(ingredient);
			recipe.AddIngredient(ingredient, pot);
		}

		public void RemoveIngredient(Ingredient ingredient, Pot pot)
		{
			ingredients.Remove(ingredient);
		}
	}
}