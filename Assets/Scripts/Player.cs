using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{


    private float deltaX, deltaY;
    private float xMax, xMin, yMin, yMax;
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
    [SerializeField] GameObject laser2;
    [SerializeField] float shootingSpeed = 20f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    private Rigidbody2D rigidbody2;


    void Start()
    {


        StartCoroutine(FireCoroutine());
        rigidbody2 = GetComponent<Rigidbody2D>();
        CameraBoundries();
    }


    void Update()
    {
        TouchMovement();


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

    private void TouchMovement()
    {


        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            var touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    Time.timeScale = 1f;
                    deltaX = touchPosition.x - transform.position.x;
                    deltaY = touchPosition.y - transform.position.y;

                    break;

                case TouchPhase.Moved:

                    rigidbody2.MovePosition(new Vector2(touchPosition.x - deltaX, touchPosition.y - deltaY));
                    var xPos = Mathf.Clamp(transform.position.x, xMin, xMax);
                    var yPos = Mathf.Clamp(transform.position.y, yMin, yMax);
                    transform.position = new Vector2(xPos, yPos);
                    break;


                case TouchPhase.Ended:

                    Time.timeScale = 0f;
                    break;

            }

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


    private void CameraBoundries()
    {
        xMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;


    }

}
