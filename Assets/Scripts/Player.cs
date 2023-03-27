using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{

    private float xMin, xMax, yMin, yMax;
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject laser;
    private float shootingSpeed = 10f;
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
            yield return new WaitForSeconds(0.1f);
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
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + 0.5f;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - 0.5f;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + 0.5f;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - 0.5f;
    }
}
