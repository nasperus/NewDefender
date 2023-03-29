using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    
    [SerializeField] GameObject[] powerUp;
    public static PowerUp instance;
    private float padding = 0.5f;
    private int xMax = 17, xMin = 17;
    private int speed = 10;
    Vector2 newPos;

    private void Awake()
    {
        instance = this;

        
       
    }


    private void Start()
    {
     
        StartCoroutine(PowerDown());
    }


    private void PowerUpScore()
    {

        var random = Random.Range(-xMin, xMax);
        newPos = new Vector2(random, transform.position.y);  
        var spawn = Instantiate(powerUp[0], newPos, transform.rotation);
        spawn.GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;

    }

    IEnumerator PowerDown()
    {
        yield return new WaitForSeconds(10f);
        while (true)
        {
            PowerUpScore();
            yield return new WaitForSeconds(10f);
        }
    }

}
