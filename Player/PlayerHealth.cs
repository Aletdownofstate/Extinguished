using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private AudioSource painSound;
    
    private SpriteRenderer playerSprite;
    private HealthBar healthBar;
    private WaterBar waterBar;

    public int maxHealth = 100;
    public int currentHealth;

    private bool isInvincible;
    private float iFrameTime = 1.5f;

    private bool uiFade;

    void Start()
    {
        playerSprite = GameObject.FindGameObjectWithTag("PlayerSprite").GetComponent<SpriteRenderer>(); 
        healthBar = FindObjectOfType<HealthBar>();

        if (GameData.currentHealth == 0)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth = GameData.currentHealth;
        }

        StartCoroutine(IFrames());        
        healthBar.SetMaxHealth(maxHealth);
        healthBar.Sethealth(currentHealth);
        healthBar.canvasGroup.alpha = 0;
        uiFade = false;        
    }

    private void Update()
    {
        GameData.currentHealth = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible)
        {
            return;
        }
        else
        {
            StopAllCoroutines();
            uiFade = true;
            StartCoroutine(FadeHealthBar());
            float painPitch = Random.Range(0.9f, 1.1f);
            painSound.pitch = painPitch;
            painSound.Play();
            currentHealth -= damage;            
            healthBar.Sethealth(currentHealth);
            IFrameStart();            
        }
    }

    private void IFrameStart()
    {
        if (!isInvincible)
        {
            StartCoroutine(IFrames());
            StartCoroutine(DamageFlash());
        }
    }

    IEnumerator IFrames()
    {
        isInvincible = true;
        yield return new WaitForSeconds(iFrameTime);        
        isInvincible = false;        
    }    

    IEnumerator DamageFlash()
    {
        while(isInvincible)
        {
            playerSprite.color = Color.red;
            yield return new WaitForSeconds(0.10f);
            playerSprite.color = Color.white;
            yield return new WaitForSeconds(0.10f);            
        }
    }

    IEnumerator FadeHealthBar()
    {
        yield return new WaitForSeconds(3f);
        uiFade = false;
    }

    private void FixedUpdate()
    {
        if (uiFade)
        {
            healthBar.canvasGroup.alpha += Time.deltaTime / 0.35f;            
        }
        else
        {
            healthBar.canvasGroup.alpha -= Time.deltaTime / 1;            
        }
    }
}