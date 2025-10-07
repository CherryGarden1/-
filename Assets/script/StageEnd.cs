using UnityEngine;

public class StageEnd : MonoBehaviour
{
	public event System.Action NextStage;
	private void OnTriggerEnter(Collider other)
	{
		NextStage?.Invoke();
	}
}
