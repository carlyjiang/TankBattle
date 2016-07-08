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
		nav = GetComponent<NavMeshAgent> ();
	}

	void findPlayer()
    {	
		player = GameObject.FindGameObjectWithTag("Player");
	}
		
	// Update is called once per frame
	void Update () {
        //nav.speed = 4;
        m_Flag = GameObject.FindGameObjectWithTag("Flag");

        Collider[] c = Physics.OverlapSphere(transform.position, 30, m_TankMask);        

        for (int i = 0; i < c.Length; i++)
        {
            Transform PlayerTransform = c[i].GetComponent<Transform>();

            if (PlayerTransform)
            {
                nav.SetDestination(PlayerTransform.position);
                ArrowCheckRange(c[i]);
                return;
            }
        }
        
		m_TargetArrow.SetActive (false);
        if (m_Flag)
        {
            nav.SetDestination(m_Flag.transform.position);
        }
    }

    void ArrowCheckRange(GameObject o)
    {
        if (o.tag != "Flag")
            m_TargetArrow.SetActive(true);
        else
            m_TargetArrow.SetActive(false);
    }

    void ArrowCheckRange(Collider o)
    {
        if (o.tag != "Flag")
            m_TargetArrow.SetActive(true);
        else
            m_TargetArrow.SetActive(false);
    }
}

