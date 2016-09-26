using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    void Update()
    {   
        RaycastHit hit;
        
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, 3.0f)) 
        {
            Health health = hit.collider.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(10);
                Destroy(this.transform.GetChild(0).GetChild(0).gameObject);
            }
            Destroy(this.transform.GetChild(0).gameObject, 6);
            this.transform.GetChild(0).transform.position = hit.point;
            this.transform.GetChild(0).parent = hit.collider.gameObject.transform;
            Destroy(gameObject);
        }
    }
}
