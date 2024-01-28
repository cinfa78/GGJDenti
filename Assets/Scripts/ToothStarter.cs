using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Random = UnityEngine.Random;

public enum MouthSide
{
	Lower = 1,
	Upper = -1,
}

public class ToothStarter : MonoBehaviour
{
	public ToothController[] teeth;
	public int startingMoves = 10;

	public MouthSide side;

	private void Start()
	{
		teeth.ToList().ForEach(x => x.SetTooth(false));
		switch (side)
		{
			case MouthSide.Upper:
				Shuffle();
				break;

			case MouthSide.Lower:
				break;
		}
	}


	[Button("BogoSort")]
	private void BogoSortTeeth()
	{
		teeth = teeth.OrderBy(x => x.transform.position.x).ToArray();
		foreach (ToothController toothController in teeth)
		{
			toothController.transform.parent = null;
		}

		for (var i = 0; i < teeth.Length; i++)
		{
			var tooth = teeth[i];
			tooth.name = $"{(side == MouthSide.Lower ? "Lower" : "Upper")}Tooth #{i + 1}";

			tooth.transform.parent = this.transform;
		}
	}

	[Button("Shuffle")]
	private void Shuffle()
	{
		foreach (var tooth in teeth)
		{
			tooth.clickable = false;
		}

		var moves = 0;
		var tries = 0;
		while (moves < startingMoves && tries++ < 100)
		{
			int index = Random.Range(0, teeth.Length);

			ToothController tooth = teeth[index];
			if (tooth.flag)
				continue;

			tooth.StartShuffle(true);
			Debug.Log(index);
			moves++;
		}
	}


	#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		for (var i = 0; i < teeth.Length; i++)
		{
			var tooth = teeth[i];
			Handles.Label(tooth.transform.position, i.ToString());
		}
	}
	#endif
}