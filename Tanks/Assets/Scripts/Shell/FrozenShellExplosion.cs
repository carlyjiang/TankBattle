using UnityEngine;
using UnityEngine.UI;

public class FrozenShellExplosion : MonoBehaviour
{
    public ParticleSystem m_ExplosionParticles;       
    public AudioSource m_ExplosionAudio;              
    public float m_MaxDamage;                  
    public float m_ExplosionForce;            
    public float m_MaxLifeTime = 1f;                  
    public float m_ExplosionRadius;
    public int shellType; // 0 for normal shell, 1 for frozen shell, 2 for cannon shell             


    private void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        // Find all the tanks in an area around the shell and damage them.		
        Collider[] collider = Physics.OverlapSphere(transform.position, m_ExplosionRadius);

        for (int i = 0; i < collider.Length; i++)
        {
            Rigidbody targetRigidbody = collider[i].GetComponent<Rigidbody>();
            
            if(!targetRigidbody)
            {
                continue;
            }

            targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);

            TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();
			OilStorageHealth oilhealth = targetRigidbody.GetComponent<OilStorageHealth>();
			HouseHealth house1 = targetRigidbody.GetComponent<HouseHealth>();

            if (targetHealth) 
			{
				float damege = CalculateDamage(targetRigidbody.position);
				targetHealth.TakeDamage(damege);	
			}
			if (oilhealth) 
			{
				float damege = CalculateDamage(targetRigidbody.position);
				oilhealth.TakeDamage(damege);
			}
			if (house1) 
			{
				float damege = CalculateDamage(targetRigidbody.position);
				house1.TakeDamage(damege);
			}
        }

        // Unparent the particles from the shell.
        m_ExplosionParticles.transform.parent = null;

        // Play the particle system.
        m_ExplosionParticles.Play();

        // Play the explosion sound effect.
        //m_ExplosionAudio.Play();

        // Once the particles have finished, destroy the gameobject they are on.
        //Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);
		Destroy(m_ExplosionParticles.gameObject, 2f);

        // Destroy the shell.
        Destroy(gameObject);
    }


    public float CalculateDamage(Vector3 targetPosition)
    {
        // Calculate the amount of damage a target should take based on it's position.

        // Create a vector from the shell to the target.
        Vector3 explosionToTarget = targetPosition - transform.position;

        // Calculate the distance from the shell to the target.
        float explosionDistance = explosionToTarget.magnitude;

        // Calculate the proportion of the maximum distance (the explosionRadius) the target is away.
        float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;

        // Calculate damage as this proportion of the maximum possible damage.
        float damage = relativeDistance * m_MaxDamage;

        // Make sure that the minimum damage is always 0.
        damage = Mathf.Max(0f, damage);

        return damage;
    }
}


