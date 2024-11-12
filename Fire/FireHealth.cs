using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireHealth : MonoBehaviour
{
    [SerializeField] private AudioSource fireSound;
    [SerializeField] private ParticleSystem fireParticles;    

    private FireCounter fireCounter;
    private FiresRemaining firesRemaining;

    public int fireMaxHealth = 100;
    public float fireCurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        fireCounter = GameObject.FindObjectOfType<FireCounter>();
        firesRemaining = GameObject.FindObjectOfType<FiresRemaining>();
        fireCurrentHealth = fireMaxHealth;
    }

    private void Update()
    {
        fireSound.volume = fireCurrentHealth / 100;

        if (fireCurrentHealth <= 0)
        {
            firesRemaining.uiFade = true;
            fireCounter.noOfFires--;            
            Destroy(gameObject);            
        }        
    }    

    public void FireDamage(float damage)
    {
        fireCurrentHealth -= damage;
        fireParticles.emissionRate -= damage / 3;        
    }
}