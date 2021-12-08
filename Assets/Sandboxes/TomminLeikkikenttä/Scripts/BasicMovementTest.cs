using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovementTest : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject player;
    public GameObject enemy;
    public float speed = 5f;
    public float jumpForce = 5f;

    public bool attacking;

    public float distance;

    void Start()
    {
        player = gameObject;
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        rb = gameObject.GetComponent<Rigidbody>();

        attacking = false;
    }

    public void MoveLeft()
    {
        rb.velocity = new Vector3(-speed, 0);
    }

    public void MoveRight()
    {
        rb.velocity = new Vector3(speed, 0);
    }

    public void JumpRight()
    {
        rb.velocity = new Vector3(speed, jumpForce);
    }

    public void JumpLeft()
    {
        rb.velocity = new Vector3(-speed, jumpForce);
    }

    public void Plunge()
    {
        attacking = true;
        JumpRight(); // just using JumpRight's logic for testing
    }

    public void Rest()
    {
        Debug.Log("Sleeping in battle seems good to me");
    }

    public void Update()
    {
        //distance between player and enemy
        if (enemy != null)
            distance = Vector3.Distance(player.transform.position, enemy.transform.position);
    }

    void OnTriggerEnter(Collider col)
    {
        if (attacking && col.gameObject.tag == "Enemy")
        {
            // Plunge one shots player for now :D
            Destroy(enemy);
            attacking = false;
        }
    }
}
