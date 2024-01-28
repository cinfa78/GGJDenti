using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

[SelectionBase]
public class ToothController : MonoBehaviour
{
	public static event Action Moved;

	[SerializeField] public bool flag = true;
	[SerializeField] public ToothController[] connectedTooths;
	[SerializeField] private MouthSide side;

	[SerializeField] private Color highlightColor;
	private Color startingColor;

	public float yOffset = 0.25f;
	private int currentPosition;
	private Vector3 defaultPosition;
	public ParticleSystem bloodVfx;
	public bool clickable;
	private MeshRenderer renderer;

	private void Awake()
	{
		defaultPosition = transform.localPosition;


		renderer = this.GetComponentInChildren<MeshRenderer>();
		startingColor = renderer.material.color;
	}

	public void OnMouseEnter()
	{
		GetComponentInChildren<MeshRenderer>().material.DOColor(highlightColor, 1);
	}

	public void OnMouseExit()
	{
		GetComponentInChildren<MeshRenderer>().material.DOColor(startingColor, 1);
	}

	public void Activate()
	{
		if (!clickable)
			return;

		Debug.Log($"{name} clicked");
		FlipTooth();
		foreach (var tooth in connectedTooths)
		{
			tooth.FlipTooth();
		}
	}

	public void StartShuffle(bool isSilent = false)
	{
		FlipTooth(isSilent);
		foreach (var tooth in connectedTooths)
		{
			tooth.FlipTooth(isSilent);
		}
	}

	public void FlipTooth(bool isSilent = false)
	{
		flag = !flag;
		MoveTooth(isSilent);

		if (!isSilent)
		{
			bloodVfx.Play();
			Moved?.Invoke();
		}
	}

	private void MoveTooth(bool isSilent = false)
	{
		var offset = side switch
		{
			MouthSide.Lower => yOffset * (flag ? 0 : -1),
			MouthSide.Upper => yOffset * (flag ? -1 : 0),
		};

		if (!isSilent)
		{
			ServiceLocator.sfxController.OnToothMoved();
		}

		SetRenderer();
		transform.DOLocalMove(defaultPosition + Vector3.up * offset, 0.5f)
				.OnComplete(SetRenderer);
	}

	private void SetRenderer()
	{
		renderer.enabled = side switch
		{
			MouthSide.Lower => !flag,
			MouthSide.Upper => flag,
		};

		clickable = renderer.enabled;
	}


	public void SetTooth(bool isGum)
	{
		this.flag = isGum;

		MoveTooth(true);
	}
}