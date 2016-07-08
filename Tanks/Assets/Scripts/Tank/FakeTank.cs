using UnityEngine;
using UnityEngine.UI;

public class FakeTank : MonoBehaviour
{
            
	public float m_MaxLifeTime = 100f;                  
	public int shellType; // 0 for normal shell, 1 for frozen shell, 2 for cannon shell       
	public GameObject FakeTankPreb;
  


	private void Start()
	{
		Destroy(gameObject, m_MaxLifeTime);
	}


	private void OnTriggerEnter(Collider other)
	{
		Vector3 explosionPosition = transform.position;
		explosionPosition.y = 0.15f;
		GameObject FakeTankField = Instantiate(FakeTankPreb, explosionPosition, new Quaternion(0f, 0f, 0f, 0f)) as GameObject;

		Destroy(gameObject);
	}
}

