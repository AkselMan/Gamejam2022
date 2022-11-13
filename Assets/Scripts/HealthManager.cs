using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public enum DamageType
{
    player = 0,
    box = 1,
    enemy = 2,
}
public class HealthManager : MonoBehaviour
{
    public float health, maxHealth;
    public Slider healthSlider;

    public UnityEvent hitEvent, deathEvent;
    public Material whiteMaterial; //sejt materiale til at lave et hvidt flash ved skade
    private Material origMaterial;

    public bool hitEventOnDeath = true; //hvis man skal lave hit hvis man dør
    public bool EnemySpawnerDecrease = false;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        origMaterial = sr.sharedMaterial;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = health;
        }
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
                hitEvent.Invoke();
            }
            Death();
            if (!hitEventOnDeath) return;
        }
        if (healthSlider != null)
        {
            healthSlider.value = health;
        }
        StartCoroutine(whiteFlash(0.1f));

        hitEvent.Invoke();
    }

    public void Death()
    {
        if (EnemySpawnerDecrease) EnemySpawner.Instance.aliveEnemies--;
        deathEvent.Invoke();
    }

    IEnumerator whiteFlash(float duration)
    {
        sr.material = new Material(whiteMaterial);

        yield return new WaitForSeconds(duration);

        sr.material = origMaterial;
    }
}
