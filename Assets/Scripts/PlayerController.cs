using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    float playerYPos;
    public GameObject particle;


    // Start is called before the first frame update
    void Start()
    {
        playerYPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameStarted)
        {
            if(!particle.activeInHierarchy)
            {
                particle.SetActive(true);
            }
            if (Input.GetMouseButtonDown(0))
            {
                PositionSwitch();
            }
        }
    }

    void PositionSwitch()
    {
        playerYPos = -playerYPos;
        transform.position = new Vector3(transform.position.x, playerYPos, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("obstacle"))
        {
            GameManager.Instance.UpdateLive();
            GameManager.Instance.Shake();
        }
    }
}
