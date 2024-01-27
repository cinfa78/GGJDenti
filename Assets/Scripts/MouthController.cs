using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MouthController : MonoBehaviour
{
	[SerializeField] private Transform lowerJaw;
	[SerializeField] private Transform upperJaw;

	[Range(0, 5)] [SerializeField] private float separation;

	private void Update()
	{
		lowerJaw.localPosition = Vector3.down * separation;
		upperJaw.localPosition = Vector3.up * separation;
	}
}