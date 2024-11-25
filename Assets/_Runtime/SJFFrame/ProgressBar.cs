using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public float duration = 2.0f; // Thời gian để tăng dần Width
    public TextMeshProUGUI TextName;
    public TextMeshProUGUI TextTime1;
    public TextMeshProUGUI TextTime2;
    public float targetWidth = 150.0f; // Width mục tiêu
    public float initialWidth;
    public float elapsedTime = 0.0f;
    public bool isScaling = false;
    public HorizontalLayoutGroup horizontalLayoutGroup;

    void Start()
    {
        horizontalLayoutGroup = GetComponentInParent<HorizontalLayoutGroup>();
        RectTransform rectTransform = GetComponent<RectTransform>();
        initialWidth = rectTransform.sizeDelta.x;
    }

    void Update()
    {
        if (isScaling)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            RectTransform rectTransform = GetComponent<RectTransform>();
            float newWidth = Mathf.Lerp(initialWidth, targetWidth, t);
            rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y);
            horizontalLayoutGroup.SetLayoutHorizontal();

            // Tính toán giá trị của TextTime2 dựa trên TextTime1 và thời gian
            float time2Value = Mathf.Lerp(float.Parse(TextTime1.text), float.Parse(TextTime1.text) + duration, t);
            TextTime2.text = time2Value.ToString("F2"); // Hiển thị số thập phân với 2 chữ số sau dấu phẩy

            if (t >= 1.0f)
            {
                isScaling = false;
            }
        }
    }

    public void StartScaling()
    {
        elapsedTime = 0.0f;
        isScaling = true;
    }
}

