using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float baseJumpForce = 5f;       // �̤p���O
    public float maxJumpForce = 15f;       // �̤j���O�]�W���^
    public float inflateScale = 1.5f;
    public float inflateSpeed = 2f;

    private Rigidbody2D rb;
    private Vector3 originalScale;
    private bool isGrounded = false;
    private bool isInflating = false;
    private float chargeTime = 0f;
    private float maxChargeTime = 1.5f; // �W�O�̤j���

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

        // �W�O�}�l�G����ť���
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            isInflating = true;
            chargeTime += Time.deltaTime;
            chargeTime = Mathf.Clamp(chargeTime, 0f, maxChargeTime);

            float scaleRatio = Mathf.Lerp(1f, inflateScale, chargeTime / maxChargeTime);
            transform.localScale = originalScale * scaleRatio;
        }

        // �P�}�ť��� �� �u��
        if (Input.GetKeyUp(KeyCode.Space) && isInflating && isGrounded)
        {
            float jumpPower = Mathf.Lerp(baseJumpForce, maxJumpForce, chargeTime / maxChargeTime);
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);

            // ���]�W�O���A�P�j�p
            chargeTime = 0f;
            isInflating = false;
            transform.localScale = originalScale;
        }

        // �^�_��j�p�]�Ť��ɩΥ�����^
        if (!Input.GetKey(KeyCode.Space) && !isInflating)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * inflateSpeed);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
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
