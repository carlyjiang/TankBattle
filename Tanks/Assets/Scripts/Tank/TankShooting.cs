using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class TankShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;              // Used to identify the different players.
    public Rigidbody m_Shell;                   // Prefab of the normal shell.
    public Rigidbody m_FrozonShell;             // Prefab of the frozen shell.
    public Rigidbody m_CannonShell;             // Prefab of the cannon shell.
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

    public int m_SpecialWeapon;                 // 0 for none, 1 for cannon shell, 2 for frozen shell
    public int m_SpecialWeaponCount = 0;
    public Image m_ShellIndicatorImage;

    public Text m_ShellLeft;

    private string m_FireButton;                // The input axis that is used for launching shells.
    private float m_CurrentLaunchForce;         // The force that will be given to the shell when the fire button is released.
    private float m_ChargeSpeed;                // How fast the launch force increases, based on the max charge time.
    private bool m_Fired;                       // Whether or not the shell has been launched with this button press.
    private Vector3 m_AimCrossOriginalPositionOffset;

    private ShootButton m_ShootButton;          // add touch screen shooting button
    private Slider m_ReloadSlider;
    private float m_ReloadTime = 2f;
    private float m_ShootingTime;


    private void OnEnable()
    {
        // When the tank is turned on, reset the launch force and the UI
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
        m_Fired = false;

        m_AimCrossOriginalPositionOffset = new Vector3(
            0f,
            m_AimSlider.gameObject.transform.position.y - m_AimCross.gameObject.transform.position.y,
            0f);

        if (m_ReloadSlider)
        {
            m_ReloadSlider.value = 100f;
        }
    }


    private void Start()
    {
        m_FireButton = "Fire" + m_PlayerNumber;
        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
        m_ShootButton = GameObject.FindGameObjectWithTag("ShootButton").GetComponent<ShootButton>();
        m_ShellIndicatorImage = GameObject.FindGameObjectWithTag("ShootButton").GetComponent<Image>();
        m_ReloadSlider = GameObject.FindGameObjectWithTag("Reload").GetComponent<Slider>();
        m_ShellLeft = GameObject.FindGameObjectWithTag("ShellCount").GetComponent<Text>();
    }


    private void Update()
    {
        m_AimCross.gameObject.transform.position = m_AimSlider.gameObject.transform.position 
            - m_AimCrossOriginalPositionOffset;

        changeShootButtonColor();

        m_ShellLeft.text = "Left: " + (m_SpecialWeaponCount <= 0 ? "INF" : Convert.ToString(m_SpecialWeaponCount));

        if (Time.time - m_ShootingTime <= m_ReloadTime + 0.1f && m_Fired)
        {
            m_ReloadSlider.value = 100 * (Time.time - m_ShootingTime) / m_ReloadTime;
        }
        else
        {
            if (Input.GetButtonDown(m_FireButton) || m_ShootButton.isKeyDown())
            {
                m_Fired = false;
                m_CurrentLaunchForce = m_MinLaunchForce;

                m_ShootingAudio.clip = m_ChargingClip;
                m_ShootingAudio.Play();
            }
            else if ((Input.GetButton(m_FireButton) || m_ShootButton.IsPressing()) && !m_Fired)
            {
                if (m_CurrentLaunchForce <= m_MaxLaunchForce)
                {
                    m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;
                }

                m_AimCross.gameObject.transform.position += new Vector3(0f, 0.04f * (m_CurrentLaunchForce - m_MinLaunchForce), 0f);
            }
            else if ((Input.GetButtonUp(m_FireButton) || m_ShootButton.isKeyUp()) && !m_Fired)
            {
                Fire();
            }
        }
    }


    private void changeShootButtonColor()
    {
        if (m_SpecialWeapon == 1)
        {
            m_ShellIndicatorImage.color = Color.blue;
        }
        else if (m_SpecialWeapon == 2)
        {
            m_ShellIndicatorImage.color = Color.red;
        }
        else
        {
            m_ShellIndicatorImage.color = Color.yellow;
        }
    }


    private void Fire()
    {
        m_Fired = true;
        m_ShootingTime = Time.time;
        Rigidbody shellInstance = null;
        m_ReloadSlider.value = 0f;

        if (m_SpecialWeapon == 1 && m_SpecialWeaponCount > 0)
        {
            shellInstance = Instantiate(m_FrozonShell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
            m_SpecialWeaponCount--;
            m_ReloadTime = 0.5f;
        }
        else if (m_SpecialWeapon == 2 && m_SpecialWeaponCount > 0)
        {
            shellInstance = Instantiate(m_CannonShell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
            m_SpecialWeaponCount--;
            m_ReloadTime = 2f;
        }
        else
        {
            shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
            m_SpecialWeapon = 0;
            m_SpecialWeaponCount--;
            m_ReloadTime = 1f;
        }

        if (m_SpecialWeaponCount == 0)
        {
            m_SpecialWeapon = 0;
        }

        shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();

        m_CurrentLaunchForce = m_MinLaunchForce;

        changeShootButtonColor();
    }
}

