using UnityEngine;

public class TestEnemy : MonoBehaviour
{
	public Transform target;   // �v���C���[
	public float fallSpeed = 5f; // �����X�s�[�h
	public float moveSpeed = 3f; // �v���C���[�ǐՃX�s�[�h

	private bool hasLanded = false;

	private void Update()
	{
		if (!hasLanded)
		{
			// ���Ɉړ�
			transform.position += Vector3.down * fallSpeed * Time.deltaTime;

			// �n�ʂɓ��B������ǐՂɐ؂�ւ�
			if (transform.position.y <= 0.5f) // �� �n�ʂ̍����ɍ��킹��
			{
				hasLanded = true;
			}
		}
		else
		{
			// �v���C���[�Ɍ������Ĉړ�
			if (target != null)
			{
				Vector3 direction = (target.position - transform.position).normalized;
				transform.position += direction * moveSpeed * Time.deltaTime;
			}
		}
	}
}
