using UnityEngine;

public class ScloolTest : MonoBehaviour
{
	[SerializeField] private GameObject scrollBlockObject;
	[SerializeField] private Transform blockPopPoint;
	[SerializeField] private Vector3 blockMoveForward = new Vector3(0, 0, 1);
	[SerializeField] private int before_block_create_count = 5;

	private Renderer beforeBlockRender;

	void Start()
	{
		// �ŏ��ɕ��ׂ�
		if (scrollBlockObject != null && before_block_create_count > 0)
		{
			Renderer prefabRenderer = scrollBlockObject.GetComponent<Renderer>();
			Vector3 blockSize = prefabRenderer.bounds.size;

			for (int i = 0; i < before_block_create_count; i++)
			{
				Vector3 createPosition = blockPopPoint.position +
										 blockMoveForward.normalized * blockSize.z * i;
				CreateBlock(createPosition);
			}
		}
	}

	private void FixedUpdate()
	{
		if (beforeBlockRender == null) return;

		// �Ō�ɐ��������u���b�N��Bounds���擾
		Bounds lastBounds = beforeBlockRender.bounds;

		// ���̃u���b�N�̐����ʒu�iZ�����Ɉ���i�߂�j
		Vector3 nextPosition = lastBounds.center + blockMoveForward.normalized * lastBounds.size.z;

		// blockPopPoint ���Ō�̃u���b�N�̒��ɂ��Ȃ� �� �ǉ�����
		if (!lastBounds.Contains(blockPopPoint.position))
		{
			CreateBlock(nextPosition);
		}
	}

	private void CreateBlock(Vector3 createPosition)
	{
		GameObject blockObject = Instantiate(scrollBlockObject, createPosition, scrollBlockObject.transform.rotation);

		// �ړ��ƍ폜���s���R���|�[�l���g
		blockObject.AddComponent<AutoDestroy>().time = 10f; // ��������
		blockObject.AddComponent<ObjectTransform>().translate = blockMoveForward;

		beforeBlockRender = blockObject.GetComponent<Renderer>();
	}
}

