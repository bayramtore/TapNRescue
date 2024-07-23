using UnityEngine;
using TMPro;

public class SafeArea : MonoBehaviour
{
    RectTransform rectTransform;
    Rect safeArea;

    //TEST
    public TMP_Text fpsText;
    public float deltaTime;
    public bool showFPS = false;

    public static SafeArea Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Application.targetFrameRate = 120;
#if UNITY_IOS
      Application.targetFrameRate = 60;
#endif


    }

    private void Start()
    {
        rectTransform = this.GetComponent<RectTransform>();
        safeArea = Screen.safeArea;
        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = anchorMin + safeArea.size;
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;
        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
    }

    void Update()
    {
        if (!showFPS)
        {
            fpsText.gameObject.SetActive(false);
        }
        else
        {
            //deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / Time.deltaTime;
            fpsText.text = Mathf.Ceil(fps).ToString();
        }
    }

}



    

/*
    public float GetSliderValue()
    {
        return slider.value;
    }

    public void UpdateSliderValueText(float val)
    {
        sliderValue.text = "V = " + val; 
    }
*/

   



