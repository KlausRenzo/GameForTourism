using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Amatrice
{
	public class RecipeManager : MonoBehaviour
	{
		public Recipe recipe;
		public PuzzleDefinition puzzle;

		public List<Ingredient> ingredients = new List<Ingredient>();
		public UiManager uiManager;

		public void Start()
		{
			recipe.StepOk += Recipe_StepOk;
			recipe.StepError += RecipeOnStepError;

			recipe.Finished += RecipeOnFinished;
			recipe.Failed += RecipeOnFailed;

			uiManager.puzzle = puzzle;
		}

		private void RecipeOnFailed()
		{
		}

		private void RecipeOnStepError()
		{
		}

		private void Recipe_StepOk()
		{
		}

		private void RecipeOnFinished()
		{
			uiManager.ShowSuccess();
		}

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