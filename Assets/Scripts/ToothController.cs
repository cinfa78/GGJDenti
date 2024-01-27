using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class ToothController : MonoBehaviour {
	public float yOffset = 0.25f;
	public ToothController[] connectedTooths;
	private bool moved;
	public AudioClip flipClip;
	private AudioSource audioSource;
	private int currentPosition;
	private Vector3 defaultPosition;
	public ParticleSystem bloodVfx;

	private void Awake() {
		defaultPosition = transform.localPosition;
		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.playOnAwake = false;
		audioSource.clip = flipClip;
	}

	public void OnMouseDown() {
		Debug.Log($"{name} clicked");
		FlipTooth();
		foreach (var tooth in connectedTooths) {
			tooth.FlipTooth();
		}
	}

	public void SilentFlip() {
		transform.localPosition = defaultPosition + transform.up * yOffset * (moved ? -1 : 0);
		moved = !moved;
	}

	public void FlipTooth() {
		audioSource.pitch = 1 + Random.Range(-0.5f, 0.5f);
		audioSource.Play();
		audioSource.DOPitch(1, 0.2f);

		transform.localPosition = defaultPosition + transform.up * yOffset * (moved ? -1 : 0);

		moved = !moved;


		bloodVfx.Play();
	}
}