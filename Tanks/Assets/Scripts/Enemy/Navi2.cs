using UnityEngine;
using System.Collections;

public class Navi2 : MonoBehaviour {
	NavMeshAgent nav;

	public LayerMask m_TankMask;
	public GameObject m_Flag;
	public GameObject m_TargetArrow;

	// Use this for initialization
	void Start () {
		nav = GetComponent<NavMeshAgent> ();		
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

