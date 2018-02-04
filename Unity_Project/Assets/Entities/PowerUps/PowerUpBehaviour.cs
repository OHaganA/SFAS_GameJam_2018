using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour {
    private PlayerController m_CharacterController1;
    private PlayerController m_CharacterController2;

    private void Start()
    {
        m_CharacterController1 = GameObject.Find("Player_1").GetComponent<PlayerController>();
        m_CharacterController2 = GameObject.Find("Player_2").GetComponent<PlayerController>();

    }
    public void PowerUp1()
    {
        if (gameObject.name == "PowerUpSpeed(Clone)")
        {
            m_CharacterController1.IncreaseSpeed();
        }
        else if (gameObject.name == "PowerUpJump(Clone)")
        {
            m_CharacterController1.IncreaseJumps();
        }
        else if (gameObject.name == "PowerUpRange(Clone)")
        {
            m_CharacterController1.IncreaseRange();
        }
    }

    public void PowerUp2()
    {
        if (gameObject.name == "PowerUpSpeed(Clone)")
        {
            m_CharacterController2.IncreaseSpeed();
        }
        else if (gameObject.name == "PowerUpJump(Clone)")
        {
            m_CharacterController2.IncreaseJumps();
        }
        else if (gameObject.name == "PowerUpRange(Clone)")
        {
            m_CharacterController2.IncreaseRange();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (other.name == "Player_1")
        {
            PowerUp1();
        }
        else
        {
            PowerUp2();
        }
        Debug.Log("PICKED UP");
    }
}
