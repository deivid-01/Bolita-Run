using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdministradorPiscinas : MonoBehaviour
{
    // Start is called before the first frame update
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }


    #region Singlenton
    public static AdministradorPiscinas Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

   // GoldACtion goldAction;

    public List<Pool> pools; //Lista de las piscinas

    public Rigidbody rgPlayer;


    public Dictionary<string, Queue<GameObject>> poolDictionary;

     void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i <pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);

        }

      //   goldAction = GoldACtion.Instance;

    }
    public GameObject SpawnFromPool(string kindOf,string tag, Vector3 position, Quaternion rotation)
    {
       if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + "doesn't exist");
            return null;
        }
        #region Extraer de la piscina
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);

       

       
        #endregion

        if (kindOf.Equals("EnemigoUPowerUp"))
        {
            

            position.z = Random.Range(-6, 6); //Posición Random en z
                                              rotation.y = Random.Range(-360, 360);
                                              rotation.z = Random.Range(0, 360);
                                              rotation.x = Random.Range(0, 360);
            objectToSpawn.GetComponent<Rigidbody>().velocity = -1 * Vector3.down * rgPlayer.velocity.x / 3f;
            objectToSpawn.transform.rotation = rotation;
            if (tag.Equals("PopPowerup"))
            {
                objectToSpawn = RestaurarPropiedadesPop(objectToSpawn);
              

            }
        }
        else if (kindOf.Equals("Pop"))
        {
            objectToSpawn.GetComponent<Rigidbody>().velocity = 3* Vector3.right * rgPlayer.velocity.x;
            
        }
        objectToSpawn.transform.position = position;
        
        //Volver a guardar en la piscina
        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }

    public GameObject RestaurarPropiedadesPop(GameObject powerUpPop)
    {
        powerUpPop.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
        powerUpPop.GetComponent<SphereCollider>().isTrigger = false;
        powerUpPop.transform.rotation = Quaternion.Euler(-90f, 90f, 0f);
        powerUpPop.GetComponent<MeshRenderer>().enabled = true;
        return powerUpPop;
    }

}
