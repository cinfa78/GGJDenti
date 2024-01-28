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
		
		
		
		for (int i = 0; i < Random.Range(5, 10); i++)
		{
			var vector = new Vector3(Random.value, Random.value, Random.value);

			transform.DOLocalRotate(vector, Random.Range(0f, 0.5f));
		}

		transform.localRotation = startingRotation;
	}
}