using UnityEngine;

public class AimAndShot : MonoBehaviour
{
	public Transform shipTransform; // �@��
	public Transform crosshairWorldObject; // ��قǂ̃��[���h��ԃN���X�w�A
	public float turnSpeed = 2f;
	public GameObject bulletPrefab;
	public Transform firePoint;

	void Update()
	{
		Vector3 dir = (crosshairWorldObject.position - shipTransform.position).normalized;
		dir.y = 0f; // �K�v�Ȃ�㉺��]��}��
		if (dir.sqrMagnitude > 0.001f)
		{
			Quaternion targetRot = Quaternion.LookRotation(dir);
			shipTransform.rotation = Quaternion.Slerp(shipTransform.rotation, targetRot, Time.deltaTime * turnSpeed);
		}

		if (Input.GetMouseButtonDown(0))
		{
			ShootAt(crosshairWorldObject.position);
		}
	}

	void ShootAt(Vector3 target)
	{
		GameObject b = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
		Vector3 velocityDir = (target - firePoint.position).normalized;
		// �e��Rigidbody������ꍇ
		Rigidbody rb = b.GetComponent<Rigidbody>();
		if (rb != null) rb.linearVelocity = velocityDir * 200f; // ����
	}
}
