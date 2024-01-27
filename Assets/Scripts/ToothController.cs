using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
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
	public AudioClip flipClip;
	private AudioSource audioSource;
	private int currentPosition;
	private Vector3 defaultPosition;
	public ParticleSystem bloodVfx;
	public bool clickable;

	private void Awake()
	{
		defaultPosition = transform.localPosition;
		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.playOnAwake = false;
		audioSource.clip = flipClip;

		startingColor = this.GetComponentInChildren<MeshRenderer>().material.color;
	}

	public void OnMouseEnter()
	{
		GetComponentInChildren<MeshRenderer>().material.DOColor(highlightColor, 1);
	}

	public void OnMouseExit()
	{
		GetComponentInChildren<MeshRenderer>().material.DOColor(startingColor, 1);
	}

	public void OnMouseDown()
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

	public void StartShuffle()
	{
		FlipTooth();
		foreach (var tooth in connectedTooths)
		{
			tooth.FlipTooth();
		}
	}

	public void FlipTooth(bool isSilent = false)
	{
		flag = !flag;
		MoveTooth();

		if (!isSilent)
		{
			audioSource.pitch = 1 + Random.Range(-0.5f, 0.5f);
			audioSource.Play();
			audioSource.DOPitch(1, 0.2f);
			bloodVfx.Play();
		}
	}

	private Vector3 MoveTooth()
	{
		var offset = side switch
		{
			MouthSide.Lower => yOffset * (flag ? 0 : -1),
			MouthSide.Upper => yOffset * (flag ? -1 : 0),
		};

		return transform.localPosition = defaultPosition + transform.up * offset;
	}

	public void SetTooth(bool isGum)
	{
		this.flag = isGum;

		MoveTooth();
	}
}