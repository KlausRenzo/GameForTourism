using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditorInternal;
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

		public List<RecipeStep> steps;

		[Header("Pots")]
		public Pentola pentola;
		public Pot padella;
		public Pot piatto;

		[Header("Ingredients")] 
		public IngredientObject spaghetti;
		public IngredientObject jug;


		private int stepIndex;

		public void Start()
		{
			uiManager.puzzle = puzzle;
			CreateRecipe();
		}

		private void CreateRecipe()
		{
			steps = new List<RecipeStep>
			{
				new RecipeStep(IngredientType.Acqua, PotType.Pentola)
				{
					OnSuccess = SetWaterPot
				},
				new RecipeStep(IngredientType.Spaghetti, PotType.Pentola)
				{
					OnSuccess = SetSpaghettiBox
				},
				new RecipeStep(IngredientType.Guanciale, PotType.Padella),
				new RecipeStep(IngredientType.Vino, PotType.Padella),
				new RecipeStep(IngredientType.Pomodoro, PotType.Padella),
				new RecipeStep(IngredientType.SpaghettiCotti, PotType.Padella)
				{
					OnSuccess = SetPadellaSpaghetti
				},
				new RecipeStep(IngredientType.Pecorino, PotType.Padella),
				new RecipeStep(IngredientType.Matriciana, PotType.Piatto),
			};
		}

		private void SetPadellaSpaghetti()
		{
			padella.GetComponent<Animator>().SetTrigger("Next");
		}

		private void SetSpaghettiBox()
		{
			spaghetti = Instantiate(spaghetti, spaghetti.originalPosition, spaghetti.originalRotation);
			spaghetti.GetComponent<Animator>().SetTrigger("Next");
		}

		private void SetWaterPot()
		{
			pentola.SetWaterPot();

			jug = Instantiate(jug, jug.originalPosition, jug.originalRotation);
			jug.GetComponent<Animator>().SetTrigger("Next");

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

			var result = CheckStep(ingredient, pot);

			if (result == IngredientResult.Bad)
			{
				timer.RemoveTime(3);
			}

			return result;
		}

		private IngredientResult CheckStep(Ingredient ingredient, Pot pot)
		{
			var currentStep = steps[stepIndex];

			if (currentStep.ingredientType == ingredient.type && currentStep.potType == pot.definition.type)
			{
				stepIndex++;
				if (stepIndex >= steps.Count)
				{
					RecipeOnFinished();
					return IngredientResult.Corret;
				}

				currentStep.OnSuccess();
				return IngredientResult.Corret;
			}
			else
			{
				return IngredientResult.Bad;
			}
		}


		public void RemoveIngredient(Ingredient ingredient, Pot pot)
		{
			ingredients.Remove(ingredient);
		}
	}

	public class RecipeStep
	{
		public IngredientType ingredientType;
		public PotType potType;

		public Action OnSuccess;

		public RecipeStep(IngredientType ingredientType, PotType potType)
		{
			this.ingredientType = ingredientType;
			this.potType = potType;
		}
	}
}