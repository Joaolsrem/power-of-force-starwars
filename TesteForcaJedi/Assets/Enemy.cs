using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    public Rigidbody rb;
    public Player p1;
    public GameObject bullet;
    public GameObject spawnerPoint;
    public float VelocidadeBala = 5;
    public bool podeAtirar;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        p1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        bullet = GameObject.FindGameObjectWithTag("Bullet");
    }

    void Update()
    {
        FindPlayer();
    }

    public void GetHit(Vector3 forcaDirecao, float posicaoY)
    {
        rb.AddForce(forcaDirecao.normalized * 15, ForceMode.Impulse);
        rb.AddForce(new Vector3(0, posicaoY * 15, 0), ForceMode.Impulse);
    }
    public void FindPlayer()
    {
        float distance = Vector3.Distance(transform.position, p1.transform.position);
        if (distance <= 50)
        {
            gameObject.transform.LookAt(p1.transform.position);
            if (!podeAtirar)
            {
                Debug.Log("Preparando o tiro - ENEMY");
                AtirarBalas();
            }
            
        }
    }

    public void AtirarBalas()
    {
        StartCoroutine(atirarBala());
    }

    IEnumerator atirarBala()
    {
        if (!podeAtirar)
        {
            podeAtirar = true;
            GameObject bala = Instantiate(bullet);
            bala.transform.position = spawnerPoint.transform.position;
            bala.transform.rotation = spawnerPoint.transform.rotation;
            yield return new WaitForSeconds(1f);
            Destroy(bala, 1.4f);
            podeAtirar = false;
        }
    }

}
