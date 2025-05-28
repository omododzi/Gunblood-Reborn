using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Scoup : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 20f;
    public Transform shootPoint;
    public float rotationSpeed = 10f;
    
    private Animator animator;
    public static List<GameObject> bullets = new List<GameObject>();
    private Vector3 targetShootPoint;
    public AudioSource audioSource;
    public AudioClip clip;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        animator.SetBool("Meny", CameraController.starting);
        
        if (Input.GetMouseButtonDown(0) && bullets.Count <= 0 && Timer.Canshoot)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                // Сохраняем только точку прицеливания
                targetShootPoint = hit.point;
                
                StartCoroutine(RotateAndShoot());
                animator.SetTrigger("Shoot");
                if (CanvasController.Mysic)
                {
                    audioSource.PlayOneShot(clip);
                }
            }
        }
    }

    IEnumerator RotateAndShoot()
    {
        // Рассчитываем начальное направление
        Vector3 initialDirection = (targetShootPoint - shootPoint.position).normalized;
        Vector3 lookDirection = new Vector3(initialDirection.x, 0, initialDirection.z);
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
        
        // Плавный поворот
        float time = 0;
        while (time < 1f)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation, 
                targetRotation, 
                time);
                
            time += Time.deltaTime * rotationSpeed;
            yield return null;
        }

        // После поворота создаем пулю с текущими координатами
        CreateProjectile();
    }

    void CreateProjectile()
    {
        // Направление в точку клика (УЧИТЫВАЕТ высоту)
        Vector3 direction = (targetShootPoint - shootPoint.position).normalized;
    
        // Спавним пулю
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        bullets.Add(projectile);
    
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