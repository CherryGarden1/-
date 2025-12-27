using System;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
	public int hp = 10;
	//public int exHp = 50;
	//BlastManagerが受け取るイベント　
	public static event Action<Vector3, EnemyBase> OnEnemyExploded;
	public GameObject explosionPrefab;
	public virtual void TakeDamage(int damage, bool isBlustDamege = false)
	{
		hp -= damage;
		if (hp <= 0)
		{
			Die(isBlustDamege);
		} 
	}



	private void Die(bool isBlustDamege)
	{
		Explode();

		if(isBlustDamege)
		{
			//連鎖爆発起動
			GetComponent<ChainExplosion>()?.StartChain();
		}
		Destroy(gameObject);
	}

		void Explode()
	{
		if (explosionPrefab)
		{ 
			Instantiate(explosionPrefab, transform.position, Quaternion.identity);
		}
	
		//イベント発火
		OnEnemyExploded?.Invoke(transform.position,this);
	
		
	}
}
