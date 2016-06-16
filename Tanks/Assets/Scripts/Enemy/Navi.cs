using UnityEngine;
using System.Collections;

public class Navi : MonoBehaviour {

	NavMeshAgent nav;
	GameObject player;

	// Use this for initialization
	void Awake () {

		findPlayer();
		nav = GetComponent<NavMeshAgent> ();
	}
	void findPlayer(){
		
		player = GameObject.FindGameObjectWithTag("Player");
	}
		
	// Update is called once per frame
	void Update () {
		//nav.speed = 4;
		if (player != null) {
			nav.SetDestination (player.transform.position);
		}
	}
}
