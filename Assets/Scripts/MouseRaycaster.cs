using UnityEngine;

public class MouseRaycaster : MonoBehaviour
{
	private Camera camera;
	public Vector3 point;

	// Start is called before the first frame update
	void Start()
	{
		camera = this.GetComponent<Camera>();
	}

	// Update is called once per frame
	void Update()
	{
		var ray = camera.ScreenPointToRay(Input.mousePosition);

		if (!Physics.Raycast(ray, out RaycastHit hit))
			return;

		point = hit.point;
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawSphere(point, 0.5f);
	}
}