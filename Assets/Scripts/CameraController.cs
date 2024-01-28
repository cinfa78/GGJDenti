using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraController : MonoBehaviour
{
	[SerializeField] private float focalLength;
	[SerializeField] private float zommedFocalLength;
	[SerializeField] private float startingSequenceDuration;

	private Camera camera;

	private void Awake()
	{
		camera = this.GetComponent<Camera>();
	}

	public void Shake()
	{
		var startingRotation = transform.localRotation;

		DOTween.Shake(() => transform.localRotation.eulerAngles,
					x => transform.localRotation = Quaternion.Euler(x),
					0.69f,
					new Vector3(Random.value, Random.value, Random.value))
				.OnComplete(() => transform.DORotate(startingRotation.eulerAngles, 0.4f));
	}


	public void StartingSequence()
	{
		DOTween.To(() => camera.focalLength, x => camera.focalLength = x, focalLength, startingSequenceDuration);
	}
}