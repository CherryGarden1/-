using UnityEngine;

public class AimAndShot : MonoBehaviour
{
	public Transform shipTransform; // �@��
	public RectTransform crosshairUI;      // UI上のクロスヘア
	public float turnSpeed = 2f;
	public GameObject bulletPrefab;
	public Transform firePoint;
	public Camera mainCamera;              // メインカメラ

	void Update()
	{
		//スクリーン座標に変換
		Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(null, crosshairUI.position);

		// === スクリーン座標 → レイ ===
		Ray ray = mainCamera.ScreenPointToRay(screenPos);
		Vector3 targetPos;
		// レイキャストでヒットを確認（敵や地形があるならそこをターゲットにする）
		if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
		{
			targetPos = hit.point;
		}
		else
		{
			// ヒットしなければ一定距離先をターゲットにする
			targetPos = ray.origin + ray.direction * 1000f;
		}

		// === 機体をターゲット方向に向ける ===
		Vector3 dir = (targetPos - shipTransform.position).normalized;
		if (dir.sqrMagnitude > 0.001f)
		{
			Quaternion targetRot = Quaternion.LookRotation(dir);
			shipTransform.rotation = Quaternion.Slerp(shipTransform.rotation, targetRot, Time.deltaTime * turnSpeed);
		}

		// === マウス左クリックで弾を発射 ===
		if (Input.GetMouseButtonDown(0))
		{
			ShootAt(targetPos);
		}
	}

	void ShootAt(Vector3 target)
	{
		//弾を生成
		GameObject b = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
		//クロスヘアに向けて飛ばす
		Vector3 velocityDir = (target - firePoint.position).normalized;
		Rigidbody rb = b.GetComponent<Rigidbody>();
		if (rb != null)
		{
			rb.linearVelocity = velocityDir * 200f;
		}
	}
}
