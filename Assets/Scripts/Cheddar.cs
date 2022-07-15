using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheddar : MonoBehaviour
{
    GameManager gameManager; 

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other) {
        gameManager.GetCheddar();
        Destroy(gameObject);
    }
}
