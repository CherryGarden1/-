using UnityEngine;

public class Bullet : MonoBehaviour 
{
	[SerializeField]
	private float bulletSpeed;
	[SerializeField]
	private Rigidbody rb;
	[SerializeField]
	private int damage = 1;

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
	private void OnTriggerEnter(Collider other)
	{
		// �G�ƏՓ˂������m�F
		if (other.CompareTag("Enemy"))
		{
			// �G��HP�������Ă�
			EnemyBase enemy = other.GetComponent<EnemyBase>();
			if (enemy != null)
			{
				enemy.TakeDamage(damage);
			}

			// �e���폜
			Destroy(gameObject);
		}
	}
	}
