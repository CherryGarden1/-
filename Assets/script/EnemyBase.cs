using System;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
	public int hp = 10;
	//public int exHp = 50;
	public static event Action<Vector3, EnemyBase> OnEnemyExploded;
	public GameObject explosionPrefab;
	public virtual void TakeDamage(int damage)
	{
		hp -= damage;
		if (hp <= 0)
		{
			Explode();
		} 
	}



	void Explode()
	{
		if (explosionPrefab)
		{ 
			Instantiate(explosionPrefab, transform.position, Quaternion.identity);
		}

		//ƒCƒxƒ“ƒg”­‰Î
		OnEnemyExploded?.Invoke(transform.position,this);

		Destroy(gameObject);
	}
}
