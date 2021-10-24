using System;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Amatrice
{
	[Serializable]
	public class Recipe
	{
		public RecipeDefinition definition;
		public int stepIndex = 0;

		public event Action StepOk;
		public event Action StepError;
		public event Action Finished;
		public event Action Failed;

		public void AddIngredient(Ingredient ingredient, Pot pot)
		{
			var currentStep = definition.recipe[stepIndex];

			if (currentStep.ingredient == ingredient.type && currentStep.pot == pot.definition.type)
			{
				Debug.Log("StepCorretto");
				stepIndex++;
				if (stepIndex >= definition.recipe.Count)
					Finished?.Invoke();

				StepOk?.Invoke();
			}
			else
			{
				Debug.Log("StepErrato");
				StepError?.Invoke();
			}
		}
	}
}