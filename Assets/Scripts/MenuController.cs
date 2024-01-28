using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
	private bool isStarting = false;

	public void QuitGame() {
		Application.Quit();
	}

	public void PlayGame() {
		if (!isStarting) {
			StartCoroutine(Starting());
		}
	}

	private IEnumerator Starting() {
		yield return new WaitForSeconds(0.3f);
		SceneManager.LoadScene("DENTI");
	}
}