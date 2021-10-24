using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Amatrice
{
	public class Timer : MonoBehaviour
	{
		public TextMesh timeText;
		private Coroutine timerCoroutine;
		private int currentTime;

		public event Action timeFinished;

		public void Start()
		{
			StartTimer(120);
		}
		public void StartTimer(int seconds)
		{
			currentTime = seconds;

			if(timerCoroutine != null)
				StopCoroutine(timerCoroutine);

			timerCoroutine = StartCoroutine(TimerCoroutine());
		}

		private IEnumerator TimerCoroutine()
		{
			var wait = new WaitForSeconds(1);

			while (currentTime > 0)
			{
				yield return wait;
				currentTime--;
				FormatTimer(currentTime);
			}

			timeFinished?.Invoke();
		}

		private bool timeReduction;
		private int timeReductionAmount;

		public void RemoveTime(int seconds)
		{
			currentTime -= seconds;
		}

		private void FormatTimer(int seconds)
		{
			var timeSpan = TimeSpan.FromSeconds(seconds);

			timeText.text = timeSpan.ToString(@"mm\:ss");
		}
	}
}