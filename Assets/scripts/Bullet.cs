using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {
    [SyncVar]
    public Quaternion direction;
    public GameObject bloodFX;

    void Start(){
        this.transform.rotation = direction;
        this.GetComponent<Rigidbody>().velocity = this.transform.forward * 30.0f;
    }
    void Update()
    {   
        if (GameManager.hosting){
            RaycastHit hit;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            if (Physics.Raycast(transform.position, fwd, out hit, 3.0f)) 
            {
                Health health = hit.collider.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(10);
                    Destroy(this.transform.GetChild(0).GetChild(0).gameObject);
                    //fx
                    GameObject blood = (GameObject)Instantiate(bloodFX, hit.point, Quaternion.identity);
                    NetworkServer.Spawn(blood);
                    Destroy(blood, 1.2f);
                }
                Destroy(this.transform.GetChild(0).gameObject, 6);
                this.transform.GetChild(0).transform.position = hit.point;
                this.transform.GetChild(0).parent = hit.collider.gameObject.transform;
                Destroy(gameObject);
            }
        }
    }
}
