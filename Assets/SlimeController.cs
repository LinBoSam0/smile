using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float baseJumpForce = 5f;       // 最小跳力
    public float maxJumpForce = 15f;       // 最大跳力（蓄滿）
    public float inflateScale = 1.5f;
    public float inflateSpeed = 2f;

    private Rigidbody2D rb;
    private Vector3 originalScale;
    private bool isGrounded = false;
    private bool isInflating = false;
    private float chargeTime = 0f;
    private float maxChargeTime = 1.5f; // 蓄力最大秒數

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        // 左右移動
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        // 蓄力開始：按住空白鍵
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            isInflating = true;
            chargeTime += Time.deltaTime;
            chargeTime = Mathf.Clamp(chargeTime, 0f, maxChargeTime);

            float scaleRatio = Mathf.Lerp(1f, inflateScale, chargeTime / maxChargeTime);
            transform.localScale = originalScale * scaleRatio;
        }

        // 鬆開空白鍵 → 彈跳
        if (Input.GetKeyUp(KeyCode.Space) && isInflating && isGrounded)
        {
            float jumpPower = Mathf.Lerp(baseJumpForce, maxJumpForce, chargeTime / maxChargeTime);
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);

            // 重設蓄力狀態與大小
            chargeTime = 0f;
            isInflating = false;
            transform.localScale = originalScale;
        }

        // 回復原大小（空中時或未按鍵）
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
