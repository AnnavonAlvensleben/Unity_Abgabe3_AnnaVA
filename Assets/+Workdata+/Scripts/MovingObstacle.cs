using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public float direction = 0f;
    [SerializeField] private float speed = 5f;
    
    void Start()
    {
        StartCoroutine(MoveObstacle());
    }
    
    void Update()
    {
       gameObject.transform.Translate(direction * speed * Time.deltaTime, 0f, 0f);
    }
    IEnumerator MoveObstacle()
    {
        while (true)
        {
               for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.1f);
            direction = -1;
        }
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.1f);
            direction = 1;
        }
        }
     
    }

}

