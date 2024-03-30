using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderChange : MonoBehaviour
{
    public Slider speedSlider;
    public TextMeshProUGUI speedValue;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speedValue.text = speedSlider.value.ToString("0.0");
    }
}
