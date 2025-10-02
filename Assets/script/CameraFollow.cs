using UnityEngine;

public class CameraFllow : MonoBehaviour
{
	[SerializeField]
	private  int PlayerMove;
	public Transform player; // プレイヤー
	public Vector3 offset; //カメラの相対位置


	void Start()
	{
		this.player = GetComponent<Transform>();
		//unitychanの情報を取得
		//this.player = GameObject.Find("playerTest");

		// MainCamera(自分自身)とplayerとの相対距離を求める
		offset = transform.position - player.transform.position;

	}

	// Update is called once per frame
	void Update()
	{

		//新しいトランスフォームの値を代入する
		transform.position = player.transform.position + offset;

	}
}
