using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FrozenField : MonoBehaviour {
    public float m_MaxLifeTime = 30f;
    private float startTime;
    public float m_ExplosionRadius = 10f;
	public float EnemyMaxSpeed = 4f;


    // Use this for initialization
    void Start () {
        Destroy(gameObject, m_MaxLifeTime);
    }

    // Update is called once per frame
   
    private void Update()
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius);
        

        foreach (Collider c in colliders)
        {
            TankMovement movement = c.GetComponent<TankMovement>();

            if (movement)
            {
                float radio = Vector3.Distance(transform.position, c.transform.position) / m_ExplosionRadius;
                movement.frozenSpeed = Mathf.Max(0.1f, radio);
            }
			NavMeshAgent nv = c.GetComponent<NavMeshAgent> ();
			if (nv) {
				float radio = Vector3.Distance(transform.position, c.transform.position) / m_ExplosionRadius;
				nv.speed = Mathf.Max(0.1f, radio * EnemyMaxSpeed);
				//print (nv.speed);
			} else {
				//Debug.Log ("Not Get Enemy in Frozen Field");
			}
				
        }
    }


    void OnDestroy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius);

        foreach (Collider c in colliders)
        {
            TankMovement movement = c.GetComponent<TankMovement>();

            if (movement)
            {
                float radio = Vector3.Distance(transform.position, c.transform.position) / m_ExplosionRadius;
                movement.frozenSpeed = 1f;
            }
        }
    }
}

