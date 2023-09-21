using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IShotHit
{
    public int speed;
    public float speedRotation;
    public Rigidbody rb;

    public List<Transform> EnemiesList = new List<Transform>();
    public float ColliderRange;
    public bool PodeAtacar;
    public Vector3 ultimaPosicao;
    public float TimeCount;

    public Text tempoPoder;

    public int life = 3;

    public GameObject SpawnPoint;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tempoPoder.enabled = false;
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.up * speedRotation * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * speedRotation * Time.deltaTime);
        }
        if (!PodeAtacar && Input.GetKeyDown(KeyCode.E))
        { 
            StartCoroutine(ForcaJEDI());
        }
        if (PodeAtacar)
        {
            TimeParaPoder();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Shotting();
        }

    }

    public void AbilityRange()
    {
        foreach (Collider c in Physics.OverlapSphere((transform.position + transform.forward), ColliderRange))
        {
            if (c.gameObject.CompareTag("Enemy"))
            {
                EnemiesList.Add(c.transform);
            }
        }
    }

    public void TimeParaPoder()
    {
        TimeCount += Time.deltaTime;
        if (TimeCount <= 5)
        {
            tempoPoder.enabled = true;
            tempoPoder.text = "Esperar tempo: " + TimeCount.ToString();
        }
        if (TimeCount >= 5)
        {
            tempoPoder.enabled = false;
            TimeCount = 0;
        }
    }

    IEnumerator ForcaJEDI()
    {
        if (!PodeAtacar)
        {
            PodeAtacar = true;
            AbilityRange();
            foreach (Transform enemies in EnemiesList)
            {
                Vector3 direcao = (enemies.position - transform.position).normalized;
                float direcaoY = transform.position.y;
                Enemy inimigo = enemies.transform.GetComponent<Enemy>();
                if (inimigo != null)
                {
                    inimigo.GetHit(direcao, direcaoY);
                }
            }
            yield return new WaitForSeconds(5f);
            PodeAtacar = false;
        }
    }

    public void OnCollisionEnter(Collision colisao)
    {
        if (colisao.gameObject.CompareTag("Bullet"))
        {
            life -= 1;
        }
    }

    void Shotting()
    {
        RaycastHit Hit;
        if(Physics.Raycast(SpawnPoint.transform.position, SpawnPoint.transform.forward, out Hit,Mathf.Infinity, LayerMask.GetMask("Hittables")))
        {
            IShotHit hitted = Hit.transform.GetComponent<IShotHit>();
            if (hitted != null)
            {
                hitted.Hit(SpawnPoint.transform.forward);
                print(hitted);
            }
        }
    }

    public void Hit(Vector3 direcao)
    {
        throw new System.NotImplementedException();
    }
}
