using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
	//�����܂ł̎���
	public float time;

	private void Start()
	{
		Destroy(gameObject,time);
	}
}
