﻿using UnityEngine;

public class DestroyIfOutOfView : MonoBehaviour {

    // If the projectile is not visible, destroy it
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
