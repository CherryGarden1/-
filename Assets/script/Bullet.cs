using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField]
	private float bulletSpeed;
	[SerializeField]
	private Rigidbody rb;

	void Start()
	{
		// ���˕����͐������ꂽ�Ƃ��� forward
		if (rb != null)
		{
			rb.linearVelocity = transform.forward * bulletSpeed; // Unity 6�n
																 // rb.velocity = transform.forward * bulletSpeed;   // Unity 2023�ȑO
		}


		// 5�b��Ɏ�������
		Destroy(gameObject, 5f);
	}

}
