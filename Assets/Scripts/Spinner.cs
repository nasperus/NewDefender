using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{

    [SerializeField] float speedOfSpeen;

    void Update()
    {
        transform.Rotate(0, 0, speedOfSpeen * Time.deltaTime);
    }
}
