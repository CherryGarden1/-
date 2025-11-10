using UnityEngine;

public class Bulast : MonoBehaviour
{
	[SerializeField]
	private float bulletSpeed;
	[SerializeField]
	private Rigidbody rb;
	[SerializeField]
	GameObject FirePoint;
	public float explosionRadius = 10f;  // 最初の爆発範囲
	public float chainDelay = 0.2f;      // 連鎖までの遅延
	public int maxChains = 5;            // 最大連鎖回数
	public float Exdamage = 50f;           // 連鎖爆発用ダメージ
			
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

		// 爆発エフェクト
		Instantiate(Resources.Load<GameObject>("ExplosionEffect"), originEnemy.position, Quaternion.identity);

		// 敵を破壊 or ダメージ
		EnemyBase enemy = originEnemy.GetComponent<EnemyBase>();
		if (enemy != null)
		{
			enemy.TakeDamage(Exdamage);
		}

		// 次の連鎖を探索
		Collider[] hits = Physics.OverlapSphere(originEnemy.position, explosionRadius);
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
