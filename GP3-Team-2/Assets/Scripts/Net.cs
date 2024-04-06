using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour
{
    public float timer = 0f;
    public float expandTime = 3f;
    public float maxSize = 2f;

    public bool isMaxSize = false;

    public float rotateSpeed;
    [SerializeField] Vector3 rotationDirection = new Vector3();

    void Start()
    {
        StartCoroutine(Expand());
    }

    private IEnumerator Expand()
    {
        Vector3 startScale = transform.localScale;
        Vector3 maxScale = new Vector3(maxSize, maxSize, maxSize);

        do 
        {
            transform.localScale = Vector3.Lerp(startScale, maxScale, timer/expandTime);
            transform.Rotate(rotateSpeed * rotationDirection * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        } while (timer < expandTime);

        isMaxSize = true;
    }

    void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
