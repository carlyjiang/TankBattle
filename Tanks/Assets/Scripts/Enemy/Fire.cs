using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Fire : MonoBehaviour {

	public LayerMask m_TankMask;
	public Rigidbody m_Shell;  
	public GameObject m_Field;
	public static List<Vector3> m_FieldPositions = new List<Vector3>();
	public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
	public AudioSource m_ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
	public AudioClip m_FireClip;                // Audio that plays when each shot is fired.
	public float m_LaunchForce = 15f;
	//public float timeBetweenAttack = 1f;
	bool playerInRange;
	private bool isFired;
	public float fireRate = 2f;
	private float nextFire = 0f;


	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
		Collider[] c = Physics.OverlapSphere(transform.position, 10, m_TankMask);

		for (int i = 0; i< c.Length; i++){
			
			Transform PlayerTransform = c[i].GetComponent<Transform> ();

			if (!PlayerTransform) {
				continue;
			}

			if (!isFired && Time.time > nextFire ) {
				nextFire = Time.time + fireRate;
				Fight ();
			}
		}
	}


	private void Fight(){

		isFired = true;
		
		Rigidbody shellInstance =
			Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

		shellInstance.velocity = m_LaunchForce  * m_FireTransform.forward;

		//System.Threading.Thread.Sleep (3000);
		isFired = false;
	}
}
