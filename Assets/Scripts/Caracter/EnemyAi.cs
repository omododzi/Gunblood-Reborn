using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [Header("References")]
    public GameObject projectilePrefab;
    public Transform shootPoint;
    
    [Header("Settings")]
    public float projectileSpeed = 20f;
    public float rotationSpeed = 10f;
    public float shootingOffsetRange = 0.5f;
    public Vector3 targetShootPoint;
    public float offsety = 2f;
    
    private Animator animator;
    private Transform playerTarget;
    private float nextShootTime;
    private bool isShooting;
    public AudioSource audioSource;
    public AudioClip clip;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        animator.SetBool("Meny", CameraController.starting);
        
        if (CanShoot())
        {
            StartShooting();
            audioSource.PlayOneShot(clip);
        }
    }

   

    bool CanShoot()
    {
        return Timer.Canshootenemy
            && !CameraController.starting
            && Scoup.bullets.Count <= 0 
            && !isShooting 
            && Time.time >= nextShootTime;
    }

    void StartShooting()
    {
        // Добавляем случайное смещение для реалистичности
        Vector3 randomOffset = new Vector3(
            Random.Range(-shootingOffsetRange, shootingOffsetRange),
            0,
            Random.Range(-shootingOffsetRange, shootingOffsetRange)
        );
        randomOffset.y += offsety;
        targetShootPoint = playerTarget.position + randomOffset;
        isShooting = true;
        StartCoroutine(RotateAndShoot());
        animator.SetTrigger("Shoot");
        
        // Устанавливаем время следующего выстрела
        nextShootTime = Time.time + Timer.shoottime + Random.Range(0f, 1f);
    }

    IEnumerator RotateAndShoot()
    {
        Vector3 directionToTarget = (targetShootPoint - shootPoint.position).normalized;
        Vector3 horizontalDirection = new Vector3(directionToTarget.x, 0, directionToTarget.z);
        Quaternion targetRotation = Quaternion.LookRotation(horizontalDirection);
        
        // Плавный поворот
        while (Quaternion.Angle(transform.rotation, targetRotation) > 1f)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation, 
                targetRotation, 
                rotationSpeed * Time.deltaTime);
            yield return null;
        }

        CreateProjectile();
        isShooting = false;
    }

    void CreateProjectile()
    {
        // Направление в точку клика (УЧИТЫВАЕТ высоту)
        Vector3 direction = (targetShootPoint - shootPoint.position).normalized;
    
        // Спавним пулю
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Scoup.bullets.Add(projectile);
    
        // Задаём скорость
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.linearVelocity = direction * projectileSpeed;

        // Поворачиваем пулю в сторону выстрела (если нужно)
        if (direction != Vector3.zero)
        {
            projectile.transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}