using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    private Rigidbody _rb;
    private bool _readyToStick = false;
    [HideInInspector]
    public bool _readyToTeleport;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _readyToStick = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _readyToStick = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_readyToStick)
        {
           _rb.constraints  = RigidbodyConstraints.FreezeAll;
            _readyToTeleport = true;
        }
        /*if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject, 1);
        }*/
    }

}