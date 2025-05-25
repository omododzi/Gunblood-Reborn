using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public AnimatorController animator;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Head"))
        {
            Animator anim = other.GetComponentInParent<Animator>();
            anim.Play("Headshot");
        }
        else if (other.CompareTag("Body"))
        {
            Animator anim = other.GetComponentInParent<Animator>();
            anim.Play("Forward");
        }
        else if (other.CompareTag("RightHand"))
        {
            Animator anim = other.GetComponentInParent<Animator>();
            anim.Play("Right");
        }
        else if (other.CompareTag("LeftHand"))
        {
            Animator anim = other.GetComponentInParent<Animator>();
            anim.Play("Left");
        }
    }
}
