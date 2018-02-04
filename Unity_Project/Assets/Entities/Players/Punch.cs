using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour {
    private PlayerController characterController;

    private void Start()
    {
        characterController = GameObject.Find("Player_1").GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player_2" && characterController.isPunching() == true)
        {
            Vector3 dir = collision.transform.position - transform.position;
            dir = dir.normalized;
            collision.transform.position = collision.transform.position + (dir * 2);
        }
    }
}
