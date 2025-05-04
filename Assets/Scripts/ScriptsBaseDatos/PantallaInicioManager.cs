using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaInicioManager : MonoBehaviour
{
    public void IrARegistro() => SceneManager.LoadScene("Registro");
    public void IrALogin() => SceneManager.LoadScene("Login");
}

