using UnityEngine;
using System.Collections;

public class Flier : MonoBehaviour {
    public GameObject target;

    void OnDrawGizmos(){
        Gizmos.color = Color.green;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        Gizmos.DrawRay(transform.position, direction);
    }
    void Update()
    {   
        if (target && Random.value > 0.02){
            transform.LookAt(target.transform);}
        else {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            target = players[Random.Range (0, players.Length)];
        }

        GetComponent<Rigidbody>().velocity = transform.forward * 3f;

        RaycastHit hit;
        
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, 100.0f)) 
        {
        }
    }
    void OnCollisionStay(Collision collision) {
        GameObject hit = collision.gameObject;
        if (hit.tag == "Player")
            hit.GetComponent<Health>().TakeDamage(0.2f);
    }
}
