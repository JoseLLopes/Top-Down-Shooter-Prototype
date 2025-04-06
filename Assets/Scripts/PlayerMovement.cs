using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator animator;
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float shootDelay = 0.5f;

    float shootTimer = 0f;

    private Vector3 movement;

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        movement = new Vector3(h, 0f, v).normalized;

        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (shootTimer > 0f)
        {
            shootTimer -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        if(shootTimer > 0f)
        {
            return;
        }
        shootTimer = shootDelay;
        animator.SetTrigger("attack");       
        StartCoroutine(ShootTime());
    }

    IEnumerator ShootTime()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(transform.forward * 20f, ForceMode.Impulse);
        Destroy(bullet, 2f); // Destroy the bullet after 2 seconds
    }

    void FixedUpdate()
    {
        Move();
        this.Rotate();
    }



    private void Rotate()
    {
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.deltaTime);
        }
    }

    void Move()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        if (movement != Vector3.zero)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}