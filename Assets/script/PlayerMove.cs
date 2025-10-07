using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	//速度の設定
	[SerializeField]
	private float speed;
	[SerializeField]
	private float scrollSpeed;
	//リジットボディの確認
	Rigidbody rb;
	//アニメーションのコンポーネントの確認
	//Animator animator;
	private void Start()
	{
		rb = GetComponent<Rigidbody>();

		// Rigidbody の補正（物理で転がらないように）
		rb.useGravity = false;
		rb.constraints = RigidbodyConstraints.FreezeRotation;
	}

	void Update()
	{

		// 入力取得（WASD / 矢印キー）
		float x = Input.GetAxis("Horizontal"); // A(-1) D(+1)
		float y = Input.GetAxis("Vertical");     // ワールド座標XZ平面での移動ベクトル
		Vector3 input = new Vector3(x, y, 0).normalized;
		Vector3 movement = new Vector3(input.x * speed, input.y * speed, scrollSpeed);
		//動いてるか確認
		//bool Run = y!=0 || x!=0;
		// 移動適用
		rb.MovePosition(transform.position + movement * Time.deltaTime);
	}
}
