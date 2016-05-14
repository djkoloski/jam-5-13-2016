using UnityEngine;
using System.Collections.Generic;

public class BlueRoomStarLayout: MonoBehaviour
{
	// Public variables
	public GameObject starPrefab;
	public Vector3 xAxis;
	public Vector3 yAxis;

	// Private variables
	private LineRenderer lineRenderer_;
	private List<GameObject> stars_;

	// Initialization
	public void Awake()
	{
		lineRenderer_ = GetComponent<LineRenderer>();
		stars_ = new List<GameObject>();
	}

	// Public interface
	public void Clear()
	{
		for (int i = 0; i < stars_.Count; ++i)
			Destroy(stars_[i]);
		stars_.Clear();
	}
	public void Rebuild(List<Vector2> stars, int randomIndex0, int randomIndex1)
	{
		Clear();
		for (int i = 0; i < stars.Count; ++i)
			stars_.Add((GameObject)Instantiate(starPrefab, MapPosition(stars[i]), Quaternion.identity));

		// Draw a line through two random stars
		Vector2 star0 = stars[randomIndex0];
		Vector2 star1 = stars[randomIndex1];

		lineRenderer_.SetVertexCount(2);
		lineRenderer_.SetPositions(new Vector3[] { MapPosition(star0), MapPosition(star1) });
	}

	// Private interface
	private Vector3 MapPosition(Vector2 p)
	{
		return transform.position + xAxis * (p.x - 0.5f) * 2.0f + yAxis * (p.y - 0.5f) * 2.0f;
	}
}