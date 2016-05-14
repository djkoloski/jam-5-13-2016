using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class CrucibleController : MonoBehaviour
{
	// Public variables
	public Transform talismanAnchor;

	[Header("Talisman Prefabs")]
	public GameObject skullTalismanPrefab;
	public GameObject bookTalismanPrefab;
	public GameObject coinsTalismanPrefab;

	// System variables
	public Talisman ActiveTalisman
	{
		get { return talisman_; }
	}

	// Private variables
	private Talisman talisman_;
	private GameObject talismanGO_;

	// Initialization
	public void Awake()
	{
		talisman_ = Talisman.None;
		talismanGO_ = null;
	}

	// Public interface
	public void SetTalisman(Talisman talisman)
	{
		talisman_ = talisman;
		Destroy(talismanGO_);

		GameObject talismanPrefab = null;
		switch (talisman_)
		{
			case Talisman.None:
				return;
			case Talisman.Skull:
				talismanPrefab = skullTalismanPrefab;
				break;
			case Talisman.Book:
				talismanPrefab = bookTalismanPrefab;
				break;
			case Talisman.Coins:
				talismanPrefab = coinsTalismanPrefab;
				break;
			default:
				throw new System.NotImplementedException();
		}

		talismanGO_ = SpawnOnTable(talismanPrefab);
	}
	public void RotateTalisman()
	{
		SetTalisman(Level.GetNextTalisman());
	}
	public GameObject SpawnOnTable(GameObject prefab)
	{
		return (GameObject)Instantiate(prefab, talismanAnchor.position, Quaternion.identity);
	}
}