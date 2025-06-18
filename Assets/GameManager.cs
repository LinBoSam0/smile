using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int starCount = 0;
    public Text starText; // �� UI �����i��

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void CollectStar()
    {
        starCount++;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (starText != null)
            starText.text = "Stars: " + starCount.ToString();
    }
}
