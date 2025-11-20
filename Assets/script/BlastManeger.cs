using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class BlastManeger : MonoBehaviour
{
	public float explosionRadius = 10f;
	public float chainDelay = 0.2f;
	public int maxChains = 30;
	public int chainDamage = 50;


	private HashSet<EnemyBase> exploded = new HashSet<EnemyBase>();

	void OnEnable()
	{
		EnemyBase.OnEnemyExploded += HandleExplosion;
	}

	void OnDisable()
	{
		EnemyBase.OnEnemyExploded -= HandleExplosion;
	}

	void HandleExplosion(Vector3 pos, EnemyBase origin)
	{
		if (exploded.Contains(origin)) return;

		exploded.Add(origin);
		StartCoroutine(ChainRoutine(pos, 1));
	}

	IEnumerator ChainRoutine(Vector3 originPos, int level)
	{
		if (level > maxChains) yield break;

		yield return new WaitForSeconds(chainDelay);

		// Ÿ‚ÉŠª‚«‚Ü‚ê‚é“G‚ğ’Tõ
		Collider[] hits = Physics.OverlapSphere(originPos, explosionRadius, LayerMask.GetMask("Enemy"));

		foreach (Collider hit in hits)
		{
			EnemyBase enemy = hit.GetComponent<EnemyBase>();

			if (enemy != null && !exploded.Contains(enemy))
			{
				enemy.TakeDamage(chainDamage);

				exploded.Add(enemy);
				// ‚³‚ç‚ÉŸ‚Ì˜A½‚Ö
				StartCoroutine(ChainRoutine(enemy.transform.position, level + 1));
			}
		}
	}
}

