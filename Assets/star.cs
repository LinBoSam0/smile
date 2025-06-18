using UnityEngine;

public class Star : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 通知收集管理器
            GameManager.Instance.CollectStar();

            // 播放特效或音效（可選）
            // AudioSource.PlayClipAtPoint(collectSound, transform.position);

            // 摧毀星星
            Destroy(gameObject);
        }
    }
}
