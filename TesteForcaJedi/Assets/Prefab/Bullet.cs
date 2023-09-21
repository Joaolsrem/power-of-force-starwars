using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int speedBullet = 9;

    void Start()
    {
        
    }


    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speedBullet);
    }
}
