using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	//���x�̐ݒ�
	[SerializeField]
	private float speed;
	[SerializeField]
	private float scrollSpeed;
	//���W�b�g�{�f�B�̊m�F
	Rigidbody rb;
	//�A�j���[�V�����̃R���|�[�l���g�̊m�F
	//Animator animator;
	private void Start()
	{
		rb = GetComponent<Rigidbody>();

		// Rigidbody �̕␳�i�����œ]����Ȃ��悤�Ɂj
		rb.useGravity = false;
		rb.constraints = RigidbodyConstraints.FreezeRotation;
	}

	void Update()
	{

		// ���͎擾�iWASD / ���L�[�j
		float x = Input.GetAxis("Horizontal"); // A(-1) D(+1)
		float y = Input.GetAxis("Vertical");     // ���[���h���WXZ���ʂł̈ړ��x�N�g��
		Vector3 input = new Vector3(x, y, 0).normalized;
		Vector3 movement = new Vector3(input.x * speed, input.y * speed, scrollSpeed);
		//�����Ă邩�m�F
		//bool Run = y!=0 || x!=0;
		// �ړ��K�p
		rb.MovePosition(transform.position + movement * Time.deltaTime);
	}
}
