using System;
using UnityEngine;

public class BuletsEnemy : MonoBehaviour
{
    public AnimatorController animator;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject,0.3f);
        }
        if (collision.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, 0.3f);
        }
        
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

    private void OnDestroy()
    {
        Scoup.bullets.Remove(gameObject);
    }
}
