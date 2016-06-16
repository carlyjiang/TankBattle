using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FrozenField : MonoBehaviour {
    public float m_MaxLifeTime = 30f;
    private float startTime;
    public float m_ExplosionRadius = 10f;


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
                //Debug.Log(radio);
                movement.frozenSpeed = Mathf.Max(0.1f, radio);
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

