using UnityEngine;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
	// Types
	public enum State
	{
		Free,
		FirstPerson,
		GUI
	}

	// Static variables
	private static InputManager instance_;
	private static State State_
	{
		get { return instance_.state_; }
		set { instance_.state_ = value; }
	}

	// Private variables
	private State state_;
	private State lastNonFreeState_;

	// Initialization
	public void Awake()
	{
		instance_ = this;

		TransitionState(State.FirstPerson);
	}

	// State transitions
	private void TransitionState(State newState)
	{
		state_ = newState;
		switch (state_)
		{
			case State.Free:
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				break;
			case State.FirstPerson:
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				lastNonFreeState_ = state_;
				break;
			case State.GUI:
				Cursor.lockState = CursorLockMode.Confined;
				Cursor.visible = true;
				lastNonFreeState_ = state_;
				break;
			default:
				throw new System.NotImplementedException();
		}
	}

	// Public interface
	public static Vector2 GetFirstPersonMovement()
	{
		switch (State_)
		{
			case State.Free:
				return Vector2.zero;
			case State.FirstPerson:
				return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
			case State.GUI:
				return Vector2.zero;
			default:
				throw new System.NotImplementedException();
		}
	}
	public static Vector2 GetCameraMovement()
	{
		switch (State_)
		{
			case State.Free:
				return Vector2.zero;
			case State.FirstPerson:
				return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
			case State.GUI:
				return Vector2.zero;
			default:
				throw new System.NotImplementedException();
		}
	}
	public static bool GetJump()
	{
		switch (State_)
		{
			case State.Free:
			case State.GUI:
				return false;
			case State.FirstPerson:
				return Input.GetKeyDown(KeyCode.Space);
			default:
				throw new System.NotImplementedException();
		}
	}
	public static bool GetInteract()
	{
		switch (State_)
		{
			case State.Free:
			case State.GUI:
				return false;
			case State.FirstPerson:
				return Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0);
			default:
				throw new System.NotImplementedException();
		}
	}

	// Update
	public void Update()
	{
		switch (state_)
		{
			case State.Free:
				if (Input.GetMouseButtonDown(0))
					TransitionState(lastNonFreeState_);
				break;
			default:
				if (Input.GetKeyDown(KeyCode.Escape))
					TransitionState(State.Free);
				break;
		}
	}
}