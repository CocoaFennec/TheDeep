using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    GameObject camera;
    Transform cameraTransform;
    Transform transform;
    GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraTransform = camera.GetComponent<Transform>();
        transform = GetComponent<Transform>();
        transform.Translate(new Vector3(0, gameController.seaLevel, 0));
    }

    // Update is called once per frame
    void Update()
    {
        cameraTransform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
