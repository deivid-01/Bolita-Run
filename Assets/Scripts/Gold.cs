using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{

    public Rigidbody rgGold;

    public static bool powerUpActive = false;
    public static FabricaEnemys fabricaEnemys = new FabricaEnemys();

    #region Singlenton




    #endregion
    public Gold(){

}
    public bool GetPowerUpActive()
    {
        return powerUpActive;
    }

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.name != "Pepe")
        {
            //Congelar posición
            rgGold.constraints = RigidbodyConstraints.FreezeRotation;
            rgGold.constraints = RigidbodyConstraints.FreezePosition;

            this.GetComponent<SphereCollider>().isTrigger = true;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
         if (other.name.Equals("Player"))
        {
            Debug.Log("Power Up On");
            StartCoroutine(powerUpOn());
           // powerUpOn();


            
        }
    }
   IEnumerator powerUpOn()
    {

        powerUpActive = true;
        this.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(10f);
        Debug.Log("Power up off " + Time.time);

        //Despues de 10 segundos se desactiva el powerUp
        powerUpActive = false;
        fabricaEnemys.SetGoldNumberDone(false);
        fabricaEnemys.SetDeathZoneStatus(true);
        

        
        


    }
/*
     public void powerUpOn()
    {

        powerUpActive = true;
        gameObject.SetActive(false);
        fabricaEnemys.SetGenerarGold(false);
        Debug.Log("Desactivar power up");
        //yield return new WaitForSeconds(0.)

      
       // powerUpActive = false;


    }
    
    */


    
}
