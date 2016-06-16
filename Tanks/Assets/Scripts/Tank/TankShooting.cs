using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TankShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;              // Used to identify the different players.
    public Rigidbody m_Shell;                   // Prefab of the shell.
    public Rigidbody m_FrozonShell;                   // Prefab of the shell.
    public Rigidbody m_CannonShell;                   // Prefab of the shell.
    public GameObject m_Field;
    public static List<Vector3> m_FieldPositions = new List<Vector3>();
    public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
    public Slider m_AimSlider;                  // A child of the tank that displays the current launch force.
    public AudioSource m_ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
    public AudioClip m_ChargingClip;            // Audio that plays when each shot is charging up.
    public AudioClip m_FireClip;                // Audio that plays when each shot is fired.
    public float m_MinLaunchForce = 15f;        // The force given to the shell if the fire button is not held.
    public float m_MaxLaunchForce = 30f;        // The force given to the shell if the fire button is held for the max charge time.
    public float m_MaxChargeTime = 0.75f;       // How long the shell can charge for before it is fired at max force.
    public GameObject m_AimCross;


    private string m_FireButton;                // The input axis that is used for launching shells.
    private float m_CurrentLaunchForce;         // The force that will be given to the shell when the fire button is released.
    private float m_ChargeSpeed;                // How fast the launch force increases, based on the max charge time.
    private bool m_Fired;                       // Whether or not the shell has been launched with this button press.
    private Vector3 m_AimCrossOriginalPositionOffset;

    private ShootButton shootButton;            // add touch screen shooting button

    public int m_SpecialWeapon; // 0 for none, 1 for cannon shell, 2 for frozen shell
    public int m_SpecialWeaponCount = 0;


    private void OnEnable()
    {
        // When the tank is turned on, reset the launch force and the UI
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
        m_AimCrossOriginalPositionOffset = new Vector3(
            0f,
            m_AimSlider.gameObject.transform.position.y - m_AimCross.gameObject.transform.position.y,
            0f
            );
    }


    private void Start()
    {
        m_FireButton = "Fire" + m_PlayerNumber;
        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
        shootButton = GameObject.FindGameObjectWithTag("ShootButton").GetComponent<ShootButton>();
    }


    private void Update()
    {
        //m_AimSlider.value = m_MinLaunchForce;

        m_AimCross.gameObject.transform.position = m_AimSlider.gameObject.transform.position - m_AimCrossOriginalPositionOffset;

        if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired)
        {
            m_CurrentLaunchForce = m_MaxLaunchForce;
            Fire();
        }
        else if (Input.GetButtonDown(m_FireButton) || shootButton.isKeyDown())
        {
            m_Fired = false;
            m_CurrentLaunchForce = m_MinLaunchForce;

            m_ShootingAudio.clip = m_ChargingClip;
            m_ShootingAudio.Play();
        }
        else if ((Input.GetButton(m_FireButton) || shootButton.IsPressing()) && !m_Fired)
        {
            m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;

            //m_AimSlider.value = m_CurrentLaunchForce;
            m_AimCross.gameObject.transform.position += new Vector3(0f, 0.04f * (m_CurrentLaunchForce - m_MinLaunchForce), 0f);
        }
        else if ((Input.GetButtonUp(m_FireButton) || shootButton.isKeyUp()) && !m_Fired)
        {
            Fire();
        }
        else if (Input.GetKeyDown(m_PlayerNumber == 1 ? KeyCode.Q : KeyCode.L))
        {
            // Create time manipulation field
            Vector3 pos = m_FireTransform.position + new Vector3(0, 1, 0);
            Instantiate(m_Field, pos, m_FireTransform.rotation);
            m_FieldPositions.Add(pos);
        }
    }

    // normal shell fire
    private void Fire()
    {
        m_Fired = true;
        Rigidbody shellInstance = null;

        if (m_SpecialWeapon == 1 && m_SpecialWeaponCount > 0)
        {
            m_SpecialWeaponCount--;
            shellInstance = Instantiate(m_FrozonShell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
        }
        else if (m_SpecialWeapon == 2 && m_SpecialWeaponCount > 0)
        {
            m_SpecialWeaponCount--;
            shellInstance = Instantiate(m_CannonShell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
        }
        else
        {
            shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
            m_SpecialWeapon = 0;
            m_SpecialWeaponCount--;
        }

        shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();
        
        m_CurrentLaunchForce = m_MinLaunchForce;
    }
}
