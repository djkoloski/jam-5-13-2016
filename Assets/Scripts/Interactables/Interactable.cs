using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class Interactable : MonoBehaviour
{
	// Public variables
	public float interactableDistance;
	public UnityEvent onInteract;

	// Public interface
	public void Interact()
	{
		onInteract.Invoke();
	}

	// Gizmos
	public void OnDrawGizmos()
	{
		Color backupGizmosColor = Gizmos.color;
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, interactableDistance);
		Gizmos.color = backupGizmosColor;
	}
}