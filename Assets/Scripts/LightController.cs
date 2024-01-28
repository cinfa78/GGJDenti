using System;
using UnityEngine;
using UnityEngine.Serialization;

public class LightController : MonoBehaviour
{
	[SerializeField] private MouseRaycaster target;

	[FormerlySerializedAs("delay")] [SerializeField]
	private float speed;

	[SerializeField] private AnimationCurve flickerCurve;
	private float medianIntensity;

	private new Light light;
	private float curveDuration;

	private void Start()
	{
		light = this.GetComponent<Light>();
		medianIntensity = light.intensity;

		curveDuration = flickerCurve[flickerCurve.length - 1].time;
	}

	void Update()
	{
		LookAt();
		Flicker();
	}

	private void Flicker()
	{
		var t = Time.time % curveDuration;

		light.intensity = flickerCurve.Evaluate(t) * medianIntensity;
	}

	private void LookAt()
	{
		this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.point - transform.position), speed * Time.deltaTime);
	}
}