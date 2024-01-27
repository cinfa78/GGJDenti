using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class ToothStarter : MonoBehaviour {
	public ToothController[] teeth;
	public int startingMoves = 10;

	[Button("Shuffle")]
	private void Start() {
		foreach (var tooth in teeth) {
			tooth.clickable = false;
		}
		for (int i = 0; i < startingMoves; i++) {
			int tooth = Random.Range(0, teeth.Length);
			teeth[tooth].StartShuffle();
			Debug.Log(tooth);
		}
	}
}