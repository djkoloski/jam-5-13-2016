using UnityEngine;

public class RedRoom : MonoBehaviour
{
	// Public variables
	public CrucibleController crucible;

	// System variables
	public Talisman ActiveTalisman
	{
		get { return crucible.ActiveTalisman; }
	}
}