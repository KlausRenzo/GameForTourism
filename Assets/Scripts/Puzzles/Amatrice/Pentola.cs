using UnityEngine;

namespace Assets.Scripts.Puzzles.Amatrice
{
	public class Pentola : Pot
	{
		public GameObject waterModel;
		public void SetWaterPot()
		{
			waterModel.SetActive(true);
		}
	}
}