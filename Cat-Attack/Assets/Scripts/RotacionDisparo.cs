using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionDisparo : MonoBehaviour
{
    private Vector3 objetivo;
    [SerializeField] private Camera camara;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        objetivo = camara.ScreenToWorldPoint(Input.mousePosition);
        float anguloRadiantes = Mathf.Atan2(objetivo.y - transform.position.y, objetivo.x - transform.position.x);
        float anguloGrados = (180 / Mathf.PI) * anguloRadiantes - 90;

        transform.rotation = Quaternion.Euler(0,0, anguloGrados);
    }
}
