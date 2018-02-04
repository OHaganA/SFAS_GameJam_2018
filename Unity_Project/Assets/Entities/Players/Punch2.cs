using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch2 : MonoBehaviour {

    private PlayerController characterController;

    private void Start()
    {
        characterController = GameObject.Find("Player_2").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player_1" && characterController.isPunching() == true)
        {
            Vector3 dir = collision.transform.position - transform.position;
            dir = dir.normalized;
            collision.transform.position = collision.transform.position + (dir * 2);
        }
    }
}
