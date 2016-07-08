using UnityEngine;
using UnityEngine.UI;

public class FakeTank : MonoBehaviour
{
	public float m_MaxLifeTime;                  
	public GameObject FakeTankPreb;
  
	private void Start()
	{
		Destroy(gameObject, m_MaxLifeTime);

        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = Color.yellow;
        }
    }
}
