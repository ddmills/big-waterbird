using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    void Update()
    {   
        RaycastHit hit;
        
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, 3.0f)) 
        {
            this.transform.GetChild(0).transform.position = hit.point;
            this.transform.GetChild(0).parent = hit.collider.gameObject.transform;
            Health health = hit.collider.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(10);
            }
            Destroy(gameObject);
        }
    }
}
