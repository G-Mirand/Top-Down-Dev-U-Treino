using UnityEditor.VersionControl;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public GameObject MeleeAttackArea;
    bool isAttacking = false;
    public float atkDuration = 0.1f;
    public float atkTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        CheckMeleeTimer();

        if (Input.GetKeyDown(KeyCode.Mouse0)) //Verifica se jogador apertou o botao esquerdo do mouse pra atacar
        {
            OnAttack();
        }

    }

    private void OnAttack()
    {
        if (!isAttacking)
        {
            MeleeAttackArea.SetActive(true);
            isAttacking = true;
            //Animação de ataque melee vem aqui
        }
    }

    private void CheckMeleeTimer()
    {
        if (isAttacking)
        {
            atkTimer += Time.deltaTime;
            if (atkTimer >= atkDuration)
            {
                MeleeAttackArea.SetActive(false);
                isAttacking = false;
                atkTimer = 0f;
            }
        }
    }
}
