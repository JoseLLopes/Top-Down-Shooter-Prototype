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
        GameObject bullet = ObjectPoolerManager.Instance.InstantiatePoolObject(bulletPrefab, shootPoint.position, shootPoint.rotation);
        if (bullet == null)
        {
            Debug.LogError("Bullet not instantiated");
            yield break;
        }
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(transform.forward * 20f, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        Move();
        LookAtMouse();
        SetAxisAnimaor();
    }


    private void LookAtMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Vector3 target = hit.point;
            target.y = transform.position.y; // Mantém rotação só no eixo Y (horizontal)

            Vector3 direction = (target - transform.position).normalized;

            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
            }
        }
    }

    void SetAxisAnimaor()
    {
        //Set horizontal and vertical based on player forward direction
        Vector3 localMovement = transform.InverseTransformDirection(movement);

        animator.SetFloat("horizontal", localMovement.x);
        animator.SetFloat("vertical", localMovement.z);
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