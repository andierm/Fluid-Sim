using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Windmill : MonoBehaviour
{
    [SerializeField]
    Vector2 minMaxSpeed = new Vector2(100, 300); //the min and max speed the blades can rotate
    [SerializeField]
    Vector2 changeFrequnecy = new Vector2(10, 20); //how often the wind speed changes --> we will change that to ui
    // [SerializeField]
    // float windChangeSpeed = 0.5f; // how quickly the blade matches the new wind speed

    [SerializeField]
    private float currentSpeed;
    private float targetSpeed;

    public Slider speedSlider;

    private void Start()
    {
        speedSlider.onValueChanged.AddListener(UpdateCurrentSpeed);
        currentSpeed = minMaxSpeed.x; //set the initial speed of the blades to the minimum speed
        StartCoroutine(Animate()); 
        
    }
   
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator Animate()
    {
        while (true)
        {
            // currentSpeed = speedSlider.value;
            float timeout = Time.time + Random.Range(changeFrequnecy.x, changeFrequnecy.y); //set the time to change the speed
            
            while (Time.time < timeout)
            {
                float rotationAmount = currentSpeed * Time.deltaTime;
                transform.Rotate(0f, rotationAmount, 0f, Space.Self);
                yield return null;
                
                
            }
            
        }
    }

    private void UpdateCurrentSpeed(float newSpeed)
    {
       currentSpeed = newSpeed;
    //    print("Slider Value: " + speedSlider.value);
    //    print("Current Speed: " + currentSpeed);
    }

    
}
