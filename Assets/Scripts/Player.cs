using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{



    [Header("Player Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] int health;
    [SerializeField] AudioClip deathSound;
    [SerializeField][Range(0, 1)] float deathSoundVolume = 0.7f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] AudioClip bulletSound;
    [SerializeField][Range(0, 1)] float shootSoundVolume = 0.25f;


    [Header("Projectile")]
    [SerializeField] GameObject laser;
    [SerializeField] GameObject laser2;
    [SerializeField] float shootingSpeed = 20f;
    [SerializeField] float projectileFiringPeriod = 0.1f;


    void Start()
    {


        StartCoroutine(FireCoroutine());

    }


    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    IEnumerator FireCoroutine()
    {
        while (true)
        {
            var shooting = Instantiate(laser, transform.position, transform.rotation);
            var shooting2 = Instantiate(laser2, transform.position, transform.rotation);

            if (TakePower.instance.IsDestroyed())
            {
                shooting2.transform.position += Vector3.right * 0.3f;
                shooting.transform.position += Vector3.left * 0.3f;
            }

            shooting2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, shootingSpeed);
            shooting.GetComponent<Rigidbody2D>().velocity = new Vector2(0, shootingSpeed);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }

    }

    private void Move()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            var touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0f;
            transform.position = touchPosition;


        }

    }

    private void Die()
    {

        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }

    public int GetHealth() { return health; }

    private void OnTriggerEnter2D(Collider2D other)
    {

        health -= Damage.instance.GetDamage();
        AudioSource.PlayClipAtPoint(bulletSound, Camera.main.transform.position, 1f);
        Damage.instance.Hit();

        if (health <= 0)
        {
            Die();
        }

    }



}
