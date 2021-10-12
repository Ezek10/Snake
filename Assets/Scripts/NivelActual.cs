using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NivelActual : MonoBehaviour
{
    public Text textNivel;
    void Start()
    {
        textNivel = textNivel.GetComponent<Text>();
        textNivel.text = "Entrada Montaña\nNivel 1";
    }
    public void NuevoNivel(GameObject nuevoNivel, GameObject lugar)
    {
        textNivel.text = lugar.name + "\n" + nuevoNivel.name;
    }
}
