using UnityEngine;
using System.Collections.Generic;

public class YellowRoom : MonoBehaviour
{
	// Types
	public enum State
	{
		Idle,
		Spinning
	}

	// Public variables
	public SlotMachine[] slotMachines;
	public Transform payoutTarget;

	// Private variables
	private State state_;

	// Initialization
	public void Awake()
	{
		TransitionState(State.Idle);
	}
	public void Start()
	{
		SetWheelsNoTripleSeven();
	}

	// State Transitions
	private void TransitionState(State newState)
	{
		state_ = newState;
		switch (state_)
		{
			case State.Idle:
				break;
			case State.Spinning:
				break;
			default:
				throw new System.NotImplementedException();
		}
	}

	// Public interface
	public void SetWheels(params SlotMachineRoll[] rolls)
	{
		for (int i = 0; i < slotMachines.Length; ++i)
			slotMachines[i].SetOutcome(rolls[i]);
	}
	public void SetWheelsNoTripleSeven()
	{
		int roll1 = Random.Range(0, 4);
		int roll2 = Random.Range(0, 4);
		int roll3 = Random.Range(0, 4);

		if (roll1 == roll2 && roll2 == roll3 && roll3 == 0)
			roll2 = Random.Range(1, 4);

		SetWheels((SlotMachineRoll)roll1, (SlotMachineRoll)roll2, (SlotMachineRoll)roll3);
	}
	public void SpinContextual()
	{
		SlotMachineRoll outcome = slotMachines[0].Outcome;
		bool consensus = true;
		foreach (SlotMachine slotMachine in slotMachines)
			if (slotMachine.Outcome != outcome)
				consensus = false;

		if (consensus)
		{
			switch (outcome)
			{
				case SlotMachineRoll.Seven:
					Spin(SlotMachineRoll.Skull, SlotMachineRoll.Skull, SlotMachineRoll.Skull);
					break;
				case SlotMachineRoll.Bar:
					Spin(SlotMachineRoll.OneHundred, SlotMachineRoll.OneHundred, SlotMachineRoll.OneHundred);
					break;
				default:
					SpinRandom();
					break;
			}
		}
		else
			SpinRandom();
	}
	public void SpinRandom()
	{
		int roll1 = Random.Range(0, 4);
		int roll2 = Random.Range(0, 3);
		if (roll2 >= roll1)
			++roll2;
		int roll3 = Random.Range(0, 4);

		Spin((SlotMachineRoll)roll1, (SlotMachineRoll)roll2, (SlotMachineRoll)roll3);
	}
	public void Spin(params SlotMachineRoll[] rolls)
	{
		for (int i = 0; i < slotMachines.Length; ++i)
			slotMachines[i].Roll(rolls[i]);
		TransitionState(State.Spinning);
	}
	public GameObject Payout(GameObject prefab)
	{
		return (GameObject)Instantiate(prefab, payoutTarget.position, payoutTarget.localRotation);
	}

	// Update
	public void Update()
	{
		switch (state_)
		{
			case State.Idle:
				break;
			case State.Spinning:
				SlotMachineRoll outcome = slotMachines[0].Outcome;
				bool allFinished = true;
				bool allAgree = true;
				foreach (SlotMachine slotMachine in slotMachines)
				{
					if (slotMachine.IsSpinning)
						allFinished = false;
					if (slotMachine.Outcome != outcome)
						allAgree = false;
				}

				if (!allFinished)
					break;

				if (!allAgree)
				{
					// TODO: play bad noise
				}
				else
				{
					// TODO: play good noise
					OnOutcome(outcome);
				}

				TransitionState(State.Idle);

				break;
			default:
				throw new System.NotImplementedException();
		}
	}

	// Private interface
	private void OnOutcome(SlotMachineRoll roll)
	{
		Debug.Log(roll);

		switch (roll)
		{
			case SlotMachineRoll.Bar:
				break;
			case SlotMachineRoll.OneHundred:
				Level.OnSpun100s();
				break;
			case SlotMachineRoll.Seven:
				break;
			case SlotMachineRoll.Skull:
				Level.OnSpunSkulls();
				break;
			default:
				throw new System.NotImplementedException();
		}
	}
}