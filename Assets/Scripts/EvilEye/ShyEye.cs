using UnityEngine;
using System.Collections.Generic;

public class ShyEye : MonoBehaviour
{
	// Private variables
	private EvilEyeController evilEyeController_;

	// Initialization
	public void Awake()
	{
		evilEyeController_ = GetComponent<EvilEyeController>();
	}

	// Update
	public void Update()
	{
		if (PlayerController.instance.CanSee(transform.position))
			evilEyeController_.Hide();
		else
			evilEyeController_.Show();
	}
}