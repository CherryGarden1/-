using UnityEngine;

public class ObjectTransform : MonoBehaviour
{
	//�X�N���[���̃X�s�[�h
	public Vector3 translate;

	void Update()
	{
		//���[���h���W��ňړ�
		if (translate != Vector3.zero)
		{
			transform.Translate(translate, Space.World);
		}
	}
}
