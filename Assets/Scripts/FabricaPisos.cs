using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabricaPisos : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] pisos; // Arreglo de pisos
    public Transform player;
    GameController gameController;

    public  int pisoActual;
    public int pisoAnterior;
    Vector3 fix;




    private void Awake()
    {
        pisoActual = 0;
        pisoAnterior = 0;
    }
    private void Start()
    {
        gameController = GameController.Instance;
        fix = new Vector3(0, 0, 0);
    }


    // Update is called once per frame
    void Update()
    {
        if (gameController.startGame == true)
        {
            if (Mathf.Abs(((pisos[pisoActual].transform.position.x-50) - player.position.x))<10)
            {
                
                if (pisoActual == 0)
                    pisoAnterior = pisos.Length - 1;
                else
                    pisoAnterior = (pisoActual - 1) % pisos.Length;

                if (pisos[pisoAnterior].transform.position.x > pisos[pisoActual].transform.position.x)
                {

                    pisos[pisoAnterior].transform.position += Vector3.left * 600;
                    //pisos[pisoAnterior].transform.position += Vector3.up *-0.5f;
                    
                }
                pisoActual = (pisoActual + 1) % pisos.Length;
            }
            fix = pisos[pisoAnterior].transform.position+Vector3.down*0.0000005f;
        }

    }

   
}
