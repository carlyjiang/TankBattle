using UnityEngine;
using UnityEngine.UI;

public class OilStorageHealth : MonoBehaviour {

	// Use this for initialization
	public float m_StartingHealth = 1f;
	public GameObject m_ExplosionPrefab;
	public float m_ExplosionForce = 1000f;
	public float m_ExplosionRadius = 5f;  
	private ParticleSystem m_ExplosionParticles;   
	private bool m_Dead;          
	private float m_CurrentHealth;  
	//private AudioSource m_ExplosionAudio;  


	private void Awake()
	{
		m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
		m_ExplosionParticles.gameObject.SetActive(false);
	}

	private void OnEnable()
	{   
		m_CurrentHealth = m_StartingHealth;
		m_Dead = false;

	}

	public void TakeDamage(float amount)
	{
		// Adjust the current health, update the UI based on the new health and check whether or not the tank is dead.
		m_CurrentHealth -= amount;

		if (m_CurrentHealth <= 0f && !m_Dead)
		{
			OnDeath();
		}
	}
		
	private void OnDeath()
	{
		// Play the effects for the death
		m_Dead = true;
		m_ExplosionParticles.transform.position = transform.position;
		m_ExplosionParticles.gameObject.SetActive(true);
		m_ExplosionParticles.Play();
		//m_ExplosionAudio.Play();
		gameObject.SetActive(false);
		OnTriggerEnter ();
	}

	private void OnTriggerEnter()
	{
        /*
		// Find all the items in an area around the shell and damage them.
		Collider[] collider = Physics.OverlapSphere(transform.position, m_ExplosionRadius);

		for (int i = 0; i < collider.Length; i++)
		{
			Rigidbody targetRigidbody = collider[i].GetComponent<Rigidbody>();

			if(!targetRigidbody)
			{
				continue;
			}

			targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);
			OilStorageHealth oilhealth = targetRigidbody.GetComponent<OilStorageHealth>();

			if (oilhealth) 
			{
				//float damege = CalculateDamage(targetRigidbody.position);
				oilhealth.TakeDamage(1);
			}
		}

		// Unparent the particles from the shell.
		m_ExplosionParticles.transform.parent = null;

		// Play the particle system.
		m_ExplosionParticles.Play();

		// Play the explosion sound effect.
		//m_ExplosionAudio.Play();

		// Once the particles have finished, destroy the gameobject they are on.
		Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);

		// Destroy the shell.
		Destroy(gameObject);
        */
	}

}
