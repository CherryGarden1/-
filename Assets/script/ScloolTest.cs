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
		// 最初に並べる
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

		// 最後に生成したブロックのBoundsを取得
		Bounds lastBounds = beforeBlockRender.bounds;

		// 次のブロックの生成位置（Z方向に一つ分進める）
		Vector3 nextPosition = lastBounds.center + blockMoveForward.normalized * lastBounds.size.z;

		// blockPopPoint が最後のブロックの中にいない → 追加生成
		if (!lastBounds.Contains(blockPopPoint.position))
		{
			CreateBlock(nextPosition);
		}
	}

	private void CreateBlock(Vector3 createPosition)
	{
		GameObject blockObject = Instantiate(scrollBlockObject, createPosition, scrollBlockObject.transform.rotation);

		// 移動と削除を行うコンポーネント
		blockObject.AddComponent<AutoDestroy>().time = 10f; // 少し長め
		blockObject.AddComponent<ObjectTransform>().translate = blockMoveForward;

		beforeBlockRender = blockObject.GetComponent<Renderer>();
	}
}

