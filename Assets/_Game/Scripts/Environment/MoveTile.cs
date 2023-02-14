using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTile : MonoBehaviour
{
    //[SerializeField]
    private Vector3 referenceToCenter;

    public Vector3 ReferenceToCenter { get => referenceToCenter; set => referenceToCenter = value; }
    // Axis X and Y : -20 , 0 , 20

    void Awake() {
        ReferenceToCenter = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (!other.CompareTag("Player"))
        {
            return;
        }
        StartCoroutine(TilesManager.Instance.TranslateTiles(referenceToCenter));
    }


}
