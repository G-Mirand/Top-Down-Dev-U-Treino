using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletScript : MonoBehaviour
{
    GameObject target;
    public float speed = 5f;
    Rigidbody2D bulletRB;

    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bulletRB.linearVelocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(this.gameObject, 3f); // Destroy bullet after 2 seconds
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
