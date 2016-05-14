using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class EvilEye : MonoBehaviour
{
	// Public variables
	public float offset;
	public float scale;

	// Private variables
	private MaterialPropertyBlock propertyBlock_;
	private MeshRenderer meshRenderer_;

	// Initialization
	public void Awake()
	{
		propertyBlock_ = new MaterialPropertyBlock();
		meshRenderer_ = GetComponent<MeshRenderer>();
	}

	// Update
	public void Update()
	{
		if (propertyBlock_ == null)
			propertyBlock_ = new MaterialPropertyBlock();

		propertyBlock_.SetFloat("_Offset", offset);
		propertyBlock_.SetFloat("_Scale", scale);
		meshRenderer_.SetPropertyBlock(propertyBlock_);
	}
}