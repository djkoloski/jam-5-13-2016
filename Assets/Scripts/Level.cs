using UnityEngine;

public enum Talisman
{
	None,
	Skull,
	Book,
	Coins
}

public class Level : MonoBehaviour
{
	// Types
	public enum LookState
	{
		LookingUp,
		LookingDown
	}
	public enum State
	{
		ReadIntroNote,
		GetFirstCoin,
		GetSecondCoin,
		Done
	}
	public delegate void Callback();

	// Static variables
	private static Level instance_;

	// Public variables
	[Header("Graphics settings")]
	public int targetFrameRate;
	public int vSyncCount;

	[Header("UI")]
	public CrosshairController crosshairController;

	[Header("Rooms")]
	public BlueRoom blueRoom;
	public RedRoom redRoom;

	[Header("Gates")]
	public Gate blueGate;
	public Gate whiteGate;
	public Gate yellowGate;

	[Header("Talismans")]
	public bool haveSkullTalisman;
	public bool haveBookTalisman;
	public bool haveCoinsTalisman;

	[Header("Interactables")]
	public GameObject introNotePrefab;
	public GameObject coinPrefab;

	// System variables
	public static Talisman ActiveTalisman
	{
		get { return instance_.redRoom.ActiveTalisman; }
	}
	public static bool HaveSkullTalisman
	{
		get { return instance_.haveSkullTalisman; }
	}
	public static bool HaveBookTalisman
	{
		get { return instance_.haveBookTalisman; }
	}
	public static bool HaveCoinsTalisman
	{
		get { return instance_.haveCoinsTalisman; }
	}

	// Private variables
	private LookState lookState_;
	private State state_;

	// Initialization
	public void Awake()
	{
		instance_ = this;
		Application.targetFrameRate = targetFrameRate;
		QualitySettings.vSyncCount = vSyncCount;

		lookState_ = LookState.LookingDown;
		TransitionState(State.ReadIntroNote);
	}

	// State transitions
	private void TransitionState(State newState)
	{
		state_ = newState;
		switch (state_)
		{
			case State.ReadIntroNote:
				SpawnOnCrucibleWithDestroyCallback(introNotePrefab, OnReadIntroNote);
				break;
			case State.GetFirstCoin:
				break;
			case State.GetSecondCoin:
				break;
			case State.Done:
				break;
			default:
				throw new System.NotImplementedException();
		}
	}

	// Public interface
	public static Talisman GetNextTalisman()
	{
		switch (ActiveTalisman)
		{
			case Talisman.None:
				if (HaveSkullTalisman)
					return Talisman.Skull;
				else if (HaveBookTalisman)
					return Talisman.Book;
				else if (HaveCoinsTalisman)
					return Talisman.Coins;
				else
					return Talisman.None;
			case Talisman.Skull:
				if (HaveBookTalisman)
					return Talisman.Book;
				else if (HaveCoinsTalisman)
					return Talisman.Coins;
				else
					return Talisman.None;
			case Talisman.Book:
				if (HaveCoinsTalisman)
					return Talisman.Coins;
				else
					return Talisman.None;
			case Talisman.Coins:
				return Talisman.None;
			default:
				throw new System.NotImplementedException();
		}
	}
	public static void OnGetSkullTalisman()
	{
		instance_.haveSkullTalisman = true;
	}
	public static void OnGetBookTalisman()
	{
		instance_.haveBookTalisman = true;
	}
	public static void OnGetCoinsTalisman()
	{
		instance_.haveCoinsTalisman = true;
	}
	public static void SetTalisman(Talisman talisman)
	{
		instance_.redRoom.crucible.SetTalisman(talisman);
		instance_.OnTalismanChanged();
	}
	public static void RotateTalisman(Talisman talisman)
	{
		instance_.redRoom.crucible.RotateTalisman();
		instance_.OnTalismanChanged();
	}

	// Update
	public void Update()
	{
		switch (lookState_)
		{
			case LookState.LookingDown:
				if (PlayerController.instance.IsLookingUp)
				{
					lookState_ = LookState.LookingUp;
					OnPlayerLookedUp();
				}
				break;
			case LookState.LookingUp:
				if (!PlayerController.instance.IsLookingUp)
				{
					lookState_ = LookState.LookingDown;
					OnPlayerLookedDown();
				}
				break;
			default:
				throw new System.NotImplementedException();
		}

		if (PlayerController.instance.IsLookingAtInteractable)
			crosshairController.SetCrosshair(Crosshair.Interact);
		else
			crosshairController.SetCrosshair(Crosshair.Default);
	}

	// Events
	public void OnPlayerLookedUp()
	{
		switch (state_)
		{
			case State.ReadIntroNote:
				break;
			case State.GetFirstCoin:
				SpawnOnCrucibleWithDestroyCallback(coinPrefab, OnGetFirstCoin);
				break;
			case State.GetSecondCoin:
				SpawnOnCrucibleWithDestroyCallback(coinPrefab, OnGetSecondCoin);
				break;
			case State.Done:
				blueRoom.GenerateStarmap();
				break;
			default:
				throw new System.NotImplementedException();
		}
	}
	public void OnPlayerLookedDown()
	{
	}
	public void OnTalismanChanged()
	{
		switch (ActiveTalisman)
		{
			case Talisman.None:
				blueGate.Close();
				whiteGate.Close();
				yellowGate.Close();
				break;
			case Talisman.Book:
				blueGate.Open();
				whiteGate.Close();
				yellowGate.Close();
				break;
			case Talisman.Skull:
				blueGate.Close();
				whiteGate.Open();
				yellowGate.Close();
				break;
			case Talisman.Coins:
				blueGate.Close();
				whiteGate.Close();
				yellowGate.Open();
				break;
			default:
				throw new System.NotImplementedException();
		}
	}

	// Story events
	public void OnReadIntroNote()
	{
		// TODO: give hint
		TransitionState(State.GetFirstCoin);
	}
	public void OnGetFirstCoin()
	{
		// TODO: play a sound
		TransitionState(State.GetSecondCoin);
	}
	public void OnGetSecondCoin()
	{
		// TODO: play a sound
		OnGetCoinsTalisman();
		SetTalisman(Talisman.Coins);
		TransitionState(State.Done);
	}

	// Private interface
	private void SetInteractableDestroyCallback(GameObject go, Callback callback)
	{
		go.GetComponent<Interactable>().onInteract.AddListener(() => { Destroy(go); callback(); });
	}
	private void SpawnOnCrucibleWithDestroyCallback(GameObject prefab, Callback callback)
	{
		SetInteractableDestroyCallback(redRoom.crucible.SpawnOnTable(prefab), callback);
	}
}