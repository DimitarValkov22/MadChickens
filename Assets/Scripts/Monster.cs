using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Monster : MonoBehaviour
{
    [SerializeField] Sprite deadSprite;
    [SerializeField] ParticleSystem particleSystem;

    bool hasDied;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldDieFromCollision(collision))
        {
            StartCoroutine(Die());
        }
           
    }

    IEnumerator Die()
    {
        hasDied = true;
        GetComponent<SpriteRenderer>().sprite = deadSprite;
        particleSystem.Play();
        yield return new WaitForSecondsRealtime(1);
        gameObject.SetActive(false);
    }

     bool ShouldDieFromCollision(Collision2D collision)
    {
        if (hasDied)
            return false;

        Bird bird = collision.gameObject.GetComponent<Bird>();
        if (bird != null)
            return true;

        if (collision.contacts[0].normal.y < -0.5)
            return true;

        return false;
    }
}
