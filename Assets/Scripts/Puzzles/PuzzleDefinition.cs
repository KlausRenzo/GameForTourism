using UnityEditor.SearchService;
using UnityEngine;

namespace Assets.Scripts.Puzzles
{
	[CreateAssetMenu(fileName = "New Puzzle", menuName = "GameForTourism/Puzzle", order = 5)]

	public class PuzzleDefinition : ScriptableObject
	{
		public LandMark landMark;
		public string sceneName;
		public object reward;
	}
}