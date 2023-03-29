using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePower : MonoBehaviour
{

    public static TakePower instance;
    private void Awake()
    {
        instance = this;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        Destroy(gameObject);


    }


}
