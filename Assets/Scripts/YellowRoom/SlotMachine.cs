using UnityEngine;
using System.Collections.Generic;

public enum SlotMachineRoll
{
	Seven,
	Skull,
	Bar,
	OneHundred
}

public class SlotMachine : MonoBehaviour
{
	// Types
	public enum State
	{
		Idle,
		Spinning
	}

	// Public variables
	public int minSpins;
	public int maxSpins;

	// System variables
	public bool IsIdle
	{
		get { return state_ == State.Idle; }
	}
	public bool IsSpinning
	{
		get { return state_ == State.Spinning; }
	}
	public SlotMachineRoll Outcome
	{
		get { return outcome_; }
	}

	// Private variables
	private Animator animator_;
	private State state_;
	private int spinsLeft_;
	private SlotMachineRoll outcome_;

	// Initialization
	public void Awake()
	{
		animator_ = GetComponent<Animator>();
		spinsLeft_ = 0;
	}

	// State transitions
	private void TransitionState(State newState)
	{
		state_ = newState;
		switch (newState)
		{
			case State.Idle:
				break;
			case State.Spinning:
				animator_.Play("spinning");
				break;
			default:
				break;
		}
	}

	// Public interface
	public void Roll(SlotMachineRoll outcome)
	{
		spinsLeft_ = Random.Range(minSpins, maxSpins);
		outcome_ = outcome;
		TransitionState(State.Spinning);
	}
	public void SetOutcome(SlotMachineRoll outcome)
	{
		outcome_ = outcome;
		TransitionState(State.Idle);
		switch (outcome)
		{
			case SlotMachineRoll.Bar:
				animator_.Play("idle_bar");
				break;
			case SlotMachineRoll.OneHundred:
				animator_.Play("idle_100");
				break;
			case SlotMachineRoll.Seven:
				animator_.Play("idle_seven");
				break;
			case SlotMachineRoll.Skull:
				animator_.Play("idle_skull");
				break;
			default:
				throw new System.NotImplementedException();
		}
	}

	// Events
	public void OnSpinComplete()
	{
		--spinsLeft_;
		if (spinsLeft_ == 0)
		{
			switch (outcome_)
			{
				case SlotMachineRoll.Bar:
					animator_.Play("land_bar");
					break;
				case SlotMachineRoll.Skull:
					animator_.Play("land_skull");
					break;
				case SlotMachineRoll.Seven:
					animator_.Play("land_seven");
					break;
				case SlotMachineRoll.OneHundred:
					animator_.Play("land_100");
					break;
				default:
					throw new System.NotImplementedException();
			}
		}
	}
	public void OnLandComplete()
	{
		TransitionState(State.Idle);
	}
}