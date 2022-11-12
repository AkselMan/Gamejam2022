using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public enum DamageType
{
    player = 0,
    box = 1,
    enemy = 2,
}
public class HealthManager : MonoBehaviour
{
    public float health, maxHealth;

    public UnityEvent hitEvent, deathEvent;
    public Material whiteMaterial; //sejt materiale til at lave et hvidt flash ved skade
    private Material origMaterial;

    public bool hitEventOnDeath = true; //hvis man skal lave hit hvis man dør

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        origMaterial = sr.sharedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0)) takeDamage(1);
    }

    public void takeDamage(float damage = 1)
    {
        health -= damage;
        if (health <= 0)
        {
            if (hitEventOnDeath)
            {
                SendMessage("OnHit");
                hitEvent.Invoke();
            }
            Death();
            if (!hitEventOnDeath) return;
        }
        StartCoroutine(whiteFlash(0.1f));

        SendMessage("OnHit");
        hitEvent.Invoke();
    }

    public void Death()
    {
        SendMessage("OnDeath");
        deathEvent.Invoke();
    }

    IEnumerator whiteFlash(float duration)
    {
        sr.material = new Material(whiteMaterial);

        yield return new WaitForSeconds(duration);

        sr.material = origMaterial;
    }
}
