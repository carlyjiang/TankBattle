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
    public GameObject FrozenFieldPreb;      


    private void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        Vector3 explosionPosition = transform.position;
        explosionPosition.y = 0.1f;
        GameObject frozenField = Instantiate(FrozenFieldPreb, explosionPosition, new Quaternion(0f, 0f, 0f, 0f)) as GameObject;

        /*
        
        // Unparent the particles from the shell.
        m_ExplosionParticles.transform.parent = null;

        // Play the particle system.
        m_ExplosionParticles.Play();

        // Play the explosion sound effect.
        //m_ExplosionAudio.Play();

        // Once the particles have finished, destroy the gameobject they are on.
        //Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);
		Destroy(m_ExplosionParticles.gameObject, 2f);

        */

        // Destroy the shell.

        Destroy(gameObject);
    }
}


