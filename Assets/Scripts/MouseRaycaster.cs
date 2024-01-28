using UnityEngine;
using UnityEngine.UIElements;

public class MouseRaycaster : MonoBehaviour
{
	[SerializeField] private Camera camera;
	public Vector3 point;
	

	// Update is called once per frame
	void Update()
	{
		var ray = camera.ScreenPointToRay(Input.mousePosition);

		if (!Physics.Raycast(ray, out RaycastHit hit))
			return;

		point = hit.point;

		if (Input.GetMouseButtonDown(0))
		{
			if (hit.transform.TryGetComponent<ToothController>(out var tooth))
				tooth.Activate();
		}
	}


	private void OnDrawGizmos()
	{
		Gizmos.DrawSphere(point, 0.5f);
	}
}