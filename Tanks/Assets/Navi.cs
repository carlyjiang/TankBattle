using UnityEngine;
using System.Collections;

public class Navi : MonoBehaviour {

	NavMeshAgent nav;
	GameObject[] players;

	// Use this for initialization
	void Awake () {
	
		players = GameObject.FindGameObjectsWithTag ("Player");

		nav = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		foreach(GameObject p in players)
			nav.SetDestination (p.transform.position);
	}
}
