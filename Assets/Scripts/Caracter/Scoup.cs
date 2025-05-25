using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
public class Scoup : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 20f;
    public Transform shootPoint; // Точка, откуда вылетает снаряд
    private Animator animator;
    private List<GameObject> bullets;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        animator.SetBool("Meny",CameraController.starting);
        if (Input.GetMouseButtonDown(0) && bullets.Count <= 0 && Timer.Canshoot) // ЛКМ
        {
            //animator.Play("Blend Tree");
            animator.SetTrigger("Shoot");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 targetPoint = hit.point;
                StartCoroutine(ShootAt(targetPoint));
            }
        }
    }

    private void FixedUpdate()
    {
        bullets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Bullet"));
    }


    IEnumerator ShootAt(Vector3 targetPosition)
    {
        yield return new WaitForSeconds(0.3f);
        Vector3 direction = (targetPosition - shootPoint.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
    
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.linearVelocity = direction * projectileSpeed;
    
        // Исправление поворота для пуль, ориентированных по Y
        if (direction != Vector3.zero)
        {
            // Поворачиваем на 90 градусов по X, если пуля "смотрит" вверх
            Quaternion correction = Quaternion.Euler(90f, 0f, 0f);
            projectile.transform.rotation = Quaternion.LookRotation(direction) * correction;
        }
    
        Destroy(projectile, 5f);
    }
}
