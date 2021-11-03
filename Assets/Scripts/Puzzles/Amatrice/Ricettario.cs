using System.Collections;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts.Puzzles.Amatrice
{
	public class Ricettario : Draggable
	{
		public GameObject ricettarioModel;
		public Vector3 ricettarioPosition;
		public Vector3 ricettarioScale;
		public Quaternion ricettarioRotation;
		public Camera camera;

		public AnimationCurve curve;

		private Coroutine coroutine;
		public float duration;
		private MeshRenderer renderer;

		private void Awake()
		{
			ricettarioPosition = ricettarioModel.transform.position;
			ricettarioScale = ricettarioModel.transform.localScale;
			ricettarioRotation = ricettarioModel.transform.rotation;

			ricettarioModel.transform.position = transform.position;
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

			var startTime = Time.time;

			while (Time.time < startTime + duration)
			{
				var value = curve.Evaluate((Time.time - startTime) / duration);

				ricettarioModel.transform.position = Vector3.Lerp(transform.position, ricettarioPosition, value);
				ricettarioModel.transform.localScale = Vector3.Lerp(transform.localScale, ricettarioScale, value);
				ricettarioModel.transform.rotation = Quaternion.Lerp(transform.rotation, ricettarioRotation, value);

				yield return null;
			}
		}

		private IEnumerator HideCoroutine()
		{
			var startTime = Time.time;

			while (Time.time < startTime + duration)
			{
				var value = curve.Evaluate((Time.time - startTime) / duration);

				ricettarioModel.transform.position = Vector3.Lerp(ricettarioPosition, transform.position, value);
				ricettarioModel.transform.localScale = Vector3.Lerp(ricettarioScale, transform.localScale, value);
				ricettarioModel.transform.rotation = Quaternion.Lerp(ricettarioRotation, transform.rotation, value);

				yield return null;
			}
		}

		public override void StopDrag()
		{
			if (coroutine != null)
				StopCoroutine(coroutine);

			coroutine = StartCoroutine(HideCoroutine());
		}
	}
}