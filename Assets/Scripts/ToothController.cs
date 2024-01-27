using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

[SelectionBase]
public class ToothController : MonoBehaviour {
	public float yOffset = 0.25f;
	public ToothController[] connectedTooths;
	[SerializeField] private bool moved = true;
	public AudioClip flipClip;
	private AudioSource audioSource;
	private int currentPosition;
	private Vector3 defaultPosition;
	public ParticleSystem bloodVfx;
	public bool clickable;

	private void Awake() {
		defaultPosition = transform.localPosition;
		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.playOnAwake = false;
		audioSource.clip = flipClip;
	}

	public void OnMouseDown() {
		if (clickable) {
			Debug.Log($"{name} clicked");
			FlipTooth();
			foreach (var tooth in connectedTooths) {
				tooth.FlipTooth();
			}
		}
	}

	public void StartShuffle() {
		SilentFlip();
		foreach (var tooth in connectedTooths) {
			tooth.SilentFlip();
		}
	}

	private void SilentFlip() {
		moved = !moved;
		transform.localPosition = defaultPosition + transform.up * yOffset * (moved ? -1 : 0);
	}

	private void FlipTooth() {
		audioSource.pitch = 1 + Random.Range(-0.5f, 0.5f);
		audioSource.Play();
		audioSource.DOPitch(1, 0.2f);

		transform.localPosition = defaultPosition + transform.up * yOffset * (moved ? -1 : 0);

		moved = !moved;
		bloodVfx.Play();
	}
}