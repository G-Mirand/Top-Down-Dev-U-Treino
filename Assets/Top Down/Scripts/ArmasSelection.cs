using UnityEngine;

public class ArmasSelection : MonoBehaviour
{
    public GameObject Lanca;
    public GameObject Escudo;
    public GameObject Arco;
    private int armaSelecionada = 0; // Variável para armazenar a arma selecionada

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            armaSelecionada = 0;
            QualArma(armaSelecionada);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            armaSelecionada = 1;
            QualArma(armaSelecionada);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            armaSelecionada = 2;
            QualArma(armaSelecionada);
        }

    }
    private void QualArma(int arma) {
        switch (arma)
        {
            case 0:
                TudoFalso();
                Lanca.SetActive(true); //ativa lança
                break;

            case 1:
                TudoFalso();
                Escudo.SetActive(true); //ativa escudo
                break;

            case 2:
                TudoFalso();
                Arco.SetActive(true); //ativa arco
                break;

            default:
                break;
        }
        //Desativa todas as armas e depois ativa a arma selecionada

    }

    private void TudoFalso()
    {
        Lanca.SetActive(false);
        Escudo.SetActive(false);
        Arco.SetActive(false);
        //Seta todas as armas como falsas
    }

}
