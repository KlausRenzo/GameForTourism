using System;
using System.Collections;
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
		public Timer timer;

		public List<RecipeStep> steps;

		[Header("Pots")] public Pentola pentola;
		public Pot padella;
		public Pot piatto;

		[Header("Ingredients")] public IngredientObject spaghetti;
		public IngredientObject jug;


		private int stepIndex;

		public void Start()
		{
			uiManager.puzzle = puzzle;
			timer.timeFinished += TimerOntimeFinished;
			CreateRecipe();
		}

		private void TimerOntimeFinished()
		{
			StartCoroutine(LoseCoroutine());
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
				new RecipeStep(IngredientType.Guanciale, PotType.Padella)
				{
					OnSuccess = () => padella.GetComponent<Animator>().SetTrigger("Next")
				},
				new RecipeStep(IngredientType.Vino, PotType.Padella),
				new RecipeStep(IngredientType.Pomodoro, PotType.Padella)
				{
					OnSuccess = () => padella.GetComponent<Animator>().SetTrigger("Next")
				},
				new RecipeStep(IngredientType.SpaghettiCotti, PotType.Padella)
				{
					OnSuccess = () => padella.GetComponent<Animator>().SetTrigger("Next")
				},
				new RecipeStep(IngredientType.Pecorino, PotType.Padella)
				{
					OnSuccess = () => padella.EnableAmatriciana()
				},
				new RecipeStep(IngredientType.Matriciana, PotType.Piatto)
				{
					OnSuccess = () =>
					{
						piatto.GetComponent<Animator>().SetTrigger("Next");
						StartCoroutine(WinCoroutine());
					}
				},
			};
		}

		private void SetSpaghettiBox()
		{
			spaghetti = Instantiate(spaghetti, spaghetti.originalPosition, spaghetti.originalRotation);
			spaghetti.GetComponent<Animator>().SetTrigger("Next");
			pentola.AddSpaghetti();
		}

		private void SetWaterPot()
		{
			jug = Instantiate(jug, jug.originalPosition, jug.originalRotation);
			jug.GetComponent<Animator>().SetTrigger("Next");
			pentola.GetComponent<Animator>().SetTrigger("Next");
		}

		private void RecipeOnFailed()
		{
			StartCoroutine(LoseCoroutine());
		}

		public IngredientResult AddIngredient(Ingredient ingredient, Pot pot)
		{
			if (stepIndex > steps.Count)
			{
				return IngredientResult.Bad;
			}

			ingredients.Add(ingredient);

			var result = CheckStep(ingredient, pot);

			if (result == IngredientResult.Bad)
			{
				timer.RemoveTime(3);
			}


			return result;
		}

		private IEnumerator WinCoroutine()
		{
			timer.Stop();
			yield return new WaitForSeconds(1);
			uiManager.ShowSuccess();
		}

		private IEnumerator LoseCoroutine()
		{
			timer.Stop();
			yield return new WaitForSeconds(1);
			uiManager.ShowFail();
		}

		private IngredientResult CheckStep(Ingredient ingredient, Pot pot)
		{
			var currentStep = steps[stepIndex];

			if (currentStep.ingredientType == ingredient.type && currentStep.potType == pot.definition.type)
			{
				stepIndex++;


				currentStep.OnSuccess?.Invoke();
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