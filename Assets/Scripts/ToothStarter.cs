using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothStarter : MonoBehaviour {
	public ToothController[] teeth;
	public int startingMoves = 10;

	private void Start() {
		for (int i = 0; i < startingMoves; i++) {
			teeth[Random.Range(0, teeth.Length)].SilentFlip();
		}
	}
}