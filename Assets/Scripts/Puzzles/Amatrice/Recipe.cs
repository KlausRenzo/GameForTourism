using System;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Amatrice
{
	[Serializable]
	public class Recipe
	{
		public RecipeDefinition definition;
		public int stepIndex = 0;

		public void AddIngredient(Ingredient ingredient, Pot pot)
		{
			var currentStep = definition.recipe[stepIndex];

			if (currentStep.ingredient == ingredient.type && currentStep.pot == pot.definition.type)
			{
				Debug.Log("StepCorretto");
				stepIndex++;
			}
			else
			{
				Debug.Log("StepErrato");
			}

		}
	}
}