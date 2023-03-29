using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 100;

    [Header("Shooting")]
    [SerializeField] GameObject enemylaser;
    [SerializeField] float enemyShootSpeed;
    private float shootCounter;
    [SerializeField] float min = 0.2f, max = 3f;

    [Header("Sound Effects")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float explotionDuration = 1f;
    [SerializeField] AudioClip deathSound;
    [SerializeField][Range(0, 1)] float deathSoundVolume;
    [SerializeField] AudioClip shootSound;
    [SerializeField][Range(0, 1)] float shootSoundVolume;


    private void Start()
    {
        shootCounter = Random.Range(min, max);
    }
    private void Update()
    {
        CountDownShoot();
    }

    private void CountDownShoot()
    {

        shootCounter -= Time.deltaTime;

        if (shootCounter <= 0)
        {
            var laser = Instantiate(enemylaser, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyShootSpeed);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
            shootCounter = Random.Range(min, max);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        Damage damage = other.gameObject.GetComponent<Damage>();
        health -= damage.GetDamage();
        damage.Hit();

        if (health <= 0)
        {
            Die();
        }


    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);
        var explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, explotionDuration);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }
}
