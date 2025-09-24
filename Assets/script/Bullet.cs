using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField]
	private float bulletSpeed;
	[SerializeField]
	private Rigidbody rb;

	void Start()
	{
		// ”­Ë•ûŒü‚Í¶¬‚³‚ê‚½‚Æ‚«‚Ì forward
		if (rb != null)
		{
			rb.linearVelocity = transform.forward * bulletSpeed; // Unity 6Œn
																 // rb.velocity = transform.forward * bulletSpeed;   // Unity 2023ˆÈ‘O
		}


		// 5•bŒã‚É©“®Á–Å
		Destroy(gameObject, 5f);
	}

}
