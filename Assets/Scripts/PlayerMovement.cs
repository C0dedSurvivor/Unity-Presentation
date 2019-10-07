using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Transform myTransform;
    Rigidbody myRigidbody;
    public float speed = 0.1f;

    public Text scoreDisplay;

    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = gameObject.GetComponent<Transform>();
        myRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            myTransform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.A))
        {
            myTransform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if(Input.GetKey(KeyCode.Space))
            Jump();
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit interactable;
            if(Physics.Raycast(new Ray(transform.position + new Vector3(0.4f, -0, 0), Vector3.right), out interactable, 0.11f))
            {
                interactable.collider.gameObject.BroadcastMessage("Interact");
            }
        }
    }

    public void Jump()
    {
        int layerMask = 1 >> 8;
        layerMask = int.MaxValue - layerMask;
        Debug.Log("Trying Jumping " + Time.timeSinceLevelLoad);
        if (Physics.Raycast(transform.position + new Vector3(0, -0.4f, 0), Vector3.down, 0.11f, layerMask))
        {
            Debug.Log("Jumping " + Time.timeSinceLevelLoad);
            myRigidbody.AddForce(new Vector3(0, 150, 0));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Coin")
        {
            Destroy(other.gameObject);
            score++;
            scoreDisplay.text = "Score: " + score;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 9)
        {
            collision.gameObject.GetComponent<Rigidbody>().useGravity = true;
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
        }

        if(collision.gameObject.tag == "KillPlane")
        {
            transform.position = new Vector3(0, 1.5f, 0);
        }
    }
}
