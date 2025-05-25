using UnityEngine;

public class CameraController : MonoBehaviour
{
      private Transform target;
      private Transform bullet;
      public Vector3 offset = new Vector3(0f, 2f, -5f);
      public Vector3 offsetstart = new Vector3(0f, 2f, -5f);
      public Vector3 offsetbullet = new Vector3(0f, 0f, 0f);
      public static bool starting = true;
      
      [Header("Follow Settings")]
      [Range(0.1f, 10f)]
      public float followSpeed = 5f;
      
      [Header("Rotation Settings")]
      [Range(0.1f, 5f)]
      public float rotationSpeed = 1f;
      
      [Header("Smoothing")]
      [Range(0.01f, 0.5f)]
      public float positionSmoothTime = 0.1f;
      
      public float lookAtHeightOffset = 0.5f;
      public float lookAtRightOffset = 0.5f;
  
      private Vector3 velocity = Vector3.zero;
      private bool targetFound = false;
      public bool buletfound;
  
      void Start()
      {
          FindTarget();
      }
  
      void LateUpdate()
      {
          if (!targetFound || target == null)
          {
              FindTarget();
          }

          if (!buletfound || bullet == null)
          {
              FindBullet();
          }

          if (!buletfound)
          {
              FollowTarget();
              RotateTowardsTarget();
          }
          else if (buletfound && bullet != null)
          {
              FollowBullet();
              RotateToBullet();
          }
          else
          {
              buletfound = false;
          }
      }

      void FindBullet()
      {
          GameObject player = GameObject.FindGameObjectWithTag("Bullet");
          if (player != null)
          {
              bullet = player.transform;
              buletfound = true;
              
              // Устанавливаем начальную позицию камеры
              transform.position = target.position + offset;
              transform.LookAt(target.position + Vector3.up *
                  lookAtHeightOffset + Vector3.right * lookAtRightOffset);
          }
      }
  
      private void FindTarget()
      {
          GameObject player = GameObject.FindGameObjectWithTag("Player");
          if (player != null)
          {
              target = player.transform;
              targetFound = true;
              
              // Устанавливаем начальную позицию камеры
              transform.position = target.position + offset;
              transform.LookAt(target.position + Vector3.up *
                  lookAtHeightOffset + Vector3.right * lookAtRightOffset);
          }
      }
  
      private void FollowTarget()
      {
          if (starting)
          {
              lookAtRightOffset = 0f;
              lookAtHeightOffset = 1f;
              Vector3 desiredPosition = target.position + 
                                        target.right * offsetstart.x + 
                                        target.up * offsetstart.y + 
                                        target.forward * offsetstart.z;
  
              transform.position = Vector3.SmoothDamp(
                  transform.position,
                  desiredPosition,
                  ref velocity,
                  positionSmoothTime,
                  Mathf.Infinity, // Макс. скорость - без ограничений
                  Time.deltaTime
              );
          }
          else
          {
              lookAtRightOffset = -1.5f;
              Vector3 desiredPosition = target.position + 
                                        target.right * offset.x + 
                                        target.up * offset.y + 
                                        target.forward * offset.z;
  
              transform.position = Vector3.SmoothDamp(
                  transform.position,
                  desiredPosition,
                  ref velocity,
                  positionSmoothTime,
                  Mathf.Infinity, // Макс. скорость - без ограничений
                  Time.deltaTime
              );
          }
      }
  
      private void RotateTowardsTarget()
      {
          Vector3 lookAtPoint = target.position + Vector3.up * lookAtHeightOffset+ Vector3.right * lookAtRightOffset;
          Vector3 direction = lookAtPoint - transform.position;
          
          // Используем SmoothDamp и для вращения (более плавно)
          Quaternion desiredRotation = Quaternion.LookRotation(direction);
          transform.rotation = Quaternion.Slerp(
              transform.rotation,
              desiredRotation,
              rotationSpeed * Time.deltaTime
          );
      }

      void FollowBullet()
      {
          lookAtRightOffset = -1.5f;
          lookAtHeightOffset = 0f;
          Vector3 desiredPosition = bullet.position + 
                                    bullet.right * offsetbullet.x + 
                                    bullet.up * offsetbullet.y + 
                                    bullet.forward * offsetbullet.z;

          transform.position = Vector3.SmoothDamp(
              transform.position,
              desiredPosition,
              ref velocity,
              positionSmoothTime,
              Mathf.Infinity, // Макс. скорость - без ограничений
              Time.deltaTime
          );
      }

      void RotateToBullet()
      {
          
          Vector3 lookAtPoint = bullet.position + Vector3.up * lookAtHeightOffset;
          Vector3 direction = lookAtPoint - transform.position;
          
          // Используем SmoothDamp и для вращения (более плавно)
          Quaternion desiredRotation = Quaternion.LookRotation(direction);
          transform.rotation = Quaternion.Slerp(
              transform.rotation,
              desiredRotation,
              rotationSpeed * Time.deltaTime
          );
      }
}


