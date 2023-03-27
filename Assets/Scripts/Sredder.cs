using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sredder : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("DestroyLaser"))
            Destroy(collision.gameObject);
    }
}
