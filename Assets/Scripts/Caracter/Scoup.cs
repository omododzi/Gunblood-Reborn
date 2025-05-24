using UnityEngine;

public class Scoup : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 20f;
    public Transform shootPoint; // Точка, откуда вылетает снаряд
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ЛКМ
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 targetPoint = hit.point;
                ShootAt(targetPoint);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
    
    void ShootAt(Vector3 targetPosition)
    {
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
