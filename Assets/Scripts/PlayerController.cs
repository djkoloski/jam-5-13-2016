using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
	// Static variables
	public static PlayerController instance;

	// Public variables
	[Header("Constants")]
	public float lookUpTolerance;

	[Header("Movement")]
	public float speed;
	public float turnSpeed;

	[Header("References")]
	public CameraController cameraController;

	// System variables
	public float CurrentPitch
	{
		get { return cameraController.CurrentPitch; }
	}
	public bool IsLookingAtSkybox
	{
		get { return isLookingAtSkybox_; }
	}
	public bool IsLookingUp
	{
		get { return Mathf.Abs(instance.CurrentPitch - 270.0f) < lookUpTolerance; }
	}
	public bool IsLookingAtInteractable
	{
		get { return isLookingAtInteractable_; }
	}

	// Private variables
	private CharacterController characterController_;
	private bool isLookingAtSkybox_;
	private bool isLookingAtInteractable_;

	// Initialization
	public void Awake()
	{
		instance = this;
		characterController_ = GetComponent<CharacterController>();
		isLookingAtSkybox_ = false;
		isLookingAtInteractable_ = false;
	}

	// Update
	public void Update()
	{
		Vector2 movementInput = InputManager.GetFirstPersonMovement();
		Vector2 cameraInput = InputManager.GetCameraMovement();

		// Turn by horizontal
		float turnAmount = cameraInput.x * turnSpeed * Time.deltaTime;
		transform.Rotate(Vector3.up, turnAmount);

		// Pitch by vertical
		cameraController.Pitch(-cameraInput.y);

		// Move relative to orientation
		Vector3 movement = (transform.right * movementInput.x + transform.forward * movementInput.y) * speed;
		characterController_.SimpleMove(movement);

		// Update skybox
		isLookingAtSkybox_ = !Physics.Raycast(cameraController.transform.position, cameraController.transform.forward);

		// Update interactables
		Interactable interactable = LookForInteractable();
		isLookingAtInteractable_ = (interactable != null);
		if (isLookingAtInteractable_ && InputManager.GetInteract())
			interactable.Interact();
	}

	// Public interface
	public bool CanSee(Vector3 point)
	{
		return cameraController.CanSee(point);
	}

	// Private interface
	private Interactable LookForInteractable()
	{
		RaycastHit hit;
		if (Physics.Raycast(cameraController.transform.position, cameraController.transform.forward, out hit, 100.0f, Layers.NotIgnoreRaycast))
		{
			Interactable interactable = hit.collider.GetComponent<Interactable>();
			if (interactable == null)
				return null;

			Vector3 toInteractable = cameraController.transform.position - interactable.transform.position;
			float distanceToInteractable = Vector3.ProjectOnPlane(toInteractable, Vector3.up).magnitude;
			if (distanceToInteractable < interactable.interactableDistance)
				return interactable;
		}

		return null;
	}
}