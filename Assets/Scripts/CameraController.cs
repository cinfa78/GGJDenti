using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private float focalLength;
	[SerializeField] private float startingSequenceDuration;

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
		var camera = this.GetComponent<Camera>();

		DOTween.To(() => camera.focalLength, x => camera.focalLength = x, focalLength, startingSequenceDuration);
	}
}