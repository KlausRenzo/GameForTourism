using System.Collections;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts.Puzzles.Amatrice
{
	public class Ricettario : Draggable
	{
		public GameObject ricettarioModel;
		public Vector3 ricettarioPosition;
		public Vector3 ricettarioScale;
		public Quaternion ricettarioRotation;
		public DepthOfField depthOfField;

		public AnimationCurve curve;
		public AnimationCurve dofCurve;

		private Coroutine coroutine;
		public float duration;
		private MeshRenderer renderer;
		public PostProcessVolume postProcessVolume;

		private void Awake()
		{
			ricettarioPosition = ricettarioModel.transform.position;
			ricettarioScale = ricettarioModel.transform.localScale;
			ricettarioRotation = ricettarioModel.transform.rotation;

			ricettarioModel.transform.position = transform.position;
			ricettarioModel.transform.localScale = transform.localScale;
			ricettarioModel.transform.rotation = transform.rotation;

			renderer = GetComponent<MeshRenderer>();
			renderer.enabled = false;
			postProcessVolume.sharedProfile.TryGetSettings(out depthOfField);
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

			float startTime = Time.time;

			while (Time.time < startTime + duration)
			{
				float value = curve.Evaluate((Time.time - startTime) / duration);
				float dofValue = dofCurve.Evaluate((Time.time - startTime) / duration);

				ricettarioModel.transform.position = Vector3.Lerp(transform.position, ricettarioPosition, value);
				ricettarioModel.transform.localScale = Vector3.Lerp(transform.localScale, ricettarioScale, value);
				ricettarioModel.transform.rotation = Quaternion.Lerp(transform.rotation, ricettarioRotation, value);
				
				
				depthOfField.focalLength.Interp(0, 300f, dofValue);
				
				yield return null;
			}
		}

		private IEnumerator HideCoroutine()
		{
			float startTime = Time.time;

			while (Time.time < startTime + duration)
			{
				float value = curve.Evaluate((Time.time - startTime) / duration);
				float dofValue = dofCurve.Evaluate((Time.time - startTime) / duration);

				ricettarioModel.transform.position = Vector3.Lerp(ricettarioPosition, transform.position, value);
				ricettarioModel.transform.localScale = Vector3.Lerp(ricettarioScale, transform.localScale, value);
				ricettarioModel.transform.rotation = Quaternion.Lerp(ricettarioRotation, transform.rotation, value);

				depthOfField.focalLength.Interp(300, 0f, dofValue);

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