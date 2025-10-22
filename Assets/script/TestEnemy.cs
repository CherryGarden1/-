using UnityEngine;

public class TestEnemy : MonoBehaviour
{
	public float fallSpeed = 5f; // �����X�s�[�h
	public float moveSpeed = 3f; // �v���C���[�ǐՃX�s�[�h
	public float frontOffset = 20; //�ǐՂ���ʒu�i�v���C���[�̑O���j
	private bool hasLanded = false;

	private Transform target;

	private void Start()
	{
		target = GameObject.Find("playerTest").transform;
	}
	private void Update()
	{
		Transform target = GameObject.Find("playerTest").transform;
		if (!hasLanded)
		{
			// ���Ɉړ�
			transform.position += Vector3.down * fallSpeed * Time.deltaTime;

			// �n�ʂɓ��B������ǐՂɐ؂�ւ�
			if (transform.position.y <= 0.5f) // �� �n�ʂ̍����ɍ��킹��
			{
				hasLanded = true;
				Debug.Log("tracking");
			}
	
		}
		else
		{

			// �v���C���[�̑O���Ɋ�Â����ڕW�ʒu���v�Z
			Vector3 forwardDir = target.TransformDirection(Vector3.forward); // ���f���̑O��(Z���)
			Vector3 targetPos = target.position + target.forward * frontOffset;

			// Y���͓G�̍������ێ�
			targetPos.y = transform.position.y;

			// �ڕW�ʒu�Ɍ������Ĉړ�
			Vector3 direction = (targetPos - transform.position).normalized;
			transform.position += direction * moveSpeed * Time.deltaTime;

			// �������v���C���[�̑O���ɍ��킹��
			if (direction.sqrMagnitude > 0.001f)
			{
				Quaternion targetRot = Quaternion.LookRotation(direction);
				transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 5f);
			}
		}
		
	}
}
