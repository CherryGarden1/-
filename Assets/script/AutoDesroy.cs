using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
	//�����܂ł̎���
	public float time;

	private void Start()
	{
		Destroy(gameObject,time);
	}
}
