using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Amatrice
{
	public class Ricettario : IngredientObject
	{
		public GameObject ricettarioModel;
		public Vector3 ricettarioPosition;
		public Vector3 ricettarioScale;
		public Quaternion ricettarioRotation;

		public AnimationCurve curve;

		private Coroutine coroutine;
		private MeshRenderer renderer;

		private void Awake()
		{
			ricettarioPosition = ricettarioModel.transform.position;
			ricettarioScale = ricettarioModel.transform.localScale;
			ricettarioRotation = ricettarioModel.transform.rotation;

			ricettarioModel.transform.position = this.transform.position;
			ricettarioModel.transform.localScale = transform.localScale;
			ricettarioModel.transform.rotation = transform.rotation;

			renderer = this.GetComponent<MeshRenderer>();
			renderer.enabled = false;
		}

		public override void StartDrag()
		{
			if (coroutine != null)
				StopCoroutine(coroutine);
			coroutine = StartCoroutine(ShowCoroutine());
		}

		private IEnumerator ShowCoroutine()
		{
			
			ricettarioModel.SetActive(true);

			ricettarioModel.transform.position = this.transform.position;
			ricettarioModel.transform.localScale = this.transform.localScale;


			var startTime = Time.time;

			while (Time.time < startTime + duration)
			{
				var value = curve.Evaluate((Time.time - startTime) / duration);

				ricettarioModel.transform.position = Vector3.Lerp(ricettarioModel.transform.position, ricettarioPosition, value);
				ricettarioModel.transform.localScale = Vector3.Lerp(ricettarioModel.transform.localScale, ricettarioScale, value);
				ricettarioModel.transform.rotation = Quaternion.Lerp(ricettarioModel.transform.rotation, ricettarioRotation, value);

				yield return null;
			}
		}

		public float duration;
		private IEnumerator HideCoroutine()
		{
			var startTime = Time.time;

			while (Time.time < startTime + duration)
			{
				var value = curve.Evaluate((Time.time - startTime) / duration);

				ricettarioModel.transform.position = Vector3.Lerp(ricettarioModel.transform.position, transform.position, value);
				ricettarioModel.transform.localScale = Vector3.Lerp(ricettarioModel.transform.localScale, transform.localScale, value);
				ricettarioModel.transform.rotation = Quaternion.Lerp(ricettarioModel.transform.rotation, transform.rotation, value);

				yield return null;
			}

			ricettarioModel.transform.position = ricettarioPosition;
			ricettarioModel.transform.localScale = ricettarioScale;
			ricettarioModel.transform.rotation = ricettarioRotation;
			ricettarioModel.SetActive(false);
		}

		public override void StopDrag()
		{
			if (coroutine != null)
				StopCoroutine(coroutine);
			coroutine = StartCoroutine(HideCoroutine());
		}
	}
}