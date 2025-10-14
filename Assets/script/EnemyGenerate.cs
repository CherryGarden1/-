using UnityEngine;

public class EnemyGenerate : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	[SerializeField]
	private Transform Player;//�v���C���[��
	[SerializeField]
	private GameObject[] EnemyPrefabs;//�f�ގw��
	[SerializeField]
	private float SpawnDistanse = 10f;//�O������
	[SerializeField]
	private float SpawnHeight = 5f;//�ォ��~���Ă��鍂��

	public event System.Action Generate;

	private int currentIndex = 0; // �ǂ̓G���o�����Ǘ�

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log($"Trigger entered by: {other.name}");
		if (other.CompareTag("Player"))
		{
			//�v���C���[�̑O���A��ɐ���
			Vector3 SpawnPos = Player.position + Player.forward * SpawnDistanse;
			SpawnPos.y += SpawnHeight;

		GenerateEnemy(SpawnPos);
		}
		Generate?.Invoke();


	}


	private void GenerateEnemy(Vector3 CreatePosition)
	{
		if (EnemyPrefabs.Length == 0) return;
		GameObject Prefab = EnemyPrefabs[currentIndex];

		Instantiate(Prefab, CreatePosition, Quaternion.identity);

		// ���̓G�ɐi�߂�i�Ō�܂ł�������ŏ��ɖ߂�j
		currentIndex = (currentIndex + 1) % EnemyPrefabs.Length;
	}
}
