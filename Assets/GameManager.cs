using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int starCount = 0;
    private int totalStars = 0;

    public Text starText;     // ��ܬP�P�ƶq�� UI Text
    public GameObject winPanel; // �L���e���]��ܡu�L���I�v�� UI�^

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // �}�l�ɦ۰ʧ��Ҧ��P�P
        totalStars = GameObject.FindGameObjectsWithTag("star").Length;

        // �@�}�l�����ùL���e��
        if (winPanel != null)
            winPanel.SetActive(false);

        UpdateUI();
    }

    public void CollectStar()
    {
        starCount++;
        UpdateUI();

        if (starCount >= totalStars)
        {
            ShowWinPanel();
        }
    }

    void UpdateUI()
    {
        if (starText != null)
            starText.text = "Stars: " + starCount + "/" + totalStars;
    }

    void ShowWinPanel()
    {
        Debug.Log("�L���F�I");
        if (winPanel != null)
            winPanel.SetActive(true);
    }
}
