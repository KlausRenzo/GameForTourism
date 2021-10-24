using UnityEngine;

namespace Assets.Scripts.Puzzles.Amatrice
{
	public class Pot : MonoBehaviour
	{
		public PotDefinition definition;

		public RecipeManager recipeManager;

		public void AddIngredient(Ingredient ingredient)
		{
			recipeManager.AddIngredient(ingredient, this);
		}

		public void RemoveIngredient(Ingredient ingredient)
		{
			recipeManager.RemoveIngredient(ingredient, this);
		}


		public void OnTriggerEnter(Collider collider)
		{
			var ingredientObject = collider.gameObject.GetComponent<IngredientObject>();

			if (ingredientObject == null)
				return;

			AddIngredient(ingredientObject.ingredient);
		}

		public void OnTriggerExit(Collider collider)
		{
			var ingredientObject = collider.gameObject.GetComponent<IngredientObject>();

			if (ingredientObject == null)
				return;

			RemoveIngredient(ingredientObject.ingredient);
		}

		public void OnDrawGizmos()
		{
			Gizmos.DrawMesh(this.GetComponent<MeshFilter>().mesh, transform.position, transform.rotation);
		}
	}
}