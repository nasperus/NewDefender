using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{

    private float xMin, xMax, yMin, yMax;
    private float padding = 0.5f;
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
    [SerializeField] float shootingSpeed = 20f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    Coroutine coroutine;

    void Start()
    {

        CameraBoundries();

    }


    void Update()
    {
        Move();
        Fire();

    }
    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            coroutine = StartCoroutine(FireCoroutine());
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopCoroutine(coroutine);
        }
    }
    IEnumerator FireCoroutine()
    {
        while (true)
        {
            var shooting = Instantiate(laser, transform.position, transform.rotation);
            shooting.GetComponent<Rigidbody2D>().velocity = new Vector2(0, shootingSpeed);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }

    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newposX = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newposY = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newposX, newposY);

    }
    private void CameraBoundries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
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

        Damage damage = other.gameObject.GetComponent<Damage>();
        health -= damage.GetDamage();
        AudioSource.PlayClipAtPoint(bulletSound, Camera.main.transform.position, 1f);
        damage.Hit();

        if (health <= 0)
        {
            Die();
        }


    }

}
