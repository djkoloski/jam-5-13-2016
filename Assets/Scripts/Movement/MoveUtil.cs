using UnityEngine;

public static class MoveUtil
{
	public static void AccelerateClamped(Rigidbody rb, Vector3 targetVelocity, float acceleration, float maxAcceleration)
	{
		Vector3 dv = targetVelocity - rb.velocity;
		dv = dv.normalized * Mathf.Min(dv.magnitude * acceleration, maxAcceleration);
		rb.AddForce(dv, ForceMode.Acceleration);
	}
	public static void AccelerateClampedToward(Rigidbody rb, Vector3 target, float acceleration, float maxAcceleration, float maxVelocity, float timeToReach)
	{
		Vector3 targetVelocity = target - rb.position;
		targetVelocity = targetVelocity.normalized * Mathf.Min(targetVelocity.magnitude / timeToReach, maxVelocity);
		AccelerateClamped(rb, targetVelocity, acceleration, maxAcceleration);
	}
	public static void ClampVelocity(Rigidbody rb, float maxVelocity)
	{
		rb.velocity = rb.velocity.normalized * Mathf.Min(rb.velocity.magnitude, maxVelocity);
	}

	public static void AccelerateClampedXZ(Rigidbody rb, Vector3 targetVelocity, float acceleleration, float maxAcceleration)
	{
		targetVelocity.y = 0.0f;
		AccelerateClamped(rb, targetVelocity, acceleleration, maxAcceleration);
	}
	public static void AccelerateClampedTowardXZ(Rigidbody rb, Vector3 target, float acceleration, float maxAcceleration, float maxVelocity, float timeToReach)
	{
		target.y = rb.position.y;
		AccelerateClampedToward(rb, target, acceleration, maxAcceleration, maxVelocity, timeToReach);
	}
	public static void ClampVelocityXZ(Rigidbody rb, float maxVelocity)
	{
		Vector3 velocity = rb.velocity;
		velocity.y = 0.0f;
		velocity = velocity.normalized * Mathf.Min(velocity.magnitude, maxVelocity);
		rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
	}

	public static void AccelerateClamped2D(Rigidbody2D rb, Vector2 targetVelocity, float acceleration, float maxAcceleration)
	{
		Vector2 dv = targetVelocity - rb.velocity;
		dv = dv.normalized * Mathf.Min(dv.magnitude * acceleration, maxAcceleration);
		rb.AddForce(dv * rb.mass, ForceMode2D.Force);
	}
	public static void AccelerateClampedToward2D(Rigidbody2D rb, Vector2 target, float acceleration, float maxAcceleration, float maxVelocity, float timeToReach)
	{
		Vector2 targetVelocity = target - rb.position;
		targetVelocity = targetVelocity.normalized * Mathf.Min(targetVelocity.magnitude / timeToReach, maxVelocity);
		AccelerateClamped2D(rb, targetVelocity, acceleration, maxAcceleration);
	}
	public static void ClampVelocity2D(Rigidbody2D rb, float maxVelocity)
	{
		rb.velocity = rb.velocity.normalized * Mathf.Min(rb.velocity.magnitude, maxVelocity);
	}
	public static Vector2 ToIsometric(Vector2 input)
	{
		return new Vector2(input.x, input.y * 0.5f);
	}
	public static Vector2 FromIsometric(Vector2 input)
	{
		return new Vector2(input.x, input.y * 2.0f);
	}
}