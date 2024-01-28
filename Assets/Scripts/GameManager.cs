using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
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
	private DepthOfField dof;

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
		dof = volume.profile.components.OfType<DepthOfField>().First();
	}

	private void Start()
	{
		ServiceLocator.sfxController.enabled = true;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
			Restart();

		if (Input.GetKeyDown(KeyCode.W))
			StartWinSequence();
	}

	private void OnEnable()
	{
		camera.StartingSequence();
		this.StartingSequence();
	}

	private void StartingSequence()
	{
		var sequence = DOTween.Sequence();
		sequence.PrependInterval(1f);
		sequence.Append(DOTween.To(() => dof.focalLength.value, x => dof.focalLength.value = x, 0, 2f));
		sequence.Play();
	}

	private void OnToothMoved()
	{
		FX();

		CheckWin();
	}

	public void Restart()
	{
		SceneManager.LoadScene(0);
	}

	private void FX()
	{
		// ChromaticAberration
		DOTween.To(() => chromaticAberration.intensity.value, x => chromaticAberration.intensity.value = x, 1f, 0.3f)
				.OnComplete(() => DOTween.To(() => chromaticAberration.intensity.value, x => chromaticAberration.intensity.value = x, 0, 0.3f));

		camera.Shake();
		camera.PunchIn();
	}

	private void CheckWin()
	{
		var hasWon = couples.All(x => (x.Item1.flag == x.Item2.flag));
		if (hasWon)
			StartWinSequence();
	}

	private void StartWinSequence()
	{
		StartCoroutine(WinSequence());
	}

	[SerializeField] private Transform victoryUi;
	[SerializeField] private GameObject restartButton;

	private IEnumerator WinSequence()
	{
		victoryUi.gameObject.SetActive(true);

		yield return new WaitForSeconds(1);
		ServiceLocator.sfxController.PlayVictory();
		yield return new WaitForSeconds(1);


		restartButton.SetActive(true);
		TMP_Text text = restartButton.GetComponentInChildren<TMP_Text>();
		text.alpha = 0;
		text.DOFade(1, 1f);
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