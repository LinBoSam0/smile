using UnityEngine;

public class Star : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // �q�������޲z��
            GameManager.Instance.CollectStar();

            // ����S�ĩέ��ġ]�i��^
            // AudioSource.PlayClipAtPoint(collectSound, transform.position);

            // �R���P�P
            Destroy(gameObject);
        }
    }
}
