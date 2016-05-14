using UnityEngine;

public class MovementController : MonoBehaviour
{
	// Types
	public enum MovementType
	{
		XYZ,
		XZ
	}

	// Public variables
	public float maxVelocity;
	public float acceleration;
	public float maxAcceleration;
	public float timeToReach;
	public float jumpForce;
	public MovementType movementType;

	// Private variables
	private Rigidbody rigidbody_;

	// Initialization
	public void Awake()
	{
		rigidbody_ = GetComponent<Rigidbody>();
	}

	// Public interface
	public void Move(Vector3 movement)
	{
		switch (movementType)
		{
			case MovementType.XYZ:
				movement = movement.normalized * maxVelocity;
				MoveUtil.AccelerateClamped(rigidbody_, movement, acceleration, maxAcceleration);
				break;
			case MovementType.XZ:
				movement.y = 0.0f;
				movement = movement.normalized * maxVelocity;
				MoveUtil.AccelerateClampedXZ(rigidbody_, movement, acceleration, maxAcceleration);
				break;
			default:
				throw new System.NotImplementedException();
		}
	}
	public void MoveTo(Vector3 location)
	{
		switch (movementType)
		{
			case MovementType.XYZ:
				MoveUtil.AccelerateClampedToward(rigidbody_, location, acceleration, maxAcceleration, maxVelocity, timeToReach);
				break;
			case MovementType.XZ:
				MoveUtil.AccelerateClampedTowardXZ(rigidbody_, location, acceleration, maxAcceleration, maxVelocity, timeToReach);
				break;
			default:
				throw new System.NotImplementedException();
		}
	}
	public void Jump()
	{
		rigidbody_.AddForce(Vector3.up * jumpForce);
	}

	// Update
	public void FixedUpdate()
	{
		switch (movementType)
		{
			case MovementType.XYZ:
				MoveUtil.ClampVelocity(rigidbody_, maxVelocity);
				break;
			case MovementType.XZ:
				MoveUtil.ClampVelocityXZ(rigidbody_, maxVelocity);
				break;
			default:
				throw new System.NotImplementedException();
		}
	}
}