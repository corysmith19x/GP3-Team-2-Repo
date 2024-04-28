using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaChangePortalScript : MonoBehaviour
{
    public Material myMaterial;


    [Range(0f, 1f)]
    public float alpha = 0f;

    float minVal = 0f;
    float maxVal = 1f;
    float timeElapsed;
    public float lerpDuration = 2f;


    float lerpValue;


    private void Start()
    {
        
    }

    private void Update()
    {
        

        if (Input.GetKey(KeyCode.T))
        {
            AlphaChange();
        }

        myMaterial.color = new Color(myMaterial.color.r, myMaterial.color.g, myMaterial.color.b, alpha);
    }

    private void AlphaChange()
    {
        if (timeElapsed < lerpDuration)
        {
            lerpValue = Mathf.Lerp(minVal, maxVal, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            alpha = lerpValue;
        }
        else
        {
            lerpValue = maxVal;
        }
    }
}
