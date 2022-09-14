using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    AdministradorPiscinas administradorPiscinas;
    public Vector3 offset;
    public GameObject objectPowerUp;
    public static Gold gold = new Gold();
    
    private void Start()
    {
        administradorPiscinas = AdministradorPiscinas.Instance;
        
    }
    void Update()
    {



        
            if (gold.GetPowerUpActive() == true && this.transform.rotation.eulerAngles.y<185)
            {
       
               if (Input.GetKeyDown(KeyCode.Z))
                {
                    administradorPiscinas.SpawnFromPool("Pop", "Pop", this.transform.position + offset, this.transform.rotation);
                }


            }
      
    }
}
