using UnityEngine;

public class CameraFllow : MonoBehaviour
{

	[SerializeField] private Transform player;   // �v���C���[�@��
	[SerializeField] private Vector3 offset = new Vector3(0, 3f, -10f);
	[SerializeField] private float smoothSpeed = 5f;

	void LateUpdate()
	{
		if (player == null) return;

		// �ڕW�ʒu�i�v���C���[���猩�Č�끕��ɃI�t�Z�b�g�j
		Vector3 targetPosition = player.position + offset;

		// �J�������X���[�Y�ɒǏ]
		float t = Mathf.Clamp01(smoothSpeed * Time.deltaTime);
		transform.position = Vector3.Lerp(transform.position, targetPosition, t);

		// ��������̂́u�v���C���[�̏����O���v
		
		transform.LookAt(player.position);

	}
}


