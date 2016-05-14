using UnityEngine;
using System.Collections.Generic;

public class BlueRoom : MonoBehaviour
{
	// Public variables
	public int starCount;
	public BlueRoomStarLayout starChart;
	public BlueRoomStarLayout starFloor;

	// System variables
	public bool IsConnected
	{
		get
		{
			return (
				chartRandomIndex0_ == floorRandomIndex0_ ||
				chartRandomIndex0_ == floorRandomIndex1_ ||
				chartRandomIndex1_ == floorRandomIndex0_ ||
				chartRandomIndex1_ == floorRandomIndex1_
			);
		}
	}

	// Private variables
	private List<Vector2> starPositions_;
	private int chartRandomIndex0_;
	private int chartRandomIndex1_;
	private int floorRandomIndex0_;
	private int floorRandomIndex1_;

	// Initialization
	public void Awake()
	{
		starPositions_ = new List<Vector2>();
	}

	// Public interface
	public void GenerateStarmap()
	{
		starPositions_ = new List<Vector2>();
		for (int i = 0; i < starCount; ++i)
			starPositions_.Add(new Vector2(Random.value, Random.value));

		chartRandomIndex0_ = Random.Range(0, starCount);
		chartRandomIndex1_ = Random.Range(0, starCount - 1);
		if (chartRandomIndex1_ >= chartRandomIndex0_)
			++chartRandomIndex1_;

		floorRandomIndex0_ = Random.Range(0, starCount);
		floorRandomIndex1_ = Random.Range(0, starCount - 1);
		if (floorRandomIndex1_ >= floorRandomIndex0_)
			++floorRandomIndex1_;

		starChart.Rebuild(starPositions_, chartRandomIndex0_, chartRandomIndex1_);
		starFloor.Rebuild(starPositions_, floorRandomIndex0_, floorRandomIndex1_);
	}
}