using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_movement : MonoBehaviour
{
    public GameObject player;
    private GameObject userPlayerInstance;

    // Start is called before the first frame update
    void Start()
    {
        userPlayerInstance = (GameObject)Instantiate(player);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(userPlayerInstance.transform.position);
    }
}
