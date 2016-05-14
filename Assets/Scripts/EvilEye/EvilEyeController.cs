using UnityEngine;
using System.Collections.Generic;

public class EvilEyeController : MonoBehaviour
{
	// Types
	public enum State
	{
		Hidden,
		Regular,
		Huge
	}

	// Private variables
	private Animator animator_;
	private State state_;

	// Initialization
	public void Awake()
	{
		animator_ = GetComponent<Animator>();
		state_ = State.Hidden;

		TransitionState(State.Hidden);
	}

	// State transitions
	private void TransitionState(State newState)
	{
		switch (newState)
		{
			case State.Hidden:
				switch (state_)
				{
					case State.Hidden:
						break;
					case State.Huge:
						animator_.Play("shrinkAndDisappear");
						break;
					case State.Regular:
						animator_.Play("disappear");
						break;
					default:
						throw new System.NotImplementedException();
				}
				break;
			case State.Regular:
				switch (state_)
				{
					case State.Hidden:
						animator_.Play("appear");
						break;
					case State.Huge:
						animator_.Play("shrink");
						break;
					case State.Regular:
						break;
					default:
						throw new System.NotImplementedException();
				}
				break;
			case State.Huge:
				switch (state_)
				{
					case State.Hidden:
						animator_.Play("appearAndGrow");
						break;
					case State.Huge:
						break;
					case State.Regular:
						animator_.Play("grow");
						break;
					default:
						throw new System.NotImplementedException();
				}
				break;
			default:
				throw new System.NotImplementedException();
		}
		state_ = newState;
	}

	// Public interface
	public void Hide()
	{
		TransitionState(State.Hidden);
	}
	public void Show()
	{
		TransitionState(State.Regular);
	}
	public void Engulf()
	{
		TransitionState(State.Huge);
	}
}