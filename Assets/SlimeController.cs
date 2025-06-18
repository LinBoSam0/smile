using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float inflateScale = 1.5f;
    public float inflateSpeed = 2f;

    private Rigidbody2D rb;
    private Vector3 originalScale;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        // ���k����
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        // ���D
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // �����Y�p
        if (Input.GetKey(KeyCode.Space))
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale * inflateScale, Time.deltaTime * inflateSpeed);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * inflateSpeed);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // ²�氻���O�_���b�a�W
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
