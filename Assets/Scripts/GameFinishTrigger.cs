using UnityEngine;

public class GameFinishTrigger : MonoBehaviour
{
	public void OnTriggerEnter()
	{
		FadeToBlack.instance.FadeOut();
	}
}