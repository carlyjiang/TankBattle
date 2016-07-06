using UnityEngine;
using System.Collections;

public class Navi : MonoBehaviour {
	NavMeshAgent nav;
	GameObject player;
    public LayerMask m_TankMask;
    public GameObject m_Flag;
    public GameObject m_TargetArrow;

    // Use this for initialization
    void Awake () 
    {
		findPlayer();
		nav = GetComponent<NavMeshAgent>();
        m_TargetArrow.SetActive(false);

        m_Flag = GameObject.FindGameObjectWithTag("Flag");
	}

	void findPlayer()
    {	
		player = GameObject.FindGameObjectWithTag("Player");
	}
		
	// Update is called once per frame
	void Update () {
		//nav.speed = 4;

        Collider[] c = Physics.OverlapSphere(transform.position, 30, m_TankMask);        

        for (int i = 0; i < c.Length; i++)
        {
            Transform PlayerTransform = c[i].GetComponent<Transform>();

            if (PlayerTransform)
            {
                //Debug.Log("player Position");
                m_TargetArrow.SetActive(true);
                nav.SetDestination(PlayerTransform.position);
                return;
            }
        }

        m_TargetArrow.SetActive(false);
        nav.SetDestination(m_Flag.transform.position);
    }
}
