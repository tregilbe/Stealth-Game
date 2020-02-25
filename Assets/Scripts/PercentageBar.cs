using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PercentageBar : MonoBehaviour
{
    public float current = 100.0f;
    public float max = 100.0f;
    public Image BarImage;

    // Start is called before the first frame update
    void Start()
    {
        BarImage = gameObject.GetComponent<Image>();
        BarImage.type = Image.Type.Filled;
        BarImage.fillMethod = Image.FillMethod.Horizontal;
    }

    // Update is called once per frame
    void Update()
    {
        float percentFilled = current / max;
        BarImage.fillAmount = percentFilled;
        if (percentFilled > 0.25f)
        {
            BarImage.color = Color.green;
        }
        else
        {
            BarImage.color = Color.red;
        }
    }
}
