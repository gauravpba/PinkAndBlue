using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    GameManager manager;
    SpriteRenderer sprite;

    private enum PlatformState { Neutral, Pink, Blue};

    
    private PlatformState currentState;


    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        sprite = GetComponent<SpriteRenderer>();
        currentState = PlatformState.Neutral;
    }

   
    private void OnCollisionEnter2D(Collision2D other)
    {       
        if (currentState == PlatformState.Neutral && !CompareTag("StartPlatform") && !CompareTag("FinishPlatform"))
        {
            if (other.gameObject.CompareTag("Player1"))
            {
                sprite.material = manager.pinkPlatform;
                currentState = PlatformState.Pink;
                gameObject.layer = 8;
            }
            else if (other.gameObject.CompareTag("Player2"))
            {
                sprite.material = manager.bluePlatform;
                currentState = PlatformState.Blue;
                gameObject.layer = 9;
            }
        }

        if (currentState == PlatformState.Neutral && CompareTag("FinishPlatform"))
        {
            if (other.gameObject.CompareTag("Player1"))
            {
                manager.isPlayer1OnFinish = true;
            }
            else if (other.gameObject.CompareTag("Player2"))
            {
                manager.isPlayer2OnFinish = true;
            }
        }        
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(currentState == PlatformState.Neutral && CompareTag("FinishPlatform"))
        {
            if (other.gameObject.CompareTag("Player1"))
            {
                manager.isPlayer1OnFinish = false;
            }
            else if (other.gameObject.CompareTag("Player2"))
            {
                manager.isPlayer2OnFinish = false;
            }
        }

    }
    

}
