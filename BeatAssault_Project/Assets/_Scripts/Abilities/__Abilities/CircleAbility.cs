using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class CircleAbility : MonoBehaviour
{
    public Vector3 maxSize = new Vector3(5f, 0f, 5f);
    public float duration;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Increasing());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public IEnumerator Increasing()
    {
        int randomDegree = Random.Range(-180, 181);
        transform.DOScale(maxSize, duration);
        transform.Rotate(0, randomDegree, 0);
        yield return new WaitForSeconds(duration + 1f);
        Destroy(gameObject);
                    
    }
}
