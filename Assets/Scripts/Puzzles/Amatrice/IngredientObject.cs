using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Puzzles.Amatrice
{
	public class IngredientObject : Draggable
	{
		public Ingredient ingredient;

		[HideInInspector] public Vector3 originalScale;
		[HideInInspector] public Quaternion originalRotation;

		protected override void Start()
		{
			base.Start();
			originalPosition = this.transform.position;
			originalScale = this.transform.localScale;
			originalRotation = this.transform.rotation;
		}
	}
}