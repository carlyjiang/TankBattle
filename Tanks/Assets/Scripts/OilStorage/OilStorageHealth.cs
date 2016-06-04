﻿using UnityEngine;
using UnityEngine.UI;

public class OilStorageHealth : MonoBehaviour {

	// Use this for initialization
	public float m_StartingHealth = 1f;
	public GameObject m_ExplosionPrefab;
	private ParticleSystem m_ExplosionParticles;   
	private bool m_Dead;          
	private float m_CurrentHealth;  
	private AudioSource m_ExplosionAudio;  


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
	}

}
