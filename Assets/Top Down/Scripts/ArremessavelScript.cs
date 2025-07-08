using UnityEngine;
using System.Collections;


public class Arremesaveis : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;
    [SerializeField] private float RangeDamage = 10f; // Dano do ataque a distancia

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * force; 
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 180);
        StartCoroutine(SelfDestruct());
    }
   

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    void Update()
    {
     
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Inimigo inimigo = collider.GetComponent<Inimigo>();
        if (collider.CompareTag("Enemy"))
        {
            inimigo.TakeDamage(RangeDamage);// Aplica dano ao inimigo
            Destroy(gameObject);// destroi a flecha junto com o inimigo
        }
    }

}
