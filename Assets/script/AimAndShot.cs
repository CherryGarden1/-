using UnityEngine;

public class AimAndShot : MonoBehaviour
{
	public Transform shipTransform; //自機
	public RectTransform crosshairUI;      // UI上のクロスヘア
	public float turnSpeed = 2f;  //振り向き速度
	public GameObject bulletPrefab;　//弾素材
	public Transform firePoint; //発射位置
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
			//Y軸のみ回転
			Vector3 euler = targetRot.eulerAngles;
			shipTransform.rotation = Quaternion.Slerp(shipTransform.rotation,
				Quaternion.Euler(shipTransform.eulerAngles.x, euler.y, shipTransform.eulerAngles.z),
				 Time.deltaTime * turnSpeed);
		}

		// === マウス左クリックで弾を発射 ===
		if (Input.GetMouseButtonDown(0))
		{
			ShootAt(targetPos);
		}
	}

	void ShootAt(Vector3 target)
	{
		Vector3 screenPos = crosshairUI.position;
		Ray ray = mainCamera.ScreenPointToRay(screenPos);
		Vector3 targetPos;

		if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
			targetPos = hit.point;
		else
			targetPos = ray.origin + ray.direction * 1000f;
		//弾を生成
		GameObject b = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		//クロスヘアに向けて飛ばす
		Vector3 velocityDir = (target - firePoint.position).normalized;
		Rigidbody rb = b.GetComponent<Rigidbody>();
		if (rb != null)
		{
			rb.useGravity = false; // 重力を無効化


			rb.linearVelocity = velocityDir * 200f;
			//rb.linearVelocity = velocityDir * 200f;
		}
	}
}
