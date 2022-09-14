using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using EZCameraShake;
using UnityEngine.UI;
public class Movement : MonoBehaviour
{

    

      GameController gameController;
    public Score score;
    // Encuentrame 
    public float forwardForce;
    public float sidewaysForce;
    public float plus;
    public float plusSide;

    public Rigidbody pepe; //CuerpoRigido de mainPLayer
    public float speedPepe; // Velocidad de mainPlayer
    public Transform pivote;  //Pivote de rotación de la camara
    public Transform camera;
    public Vector3 offset;

    float posInicial; //Posición inicial de mainPlayer 
    Quaternion inicialRotacion; // Rotación inicial de mainPlayer

    string path; //Ruta relativa del archivo a guardar

    public static Gold gold=new Gold();

    public bool rotationDone = false;

    Quaternion ideal = new Quaternion(0, -180, 0,1);

    Vector3 lastVelocity;

    public Vector3 actualPosition;



    #region Singlenton

    public static Movement Instance;

    void Awake()
    {
        path = Application.dataPath + "/timePosicion.txt";  // Ruta relativa del archivo

        Instance = this;
    }


    #endregion


    public Movement()
    {

    }
    

     void Start()
    {
        gameController = GameController.Instance;

        DeleteFile();
        //Guardar Posición inicial   
        SaveInicialRotPos();
        //Añadir Fuerza inicial para un movimiento inicial suave
        //pepe.AddForce(-speedPepe*50* Time.deltaTime, 0, 0);     
        score = Score.Instance;
    }

     void LateUpdate()//Smooth asigning of position
    {
        
          
    }
    void Update()
    {
        actualPosition = this.transform.position;
        if (gameController.startGame == true)
        {
            lastVelocity = pepe.velocity;
            //Debug.Log(this.transform.position.x - posInicial);
            #region Fuerza Constante despues de los 50 metros
            sidewaysForce += plusSide;
            forwardForce += plus;
            #endregion

            #region Save Data(Time vs Posicion)

            //CreateText(Time.time,this.transform.position.x);
            //Tiempo vs Aceleración



            #endregion

            #region OutOfRange
            if (this.transform.position.z > 7.5 || this.transform.position.z < -7.5)
            {
                pepe.useGravity = true;
            }

            #endregion
        }
    }
    private void FixedUpdate()
    {
        if (gameController.startGame == true)
        {
           
            //  Debug.Log("Game Started");
            #region Control Movement

            pepe.AddForce(-forwardForce * Time.deltaTime, 0, 0);

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                
                pepe.AddForce(0, 0, -sidewaysForce * Time.deltaTime, ForceMode.VelocityChange);
                //this.transform.position = this.transform.position - Vector3.forward * 12.5f * Time.deltaTime;
                pivote.Rotate(Vector3.up * 10f * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                pepe.AddForce(0, 0, sidewaysForce * Time.deltaTime, ForceMode.VelocityChange);
                //this.transform.position = this.transform.position + Vector3.forward * 12.5f * Time.deltaTime;
                pivote.Rotate(Vector3.up * -10f * Time.deltaTime);

            }
            else
                //pivote.rotation = Quaternion.Lerp(pivote.rotation, inicialRotacion, 4 * Time.deltaTime);

            if (this.transform.position.y < -1.5)
            {
                gameController.EndGame();
            }

            #endregion
            #region Control Rotation
            if (gold.GetPowerUpActive() == true && rotationDone == false)
            {
               // this.transform.rotation = Quaternion.Lerp(this.transform.rotation, ideal, 0.007f * Time.deltaTime);
            }

            if (gold.GetPowerUpActive() == false && this.transform.rotation.y < -170)
            {
                Debug.Log("Get me back");
            }
            /*
             Rotacion PowerUpActivefalse y rotation <185
             */

            #endregion
        }
    }
    void CreateText(float time,float x)
    {
        

        string line = time + " " + -1*x + "\n";
        File.AppendAllText(path,line);



    }


    void DeleteFile()
    {
        File.Delete(path);
    }
    void SaveInicialRotPos()
    {
        posInicial = this.transform.position.x;
        inicialRotacion = pivote.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        

        
            pepe.useGravity = true;
            // CameraShaker.Instance.ShakeOnce(7f, 10f, .1f, 1f);



            gameController.EndGame();
         

    }


}
