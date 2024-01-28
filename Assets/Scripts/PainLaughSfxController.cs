using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PainLaughSfxController : MonoBehaviour
{
	public AudioClip[] painClips;
	public AudioClip[] laughClips;
	public AudioClip victoryLaugh;
	public AudioClip toothAudioClip;

	private void Awake()
	{
		DontDestroyOnLoad(this);
		ServiceLocator.sfxController = this;
		this.enabled = false;
	}
	
	private AudioSource GetAudioSource(AudioClip clip)
	{
		var source = this.AddComponent<AudioSource>();
		source.clip = clip;
		Destroy(source, clip.length * 2.5f);
		return source;
	}

	public void PlayLaugh()
	{
		var clip = laughClips[Random.Range(0, laughClips.Length)];
		var source = GetAudioSource(clip);
		source.pitch = 1 + Random.Range(-0.2f, 0.2f);
		source.Play();
	}

	public void PlayStartingLaugh()
	{
		var clip = laughClips[Random.Range(0, laughClips.Length)];
		var source = GetAudioSource(clip);
		source.pitch = 0.6f;
		source.volume = 2;
		source.Play();
	}

	public void PlayPain()
	{
		var clip = painClips[Random.Range(0, painClips.Length)];
		var source = GetAudioSource(clip);
		source.pitch = 1 + Random.Range(-0.2f, 0.2f);
		source.Play();
	}

	public void PlayVictory()
	{
		var source = GetAudioSource(victoryLaugh);
		source.pitch = 1 + Random.Range(-0.2f, 0.2f);
		source.Play();
	}

	public void PlayTooth()
	{
		var source = GetAudioSource(toothAudioClip);
		source.pitch = 1 + Random.Range(-0.5f, 0.5f);
		source.DOPitch(1, 0.2f);

		source.Play();
	}


	public void OnToothMoved()
	{
		PlayTooth();
		PlayLaugh();
		PlayPain();
	}
}