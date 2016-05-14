using UnityEngine;
using UnityEngine.UI;

public enum Crosshair
{
	Default,
	Interact
}

public class CrosshairController : MonoBehaviour
{
	// Public variables
	public Sprite defaultCrosshair;
	public Sprite interactCrosshair;
	public Image crosshairImage;

	// Private variables
	private Crosshair crosshair_;

	// Initialization
	public void Awake()
	{
		crosshair_ = Crosshair.Default;
	}

	// Public interface
	public void SetCrosshair(Crosshair crosshair)
	{
		crosshair_ = crosshair;

		switch (crosshair_)
		{
			case Crosshair.Default:
				crosshairImage.sprite = defaultCrosshair;
				break;
			case Crosshair.Interact:
				crosshairImage.sprite = interactCrosshair;
				break;
			default:
				throw new System.NotImplementedException();
		}
	}
}