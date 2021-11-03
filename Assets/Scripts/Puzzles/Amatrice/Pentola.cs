using UnityEngine;

namespace Assets.Scripts.Puzzles.Amatrice
{
	public class Pentola : Pot
	{
		public void AddSpaghetti()
		{
			GetComponent<Animator>().SetTrigger("Next");
			canGetIngredient = true;
		}

		public void AddWater()
		{
			GetComponent<Animator>().SetTrigger("Next");
		}
	}
}