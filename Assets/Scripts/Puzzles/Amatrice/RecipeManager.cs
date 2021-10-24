using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Amatrice
{
	public class RecipeManager : MonoBehaviour
	{
		public Recipe recipe;
		public PuzzleDefinition puzzle;

		public List<Ingredient> ingredients = new List<Ingredient>();
		public UiManager uiManager;
		public Timer timer;

		public void Start()
		{
			recipe.Finished += RecipeOnFinished;
			recipe.Failed += RecipeOnFailed;

			uiManager.puzzle = puzzle;
		}

		private void RecipeOnFailed()
		{
		}

		private void RecipeOnFinished()
		{
			uiManager.ShowSuccess();
		}

		public IngredientResult AddIngredient(Ingredient ingredient, Pot pot)
		{
			ingredients.Add(ingredient);
			var result = recipe.AddIngredient(ingredient, pot);

			if (result == IngredientResult.Bad)
			{
				timer.RemoveTime(3);
			}
			return result;
		}

		public void RemoveIngredient(Ingredient ingredient, Pot pot)
		{
			ingredients.Remove(ingredient);
		}
	}
}