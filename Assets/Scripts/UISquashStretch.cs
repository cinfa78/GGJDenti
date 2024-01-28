using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISquashStretch : MonoBehaviour {
	public float amplitude;
	public float frequency;
	private RectTransform rectTransform;
	private Vector3 defaultScale;

	private void Awake() {
		rectTransform = GetComponent<RectTransform>();
		defaultScale = rectTransform.localScale;
	}

	private void Update() {
		rectTransform.localScale = new Vector3(defaultScale.x + amplitude * Mathf.Sin(Time.time * frequency),
			defaultScale.x + amplitude * Mathf.Cos(Time.time * frequency), 1
		);
	}
}