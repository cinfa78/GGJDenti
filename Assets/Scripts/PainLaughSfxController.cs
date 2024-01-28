using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainLaughSfxController : MonoBehaviour {
	public AudioClip[] painClips;
	public AudioClip[] laughClips;
	public AudioSource painSource;
	public AudioSource laughSource;
	public AudioClip victoryLaugh;

	public void PlayLaugh() {
		laughSource.Stop();
		laughSource.clip = laughClips[Random.Range(0, laughClips.Length)];
		laughSource.pitch = 1 + Random.Range(-0.2f, 0.2f);
		laughSource.Play();
	}

	public void PlayPain() {
		painSource.Stop();
		painSource.clip = painClips[Random.Range(0, painClips.Length)];
		painSource.pitch = 1 + Random.Range(-0.2f, 0.2f);
		painSource.Play();
	}

	public void PlayVictory() {
		laughSource.Stop();
		laughSource.clip = victoryLaugh;
		laughSource.pitch = 1;
		laughSource.Play();
	}
}