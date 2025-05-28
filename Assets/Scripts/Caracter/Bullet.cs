using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public AnimatorController animator;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject,0.3f);
            Scoup.bullets.Remove(gameObject);
        }

        if (collision.gameObject.CompareTag("Destroy"))
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
            ScoreController.score += 1;
            CanvasController.Win = true;
        }
        else if (other.CompareTag("Body"))
        {
            Animator anim = other.GetComponentInParent<Animator>();
            anim.Play("Forward");
            ScoreController.score += 1;
            CanvasController.Win = true;
        }
        else if (other.CompareTag("RightHand"))
        {
            Animator anim = other.GetComponentInParent<Animator>();
            anim.Play("Right");
            ScoreController.score += 1;
            CanvasController.Win = true;
        }
        else if (other.CompareTag("LeftHand"))
        {
            Animator anim = other.GetComponentInParent<Animator>();
            anim.Play("Left");
            ScoreController.score += 1;
            CanvasController.Win = true;
        }
    }
    private void OnDestroy()
    {
        Scoup.bullets.Remove(gameObject);
    }
}
