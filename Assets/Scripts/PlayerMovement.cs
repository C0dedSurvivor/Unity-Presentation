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
            myTransform.Translate(new Vector3(speed, 0, 0));
        }
        if (Input.GetKey(KeyCode.A))
        {
            myTransform.Translate(new Vector3(-speed, 0, 0));
        }
        Jump();
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && Physics.Raycast(transform.position + new Vector3(0, -0.5f, 0), Vector3.down, 0.1f))
        {
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
}
