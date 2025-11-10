using UnityEngine;

public class EnemyBase : MonoBehaviour
{
	public int hp = 10;
	public GameObject explosionPrefab;
	public virtual void TakeDamage(int damage)
	{
		hp -= damage;
		if (hp <= 0) Destroy(gameObject);
	}
	void Explode()
	{
		if (explosionPrefab)
		{ 
			Instantiate(explosionPrefab, transform.position, Quaternion.identity);
		}
		Destroy(gameObject);
	}
}
