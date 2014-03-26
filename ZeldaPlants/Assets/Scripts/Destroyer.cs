using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Break();
            return;
        }

        var parent = other.gameObject.transform.parent;
        Destroy(parent ? parent.gameObject : other.gameObject);
    }
}
