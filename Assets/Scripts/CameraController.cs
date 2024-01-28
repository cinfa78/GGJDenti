using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
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
}