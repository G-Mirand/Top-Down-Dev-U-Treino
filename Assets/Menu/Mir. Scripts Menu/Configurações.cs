using UnityEngine;
using TMPro;

public class Configurações : MonoBehaviour
{
    public TMP_Dropdown dropdownResoluções;
    public void ChangeGraphic()
    {
        QualitySettings.SetQualityLevel(dropdownResoluções.value);
    }
}
