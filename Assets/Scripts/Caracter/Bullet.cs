using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public AnimatorController animator;
    void Start()
    {
        Destroy(gameObject, 5);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject,0.3f);
            Scoup.bullets.Remove(gameObject);
            StartCoroutine(Add());
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
            StartCoroutine(Add());
        }
        else if (other.CompareTag("Body"))
        {
            Animator anim = other.GetComponentInParent<Animator>();
            anim.Play("Forward");
            ScoreController.score += 1;
            CanvasController.Win = true;
            StartCoroutine(Add());
        }
        else if (other.CompareTag("RightHand"))
        {
            Animator anim = other.GetComponentInParent<Animator>();
            anim.Play("Right");
            ScoreController.score += 1;
            CanvasController.Win = true;
            StartCoroutine(Add());
        }
        else if (other.CompareTag("LeftHand"))
        {
            Animator anim = other.GetComponentInParent<Animator>();
            anim.Play("Left");
            ScoreController.score += 1;
            CanvasController.Win = true;
            StartCoroutine(Add());
        }
    }
    IEnumerator Add()
    {
        yield return new WaitForSeconds(0.5f);
        YGadd.TryShowFullscreenAdWithChance(50);
    }
    private void OnDestroy()
    {
        Scoup.bullets.Remove(gameObject);
    }
}
