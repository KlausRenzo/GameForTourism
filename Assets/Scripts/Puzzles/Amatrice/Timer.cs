using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Amatrice
{
	public class Timer : Draggable
	{
		public TextMesh timeText;
		private Coroutine timerCoroutine;
		private int currentTime;
		private bool timeReduction;
		private int timeReductionAmount;

		public event Action timeFinished;

		protected override void Start()
		{
			base.Start();
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
			var fasterWait = new WaitForSeconds(0.1f);

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

					timeReduction = false;
				}

				yield return wait;
				currentTime--;
				FormatTimer(currentTime);
			}

			timeFinished?.Invoke();
		}

		public void RemoveTime(int seconds)
		{
			timeReduction = true;
			timeReductionAmount = seconds;
		}

		private void FormatTimer(int seconds)
		{
			var timeSpan = TimeSpan.FromSeconds(seconds);

			timeText.text = timeSpan.ToString(@"mm\:ss");
		}
	}
}