using UnityEngine;
using UnityEngine.SceneManagement;

public class Inimigo : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float health, maxhealth = 20f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        health = maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        playerMovement player = collider.GetComponent<playerMovement>();
        if (collider.CompareTag("Player"))
        {
            Debug.Log("Player Hit");
            SceneManager.LoadScene("Top Down Scene");
        }
    }
}
