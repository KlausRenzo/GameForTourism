using System;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Puzzles.Amatrice
{
	public class Pot : MonoBehaviour
	{
		public PotDefinition definition;

		public RecipeManager recipeManager;

		public void AddIngredient(IngredientObject ingredient)
		{
			var result = recipeManager.AddIngredient(ingredient.ingredient, this);

			switch (result)
			{
				case IngredientResult.Corret:
					ingredient.gameObject.SetActive(false);
					break;
				case IngredientResult.Bad:
					ingredient.rigidbody.velocity = (transform.up + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f))) * 10;
					break;
				case IngredientResult.VeryBad:
					break;
				case IngredientResult.UltraBad:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public bool canGetIngredient;
		public GameObject ingredient;

		public void OnTriggerEnter(Collider collider)
		{
			var ingredientObject = collider.gameObject.GetComponent<IngredientObject>();

			if (ingredientObject == null)
				return;

			AddIngredient(ingredientObject);
		}

		public void OnDrawGizmos()
		{
			Gizmos.DrawMesh(this.GetComponent<MeshFilter>().sharedMesh, transform.position, transform.rotation);
		}

		public void EnableAmatriciana()
		{
			canGetIngredient = true;
		}
	}
}