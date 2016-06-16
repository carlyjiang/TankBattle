using UnityEngine;
using System.Collections;

public class FrozenWeaponBonus : MonoBehaviour {
    public int m_FillCount;
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
            this.gameObject.SetActive(false);

            TankShooting ts = other.GetComponent<TankShooting>();
            ts.m_SpecialWeapon = 1;
            ts.m_SpecialWeaponCount = m_FillCount;
        }
    }
}
