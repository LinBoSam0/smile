using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int starCount = 0;
    private int totalStars = 0;

    public Text starText;     // 顯示星星數量的 UI Text
    public GameObject winPanel; // 過關畫面（顯示「過關！」的 UI）

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // 開始時自動找到所有星星
        totalStars = GameObject.FindGameObjectsWithTag("star").Length;

        // 一開始先隱藏過關畫面
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
        Debug.Log("過關了！");
        if (winPanel != null)
            winPanel.SetActive(true);
    }
}
