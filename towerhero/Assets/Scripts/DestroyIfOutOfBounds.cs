using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfOutOfBounds : MonoBehaviour {

    // If the projectile is out of bounds, destory it
    void Update () {

        if (OutOfBounds()) {
            Destroy(this.gameObject);
        }
    }

    // TODO - daniel632
    private bool OutOfBounds() {

        //GetComponent<MeshCollider>().bounds

        return false;
    }
}
