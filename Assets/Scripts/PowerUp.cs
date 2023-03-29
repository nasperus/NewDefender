using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] GameObject[] powerUp;
    public static PowerUp instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Instantiate(powerUp[0], transform.position, transform.rotation);
    }



}
