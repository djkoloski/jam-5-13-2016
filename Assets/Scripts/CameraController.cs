using UnityEngine;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
	// Public variables
	public float minPitch;
	public float maxPitch;
	public float pitchSpeed;

	// System variables
	public float CurrentPitch
	{
		get { return transform.localRotation.eulerAngles.x; }
	}

	// Private interface
	private Camera camera_;

	// Initialization
	public void Awake()
	{
		camera_ = GetComponent<Camera>();
	}

	// Public interface
	public void Pitch(float pitchInput)
	{
		Vector3 localRotation = transform.localRotation.eulerAngles;
		localRotation.x -= (localRotation.x > 180.0f ? 360.0f : 0.0f);
		localRotation.x = (Mathf.Clamp(localRotation.x + pitchInput * pitchSpeed * Time.deltaTime, minPitch, maxPitch) + 360.0f) % 360.0f;
		localRotation.y = 0.0f;
		localRotation.z = 0.0f;
		transform.localRotation = Quaternion.Euler(localRotation);
	}
	public bool CanSee(Vector3 point)
	{
		Vector3 screenPoint = camera_.WorldToScreenPoint(point);
		RaycastHit hit;
		bool contact = Physics.Raycast(transform.position, (point - transform.position).normalized, out hit, Layers.NotIgnoreRaycast);
		return (
			screenPoint.x >= 0 &&
			screenPoint.y >= 0 &&
			screenPoint.x < camera_.pixelWidth &&
			screenPoint.y < camera_.pixelHeight &&
			screenPoint.z > 0.0f &&
			(!contact || (hit.point - transform.position).magnitude >= (point - transform.position).magnitude - 0.1f)
		);
	}
}