using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float launchForce = 500;
    [SerializeField] float maxDragDistance = 3;

    Vector2 startPosition;
    Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;

     void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        startPosition = rigidbody2D.position;
       rigidbody2D.isKinematic = true;
    }

     void OnMouseDown()
    {
        spriteRenderer.color = Color.red;
    }

     void OnMouseUp()
    {
        Vector2 currentPosition = rigidbody2D.position;
        Vector2 direction = startPosition - currentPosition;
        direction.Normalize();
        rigidbody2D.isKinematic = false;
        rigidbody2D.AddForce(direction * launchForce);

        spriteRenderer.color = Color.white;
    }

     void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 desiredPosition = mousePosition;

        float distance = Vector2.Distance(desiredPosition, startPosition);
        if(distance > maxDragDistance)
        {
            Vector2 direction = desiredPosition - startPosition;
            direction.Normalize();
            desiredPosition = startPosition + (direction * maxDragDistance);
        }

        if (desiredPosition.x > startPosition.x)
            desiredPosition.x = startPosition.x;


        rigidbody2D.position = desiredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ResetAfterDelay());
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSecondsRealtime(3);
        rigidbody2D.position = startPosition;
        rigidbody2D.isKinematic = true;
        rigidbody2D.velocity = Vector2.zero;
    }
}
