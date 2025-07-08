using UnityEngine;

public class MeleeAtributos : MonoBehaviour
{
    [SerializeField] private float MeleeDamage = 20f; // Dano do ataque

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Inimigo inimigo = collision.GetComponent<Inimigo>();
        if (collision.CompareTag("Enemy"))
        {
            inimigo.TakeDamage(MeleeDamage);// Aplica dano ao inimigo
        }

    }
}
