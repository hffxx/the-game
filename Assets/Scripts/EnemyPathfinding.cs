using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * (moveSpeed * Time.fixedDeltaTime));

        if (moveDirection != Vector2.zero)
        {
            myAnimator.SetFloat("moveX", moveDirection.x);
            myAnimator.SetFloat("moveY", moveDirection.y);
            mySpriteRenderer.flipX = moveDirection.x < 0;
        }
    }

    public void MoveTo(Vector2 targetPosition)
    {
        moveDirection = targetPosition;
    }
}
