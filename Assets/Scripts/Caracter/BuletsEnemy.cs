using System;
using System.Collections;
using UnityEngine;

public class BuletsEnemy : MonoBehaviour
{
    public AnimatorController animator;

    void Start()
    {
        Destroy(gameObject, 5);
    }
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
            StartCoroutine(Add());
            CanvasController.Win = true;
        }
        
        if (other.CompareTag("Head"))
        {
            Animator anim = other.GetComponentInParent<Animator>();
            anim.Play("Headshot");
            Destroy(gameObject, 0.3f);
            CanvasController.Win = true;
            StartCoroutine(Add());

        }
        else if (other.CompareTag("Body"))
        {
            Animator anim = other.GetComponentInParent<Animator>();
            anim.Play("Forward");
            Destroy(gameObject, 0.3f);
            CanvasController.Win = true;
            StartCoroutine(Add());
        }
        else if (other.CompareTag("RightHand"))
        {
            Animator anim = other.GetComponentInParent<Animator>();
            anim.Play("Right");
            Destroy(gameObject, 0.3f);
            CanvasController.Win = true;
            StartCoroutine(Add());

        }
        else if (other.CompareTag("LeftHand"))
        {
            Animator anim = other.GetComponentInParent<Animator>();
            anim.Play("Left");
            Destroy(gameObject, 0.3f);
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
