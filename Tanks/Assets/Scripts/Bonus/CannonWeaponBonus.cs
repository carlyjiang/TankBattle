using UnityEngine;
using System.Collections;

public class CannonWeaponBonus : MonoBehaviour {
    public int m_FillCount;
    public float m_RenewInterval;

    private bool isActive;
    private float lastTime;

    // Use this for initialization
    void Start () {
        isActive = true;
        lastTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    public void RandomSpawn()
    {
        //Debug.Log(isActive);
        //Debug.Log(Time.time - lastTime);

        if (!isActive && Time.time - lastTime > m_RenewInterval + Random.Range(1, 5))
        {
            lastTime = Time.time;
            this.gameObject.SetActive(true);
            isActive = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);

            TankShooting ts = other.GetComponent<TankShooting>();
            ts.m_SpecialWeapon = 2;
            ts.m_SpecialWeaponCount = m_FillCount;
        }
    }
}
