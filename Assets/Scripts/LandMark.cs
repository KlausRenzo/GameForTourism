using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	[CreateAssetMenu(fileName = "New Landmark", menuName = "GameForTourism/Landmark", order = 0)]
	public class LandMark : ScriptableObject
	{
		public string name;
	}
}