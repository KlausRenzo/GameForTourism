using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Puzzles;
using UnityEngine;

namespace Assets.Scripts
{
	[CreateAssetMenu(fileName = "New Landmark", menuName = "GameForTourism/Landmark", order = 0)]
	public class LandMark : ScriptableObject
	{
		public string name;
		public Sprite icon, finishedIcon;
		public PuzzleDefinition puzzle;
	}
}