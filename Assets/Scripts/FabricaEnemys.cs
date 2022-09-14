using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabricaEnemys : MonoBehaviour
{
    // Start is called before the first frame update
    #region mainPlayer Propierties

    public Transform player;
    public Rigidbody rgPlayer;

    float pivote500 = 0f; //Posicion 500m del jugador
    float pivote100 = 0f; //Posicion 100m del jugador

    float anterior = 0f; //Posicion anterior del jugador 
    #endregion

    GameController gameController;
    AdministradorPiscinas administradorPiscinas;

    #region Porcentaje de Enemigos a engenderar
    float daddyPorcent;
    float babyPorcent;
    float normalPorcent;
    float motherFuckerPorcent;

    float factorDificultad; //Porcentaje al cual se engendra cada enemigo

    string[] next;
    int numeroEnemigos;
    string patron = "";

    bool changeBaby = true;
    bool changeNormal = true;
    bool changeDaddy = true;
    bool changeMotherFucker = true;

    #endregion

    float cont = 0;
    string enemy;
    Vector3 plus;

    #region GoldObject
    public GameObject prefabGold;
    float goldNumber = 0;
    float goldTime = 0;
    public static bool goldNumberDone;
    public static bool deathZonePowerUp = true;
    #endregion

    public static Gold gold = new Gold();

   
    public bool GetGoldNumberDone()
    {
        return goldNumberDone;
    }
    public void SetGoldNumberDone(bool d)
    {
        goldNumberDone = d;
    }

    public bool GetDeathZoneStatus()
    {
        return deathZonePowerUp;
    }
    public void SetDeathZoneStatus(bool d)
    {
        deathZonePowerUp = d;
    }

    private void Awake()
    {
        anterior = player.position.x;
        pivote100 = player.position.x;
        

    }

    private void Start()
    {
        administradorPiscinas = AdministradorPiscinas.Instance;
        gameController = GameController.Instance;

        InicializarEnemigos();
    }


    // Update is called once per frame
    void Update()
    {
        if (gameController.startGame == true)
        {

            GenerarEnemigos();
           
        }

    }

    void InicializarEnemigos()
    {
        babyPorcent = 100;
        normalPorcent = 0;
        daddyPorcent = 0;
        motherFuckerPorcent = 0;
        factorDificultad = 4.1666667f;


        numeroEnemigos = 24;
        next = new string[numeroEnemigos];
        for (int i = 0; i < numeroEnemigos; i++)
        {
            next[i] = "Baby";
        }

    }

    public void  GenerarEnemigos()
    {

        StartCoroutine(PowerUpConditions());

        if (Mathf.Abs(player.position.x - anterior) > 18)
        {           
            anterior = player.position.x;

            plus = GenerarPlusDistanciaEnemigo(cont);

            enemy = KindOfEnemy();


            if (babyPorcent > 26)
            {
                EnemyRandom(enemy, plus);

                //Aumentar dificultad cada 100 metros
                //La idea es que cada 100 metros configuro el patron incluyendo más enemigos
                if (Mathf.Abs(player.position.x - pivote100) > 30)
                {
                    pivote100 = player.position.x;
                    ConfigurarPorcentajes();

                }
            }
            else
            {
                EnemyRandom(enemy, plus);
            }
            //ImprimirPatronOPorcentajes();
            cont += 0.35f; 
        }

    }

    void EnemyRandom(string enemy,Vector3 plus)
    {
        if (enemy.Equals("Gold"))
        {

           
            administradorPiscinas.SpawnFromPool("EnemigoUPowerUp","PopPowerup", player.position + plus, Quaternion.identity);

         
     
        }

        if (enemy.Equals("Baby"))
        {
           administradorPiscinas.SpawnFromPool("EnemigoUPowerUp", "Baby", player.position + plus, Quaternion.identity);
            administradorPiscinas.SpawnFromPool("EnemigoUPowerUp", "Baby", player.position + plus, Quaternion.identity);
        }
        else if (enemy.Equals("Normal"))
        {
             administradorPiscinas.SpawnFromPool("EnemigoUPowerUp", "Normal", player.position + plus, Quaternion.identity);
            
            administradorPiscinas.SpawnFromPool("EnemigoUPowerUp", "Normal", player.position + plus, Quaternion.identity);

        }
        else if (enemy.Equals("Daddy"))
        {
            administradorPiscinas.SpawnFromPool("EnemigoUPowerUp", "Daddy", player.position + plus, Quaternion.identity);


        }
        else if (enemy.Equals("MotherFucker"))
        {
            administradorPiscinas.SpawnFromPool("EnemigoUPowerUp", "MotherFucker", player.position + plus, Quaternion.identity);

        }
    }
    void ConfigurarPorcentajes()
    {
        babyPorcent -= factorDificultad;
        if (babyPorcent > 50)
        {
            normalPorcent += factorDificultad;
            ModificarPatronNormal();

        }
        else
        {
            normalPorcent -= factorDificultad;
            if (babyPorcent > 33)
            {
                daddyPorcent += factorDificultad * 2; ;
                ModificarPatronDaddy();
                ModificarPatronDaddy();
          


            }
            else
            {
                daddyPorcent -= factorDificultad;
                if (babyPorcent > 25)
                {
                    motherFuckerPorcent += factorDificultad * 3; ;
                    ModificarPatronMotherFucker();
                    ModificarPatronMotherFucker();
                    ModificarPatronMotherFucker();
                }

            }

        }



       
    }
    void ModificarPatronNormal()
    {
        for (int i = 0; i < numeroEnemigos; i++)
        {
            if (next[i].Equals("Baby"))
            {
                next[i] = "Normal";
                break;
            }
        }
    }
    void ModificarPatronDaddy() {
        for (int i = 0; i < numeroEnemigos; i++)
        {
            if (changeNormal == false && changeBaby == false) //En caso de que ya haya pasado por los dos
            {
                changeBaby = true;
                changeNormal = true;
            }

            if (next[i].Equals("Baby"))
            {
           
                if (changeBaby == true)
                {
                    next[i] = "Daddy";
                    changeBaby = false;
                    break;
                }
                else
                    continue;
            }
            else if (next[i].Equals("Normal"))
            {
                if (changeNormal == true)
                {
                    next[i] = "Daddy";
                    changeNormal = false;
                    break;
                }
                else
                    continue;
            }

        }
    }
    void ModificarPatronMotherFucker()
    {
       
        for (int i = 0; i < numeroEnemigos; i++)
        {
            

            if (next[i].Equals("Baby"))
            {
                if (changeBaby == true)
                {
                    next[i] = "MotherFucker";
                    changeBaby = false;
                    break;
                }
                else
                    continue;
            }
            else if (next[i].Equals("Normal"))
            {
                if (changeNormal == true)
                {
                    next[i] = "MotherFucker";
                    changeNormal = false;
                    break;
                }
                else
                    continue;
            }
            else if (next[i].Equals("Daddy"))
            {
                if (changeDaddy == true)
                {
                    next[i] = "MotherFucker";
                    changeDaddy = false;
                    break;
                }
                else
                    continue;
            }

        }
        if (changeNormal == false && changeBaby == false && changeDaddy == false) //En caso de que ya haya pasado por los dos
        {
            changeBaby = true;
            changeNormal = true;
            changeDaddy = true;
           
        }
        
    }
    void GenerarGoldNumber()
    {
            goldNumber = Random.Range(Time.time, Time.time+15); //Escoge un tiempo al azar de 30 segundos
        //Debug.Log(" GOLD NUMBER "+goldNumber);
        //  generarGold = false;
        goldNumberDone = true;
    }
    bool isnearGoldNumber()
    {

        if (Mathf.Abs(goldNumber - Time.time) < 0.28f)
        {
           
            return true; }
        return false;
    }
    
    Vector3 GenerarPlusDistanciaEnemigo(float cont)
    {
        return new Vector3(-30 - (cont), 5, 0); //Vector para instanciar los objetos un poco mas adelante del jugador
        
    }

    string KindOfEnemy()
    {
        /*if (isnearGoldNumber() == true && gold.GetPowerUpActive() == false)
        {
            return "Gold";
        }
        else
        {*/
            return next[Random.Range(0, next.Length - 1)];
        //}
    }

    void ImprimirPatron()
        {
        patron = "";
        for (int i = 0; i < next.Length; i++)
        {
            patron += "[" + next[i] + "] ";
        }
 


        //  Debug.Log("FINAAAL: "+(babyPorcent+normalPorcent+daddyPorcent+motherFuckerPorcent)+ "| Baby: " + babyPorcent + "%| " + "Normal: " + normalPorcent + "%| " + "Daddy: " + daddyPorcent + "%| " + "MF: " + motherFuckerPorcent + "%");

    }

    IEnumerator PowerUpConditions()
    {
        //Death Zone -> DOne
        //aleatorio ZOne ->DOne (?)
        //Active Zone

        ;
        if (deathZonePowerUp == true) // Esperar 30 segundos para empezar a generar los powerUp
        {
            deathZonePowerUp = false;
          
            yield return new WaitForSeconds(15f);
           
            if (gold.GetPowerUpActive() == false && goldNumberDone == false)
            {

                GenerarGoldNumber();

            }

        }

    }

}
