using UnityEngine;

public class TutorialEnemy : MonoBehaviour
{
	public float fallSpeed = 5f; // 落下スピード
	//public float moveSpeed = 3f; // プレイヤー追跡スピード
	public float frontOffset = 20; //追跡する位置（プレイヤーの前方）
	public float radius = 10f; //半径
	public float rotateSpeed = 2f; //角速度
	public Transform player;
	
	private bool hasLanded = false;
	private Vector3 offset;
	private Transform target;
	private Vector3 circleCenter; //回転の中心
	private float angle = 0f;

	private void Start()
	{
		// シーン上のプレイヤーを自動で取得
		player = GameObject.Find("playerTest").transform;

		if (player != null)
		{
			// プレイヤーの前方位置を計算
			offset = player.forward * frontOffset;
		}
		else
		{
			Debug.LogError("Player not found in scene!");
		}
	}

	private void Update()
	{
if (player == null) return;
		if (!hasLanded)
		{
			// 下に移動
			transform.position += Vector3.down * fallSpeed * Time.deltaTime;

			//メインカメラのY軸を取得
			float cameraY = Camera.main.transform.position.y;
			// プレイヤーの目の前に来たら
			if (transform.position.y <= cameraY) // ← 地面の高さに合わせる
			{
				hasLanded = true;
				circleCenter = player.position + player.forward * frontOffset;
				angle = Random.Range(0f, Mathf.PI * 2f);
				Debug.Log("Start Circle Motion");
			}

		}
		else
		{
			angle += rotateSpeed * Time.deltaTime;
			float x = Mathf.Cos(angle) * radius;
			float z = Mathf.Sin(angle) * radius;
			// 追従しつつ円運動
			transform.position = circleCenter + new Vector3(x,0,z);
			transform.LookAt(player);
		}
	}
}
