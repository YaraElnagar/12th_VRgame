using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private int score = 0;
    public Text scoreText;
    public GameObject level2, level3;
    public float speed = 5;
    public GameObject PlayerHands;
    public GameObject ObjectInMyHands;
    private bool carry = false;
    public AudioClip collectSound, targetSound;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            this.transform.position += Camera.main.transform.forward * speed * Time.deltaTime;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball" && !carry)
        {
                other.gameObject.transform.SetParent(PlayerHands.transform);
                other.gameObject.transform.position = PlayerHands.transform.position;
                ObjectInMyHands = other.gameObject;
                carry = true;
                other.gameObject.GetComponent<Animator>().enabled = false;
                Camera.main.GetComponent<AudioSource>().PlayOneShot(collectSound);
        }

        if (other.gameObject.tag == "Target" && carry)
        {
                other.gameObject.SetActive(false);
                ObjectInMyHands.transform.SetParent(null);
                ObjectInMyHands.transform.position = other.gameObject.transform.position + new Vector3(0,0.5f,0);
                ObjectInMyHands.tag = "BallDown";
                ObjectInMyHands.GetComponent<Renderer>().material.color = Color.blue;
                carry = false;
                score++;
                Camera.main.GetComponent<AudioSource>().PlayOneShot(targetSound);
                scoreText.text = "Score: " + score;
                
        }

        if (score == 11)
        {
            level2.gameObject.GetComponent<Animator>().enabled = true;
        }

        if (score == 22)
        {
            level3.gameObject.GetComponent<Animator>().enabled = true;
        }

        if (score == 33)
        {
            scoreText.text = "We have a WINNER!";
        }
    }

}
