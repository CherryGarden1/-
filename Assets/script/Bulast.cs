using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Bulast : MonoBehaviour
{
	[SerializeField]
	private float bulletSpeed;
	[SerializeField]
	private Rigidbody rb;
	[SerializeField]
	GameObject firePoint;
	public float explosionRadius = 10f;  // 最初の爆発範囲
	public float chainDelay = 0.2f;      // 連鎖までの遅延
	public int maxChains = 5;            // 最大連鎖回数
	public int Exdamage = 50;           // 連鎖爆発用ダメージ

	// 連鎖済み敵を記録して二重処理を防ぐ
	private HashSet<Transform> explodedEnemies = new HashSet<Transform>();

	[SerializeField]
	GameObject explosionPrefab;

	void Start()
	{
		// 発射方向は生成されたときの forward
		if (rb != null)
		{


			rb.linearVelocity = transform.forward * bulletSpeed; // Unity 6系
																 // rb.velocity = transform.forward * bulletSpeed;   // Unity 2023以前
		}


		// 5秒後に自動消滅
		Destroy(gameObject, 5f);
	}
	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Enemy"))
		{
			//最初の敵を爆破する
			StartCoroutine(TriggerChainExplosion(other.transform, 0));
			Destroy(gameObject);
		}
	}

	private IEnumerator TriggerChainExplosion(Transform originEnemy, int chainLevel)
	{
		if (chainLevel > maxChains) yield break;

		// すでに爆発済みなら飛ばす
		if (explodedEnemies.Contains(originEnemy)) yield break;
		explodedEnemies.Add(originEnemy);


		// 敵が死んで transform が null になる前に座標だけ確保
		Vector3 originPos = originEnemy.position;


		// 爆発エフェクト
		if (explosionPrefab != null)
		{
			Instantiate(explosionPrefab, originPos, Quaternion.identity);
		}

		// 敵を破壊 or ダメージ
		EnemyBase enemy = originEnemy.GetComponent<EnemyBase>();
		if (enemy != null)
		{
			enemy.TakeDamage(Exdamage);
		}

		yield return new WaitForSeconds(chainDelay);

		// 次の連鎖を探索
		int enemyMask = LayerMask.GetMask("Enemy");
		Collider[] hits = Physics.OverlapSphere(originPos, explosionRadius,enemyMask);
		foreach (Collider hit in hits)
		{
			if (hit.CompareTag("Enemy") && hit.transform != originEnemy)
			{
				yield return new WaitForSeconds(chainDelay);
				StartCoroutine(TriggerChainExplosion(hit.transform, chainLevel + 1));
			}
		}
	}
}
