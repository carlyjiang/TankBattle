using UnityEngine;
using System.Collections;

public class Navi2 : MonoBehaviour {
	NavMeshAgent nav;
	//GameObject player;
	public LayerMask m_TankMask;
	public GameObject m_Flag;
	public GameObject m_TargetArrow;

	// Use this for initialization
	void Start () {
		nav = GetComponent<NavMeshAgent> ();
		//player = GameObject.FindGameObjectWithTag("Player");
		m_Flag = GameObject.FindGameObjectWithTag("Flag");
		nav.SetDestination (m_Flag.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
        m_Flag = GameObject.FindGameObjectWithTag("Flag");
        Collider[] c = Physics.OverlapSphere(transform.position, 30, m_TankMask);
        

        for (int i = 0; i < c.Length; i++)
		{
			Transform PlayerTransform = c[i].GetComponent<Transform>();

			if (PlayerTransform)
			{
				//Debug.Log("player Position");
				nav.SetDestination(PlayerTransform.position);
				m_TargetArrow.SetActive (true);
				return;
			}
		}

		m_TargetArrow.SetActive (false);
		nav.SetDestination(m_Flag.transform.position);
	}
}
