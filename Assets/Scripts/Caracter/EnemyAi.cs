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
    private List<GameObject> bullets = new List<GameObject>();
    private float nextShootTime;
    private bool isShooting;
    public AudioSource audioSource;
    public AudioClip clip;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        
        // Инициализируем список пуль
        UpdateBulletsList();
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

    void FixedUpdate()
    {
        // Оптимизированный поиск пуль - не каждый кадр
        if (Time.frameCount % 10 == 0) // Проверяем каждые 10 кадров
        {
            UpdateBulletsList();
        }
    }

    bool CanShoot()
    {
        return Timer.Canshoot 
            && bullets.Count <= 0 
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
        Vector3 currentDirection = (targetShootPoint - shootPoint.position).normalized;
        
        GameObject projectile = Instantiate(
            projectilePrefab, 
            shootPoint.position,
            Quaternion.LookRotation(currentDirection) * Quaternion.Euler(90f, 0f, 0f)
        );
        
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.linearVelocity = currentDirection * projectileSpeed;
        
        // Добавляем пулю в список
        bullets.Add(projectile);
    }

    void UpdateBulletsList()
    {
        // Удаляем уничтоженные пули
        bullets.RemoveAll(bullet => bullet == null);
        
        // Добавляем новые пули
        GameObject[] currentBullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (var bullet in currentBullets)
        {
            if (!bullets.Contains(bullet))
            {
                bullets.Add(bullet);
            }
        }
    }
}