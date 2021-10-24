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

			if (timerCoroutine != null)
				StopCoroutine(timerCoroutine);

			timerCoroutine = StartCoroutine(TimerCoroutine());
		}

		private IEnumerator TimerCoroutine()
		{
			var wait = new WaitForSeconds(1);
			var fasterWait = new WaitForSeconds(0.15f);

			while (currentTime > 0)
			{
				if (timeReduction)
				{
					for (int i = 0; i < timeReductionAmount; i++)
					{
						yield return fasterWait;
						currentTime--;
						FormatTimer(currentTime);
					}
				}

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