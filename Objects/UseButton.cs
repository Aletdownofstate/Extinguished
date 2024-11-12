using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UseButton : MonoBehaviour
{
    [SerializeField] private AudioSource btnSound;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite btnOn;
    [SerializeField] private Light2D btnLight;
    public bool isActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        isActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {            
            OnSprite();
            btnLight.color = new Color32 (46, 195, 46, 255);
        }
    }

    void OnSprite()
    {
        spriteRenderer.sprite = btnOn;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isActivated)
        {
            if (Input.GetButtonDown("Use"))
            {
                isActivated = true;
                btnSound.Play();
                FindObjectOfType<PlayerController>().controlEnabled = false;
                StartCoroutine(Delay());
            }
        }
        else
        {
            return;
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        btnSound.enabled = false;
        FindObjectOfType<PlayerController>().controlEnabled = true;        
    }
}
