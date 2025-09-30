using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
	//Á‚·‚Ü‚Å‚ÌŠÔ
	public float time;

	private void Start()
	{
		Destroy(gameObject,time);
	}
}
