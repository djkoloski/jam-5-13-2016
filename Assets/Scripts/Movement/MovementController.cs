using UnityEngine;

public class MovementController : MonoBehaviour
{
	// Public variables
	public float maxVelocity;
	public float acceleration;
	public float maxAcceleration;
	public float timeToReach;

	// Private variables
	private Rigidbody rigidbody_;

	// Initialization
	public void Awake()
	{
		rigidbody_ = GetComponent<Rigidbody>();
	}

	// Public interface
	public void Move(Vector2 movement)
	{
		movement = movement.normalized * maxVelocity;
		MoveUtil.AccelerateClamped(rigidbody_, movement, acceleration, maxAcceleration);
	}
	public void MoveTo(Vector2 location)
	{
		MoveUtil.AccelerateClampedToward(rigidbody_, location, acceleration, maxAcceleration, maxVelocity, timeToReach);
	}

	// Update
	public void FixedUpdate()
	{
		MoveUtil.ClampVelocity(rigidbody_, maxVelocity);
	}
}