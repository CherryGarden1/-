using UnityEngine;

public class CameraFllow : MonoBehaviour
{
	[SerializeField]
	private  int PlayerMove;
	public Transform player; // �v���C���[
	public Vector3 offset; //�J�����̑��Έʒu


	void Start()
	{
		this.player = GetComponent<Transform>();
		//unitychan�̏����擾
		//this.player = GameObject.Find("playerTest");

		// MainCamera(�������g)��player�Ƃ̑��΋��������߂�
		offset = transform.position - player.transform.position;

	}

	// Update is called once per frame
	void Update()
	{

		//�V�����g�����X�t�H�[���̒l��������
		transform.position = player.transform.position + offset;

	}
}
