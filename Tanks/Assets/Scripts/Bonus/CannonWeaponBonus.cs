using UnityEngine;
using System.Collections;

public class CannonWeaponBonus : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //other.gameObject.SetActive(false);
            Debug.Log("Hit Player");
            this.gameObject.SetActive(false);

            TankShooting ts = other.GetComponent<TankShooting>();
            ts.m_SpecialWeapon = 2;
            ts.m_SpecialWeaponCount = 5;
        }
        else
        {
            Debug.Log("Hit other things");
        }
    }
}
