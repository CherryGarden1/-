using UnityEngine;

public class TestEnemy : MonoBehaviour
{
	public Transform target;   // プレイヤー
	public float fallSpeed = 5f; // 落下スピード
	public float moveSpeed = 3f; // プレイヤー追跡スピード

	private bool hasLanded = false;

	private void Update()
	{
		if (!hasLanded)
		{
			// 下に移動
			transform.position += Vector3.down * fallSpeed * Time.deltaTime;

			// 地面に到達したら追跡に切り替え
			if (transform.position.y <= 0.5f) // ← 地面の高さに合わせる
			{
				hasLanded = true;
			}
		}
		else
		{
			// プレイヤーに向かって移動
			if (target != null)
			{
				Vector3 direction = (target.position - transform.position).normalized;
				transform.position += direction * moveSpeed * Time.deltaTime;
			}
		}
	}
}
