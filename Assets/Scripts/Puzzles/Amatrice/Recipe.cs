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

	}

	public enum IngredientResult
	{
		Corret,
		Bad,
		VeryBad,
		UltraBad
	}
}