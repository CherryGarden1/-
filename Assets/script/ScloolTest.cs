using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ScloolTest : MonoBehaviour
{
	[SerializeField] private GameObject scrollBlockObject;//�f�ގw��
	[SerializeField] private Transform blockPopPoint;//�����ʒu
	[SerializeField] private Vector3 blockMoveForward = new Vector3(0, 0, 1);//���ԕ���
	[SerializeField] private int before_block_create_count = 2;//�ŏ��̐�
	[SerializeField] private Vector3 playerOffset;


	private Renderer beforeBlockRender;

	void Start()
	{
		// �ŏ��ɕ��ׂ�
		if (scrollBlockObject != null && before_block_create_count > 0)
		{
			//�f�ނ̑傫�����擾
			Renderer prefabRenderer = scrollBlockObject.GetComponent<Renderer>();
			Vector3 blockSize = prefabRenderer.bounds.size;

			Vector3 createPosition = blockPopPoint.position + playerOffset;
			//before_block_create_count�u���b�N����ׂ�blockPopPoint����ɔz�u
			for (int i = 0; i < before_block_create_count; i++)
			{
				if (i == 0)
				{
					// �ŏ���1�� �� �v���C���[�̐^��
					Vector3 firstPos = blockPopPoint.position�@+playerOffset;
					CreateBlock(firstPos);
				}
				else
				{
					//2�ڈȍ~�͒��O�̃u���b�N�̃T�C�Y������ɕ��ׂ�
					Bounds lastBounds = beforeBlockRender.bounds;
					Vector3 nextPosition = lastBounds.center + blockMoveForward.normalized * lastBounds.size.z;
					CreateBlock(createPosition);
				}
				
			}
		}
	}

	private void Update()
	{
		//�u���b�N���Ȃ���Ή������Ȃ�
		if (beforeBlockRender == null) return;

		// �Ō�ɐ��������u���b�N��Bounds���擾
		Bounds lastBounds = beforeBlockRender.bounds;

		// ���̃u���b�N�̐����ʒu�iZ�����Ɉ���i�߂�j
		Vector3 nextPosition = lastBounds.center + blockMoveForward.normalized * lastBounds.size.z;

		// blockPopPoint ���Ō�̃u���b�N�̒��ɂ��Ȃ� �� �ǉ�����
		if (!lastBounds.Contains(blockPopPoint.position))
		{
		
		}
	}

	private void CreateBlock(Vector3 createPosition)
	{
		GameObject blockObject = Instantiate(scrollBlockObject, createPosition, scrollBlockObject.transform.rotation);

		// �ړ��ƍ폜���s���R���|�[�l���g
		blockObject.AddComponent<AutoDestroy>().time = 30f; // ��������
		//blockObject.AddComponent<ObjectTransform>().translate = blockMoveForward;
		blockObject.transform.Translate(blockMoveForward, Space.World);
		beforeBlockRender = blockObject.GetComponent<Renderer>();
		blockObject.GetComponentInChildren<StageEnd>().NextStage += OnNextStage;
	}

	private void OnNextStage()
	{
		Debug.Log("NextStage");
		// �Ō�ɐ��������u���b�N��Bounds���擾
		Bounds lastBounds = beforeBlockRender.bounds;

		// ���̃u���b�N�̐����ʒu�iZ�����Ɉ���i�߂�j
		Vector3 nextPosition = lastBounds.center + blockMoveForward.normalized * lastBounds.size.z;

		CreateBlock(nextPosition);

	}
}

