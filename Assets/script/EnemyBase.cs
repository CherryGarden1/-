using UnityEngine;

public class EnemyBase : MonoBehaviour
{
	public int hp = 10;
	public virtual void TakeDamage(int damage)
	{
		hp -= damage;
		if (hp <= 0) Destroy(gameObject);
	}
}
