using UnityEngine;

public class Gate : MonoBehaviour
{
	// Types
	public enum State
	{
		Closed,
		Open
	}

	// Public variables
	public AudioSource shiftSound;

	// Private variables
	private Animator animator_;
	private State state_;

	// Initialization
	public void Awake()
	{
		animator_ = GetComponent<Animator>();
	}

	// State transitions
	private void TransitionState(State newState)
	{
		state_ = newState;
		switch (state_)
		{
			case State.Closed:
				animator_.Play("closing");
				shiftSound.Play();
				break;
			case State.Open:
				animator_.Play("opening");
				shiftSound.Play();
				break;
			default:
				break;
		}
	}

	// Public interface
	public void Open()
	{
		if (state_ != State.Open)
			TransitionState(State.Open);
	}
	public void Close()
	{
		if (state_ != State.Closed)
			TransitionState(State.Closed);
	}
}