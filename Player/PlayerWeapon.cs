using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject fire;
    [SerializeField] private AudioSource waterSound;    

    private PlayerController player;
    private FireHealth fireHealth;
    private PlayerWater playerWater;
    private WaterBar waterBar;

    public Transform firePoint;
    
    [HideInInspector] public bool shootEnabled;
    private bool isShooting;
    public bool uiFade;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        fireHealth = FindObjectOfType<FireHealth>();
        playerWater = FindObjectOfType<PlayerWater>();
        waterBar = FindObjectOfType<WaterBar>();
    }

    // Update is called once per frame
    void Update()
    {
        shootEnabled = player.shootEnabled;

        if (player.controlEnabled && shootEnabled)
        {
            if (Input.GetButton("Fire1"))
            {
                isShooting = true;
            }
            else
            {
                isShooting = false;
                waterSound.Stop();
            }
            if (Input.GetButton("Fire1"))
            {
                ShootSound();
            }
        }

        // Removes ability to shoot water if water value is 0

        if (playerWater.currentWater <= 0)
        {
            if (waterSound.isPlaying)
            {
                waterSound.Stop();
            }
            isShooting = false;
            GameData.shootEnabled = false;
            player.waterGun.Stop();
        }
    }

    private void FixedUpdate()
    {        
        if (isShooting)
        {
            uiFade = true;
            ShootRaycast();
            playerWater.currentWater = playerWater.currentWater - 0.025f;
            StopAllCoroutines();
        }
        else
        {
            if (!isShooting && waterBar.canvasGroup.alpha > 0)
            {
                StartCoroutine(UIWait());                
            }            
        }

        if (uiFade)
        {
            waterBar.canvasGroup.alpha += Time.deltaTime / 0.35f;
        }
        else
        {            
            waterBar.canvasGroup.alpha -= Time.deltaTime / 1;
        }
    }

    void ShootRaycast()
    {
        if (Input.GetButton("Fire1"))
        {
            RaycastHit2D shootRay = Physics2D.Raycast(firePoint.position, firePoint.right, 7f);
            Debug.DrawRay(firePoint.position, firePoint.right * 7f);

            if (shootRay.collider != null)
            {
                if (shootRay.collider.tag.Equals("Fire"))
                {
                    FireHealth fireHealth = shootRay.collider.gameObject.GetComponent<FireHealth>();
                    if (fireHealth != null)
                    {
                        fireHealth.FireDamage(0.05f);
                    }
                }
            }
        }
    }

    private void ShootSound()
    {
        if (isShooting && !waterSound.isPlaying)
        {
            waterSound.volume = 0.5f;
            waterSound.pitch = 1.75f;
            waterSound.Play();
        }
    }

    IEnumerator UIWait()
    {
        yield return new WaitForSeconds(2);
        uiFade = false;
    }
}