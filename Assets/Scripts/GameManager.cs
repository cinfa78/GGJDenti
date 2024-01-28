using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
	public ToothStarter upperTeeth;
	public ToothStarter lowerTeeth;

	private List<(ToothController, ToothController)> couples = new();
	[SerializeField] private Volume volume;

	[SerializeField] private CameraController camera;
	private ChromaticAberration chromaticAberration;

	private void Awake()
	{
		for (int i = 0; i < upperTeeth.teeth.Length; i++)
		{
			var upperTooth = upperTeeth.teeth[i];
			var lowerTooth = lowerTeeth.teeth[i];

			couples.Add((upperTooth, lowerTooth));
		}

		ToothController.Moved += OnToothMoved;

		chromaticAberration = volume.profile.components.OfType<ChromaticAberration>().First();
	}

	private void Start()
	{
		ServiceLocator.sfxController.enabled = true;
	}

	private void OnToothMoved()
	{
		FX();

		CheckWin();
	}

	private void FX()
	{
		// ChromaticAberration
		DOTween.To(() => chromaticAberration.intensity.value, x => chromaticAberration.intensity.value = x, 1f, 0.3f)
				.OnComplete(() => DOTween.To(() => chromaticAberration.intensity.value, x => chromaticAberration.intensity.value = x, 0, 0.3f));

		camera.Shake();
	}

	private void CheckWin()
	{
		var hasWon = couples.All(x => (x.Item1 == x.Item2));
		if (hasWon)
			StartCoroutine(WinSequence());
	}

	private IEnumerator WinSequence()
	{
		yield return null;
	}

	[Button]
	private void Align()
	{
		for (int i = 0; i < upperTeeth.teeth.Length; i++)
		{
			ToothController upperTooth = upperTeeth.teeth[i];
			var lowerTooth = lowerTeeth.teeth[i];
			Debug.Log(lowerTooth.name);

			var linkedNames = lowerTooth.connectedTooths.Select(x => x.name);

			Debug.Log(linkedNames.Aggregate((a, b) => a + ", " + b));
			var upperLinkedNames = linkedNames.Select(x => x.Replace("Lower", "Upper"));
			Debug.Log(upperLinkedNames.Aggregate((a, b) => a + ", " + b));

			upperTooth.connectedTooths = upperLinkedNames.Select(x => GameObject.Find(x).GetComponent<ToothController>()).ToArray();
		}
	}

	#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		foreach ((ToothController upper, ToothController lower) tuple in couples)
		{
			Handles.color = tuple.upper.flag == tuple.lower.flag ? Color.green : Color.red;

			Handles.DrawDottedLine(tuple.upper.transform.position, tuple.lower.transform.position, 5);
		}
	}
	#endif
}