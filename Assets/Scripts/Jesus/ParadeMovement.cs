using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//TEMPORARY CODE, COULD CHANGE BASED ON OTHER GUY'S CODE

public class ParadeMovement : MonoBehaviour
{
    // [SerializeField] float speed;
    //[SerializeField] GameObject Point1;
    //[SerializeField] GameObject Point2;

    [SerializeField] GameObject EndPoint;
    [SerializeField] float seconds;
    float timer;
    Vector2 Point;
    Vector2 Difference;
    Vector2 start;
    public float percent;


    void Start()
    {
        //Point2.transform.position = new Vector2(Point2.transform.position.x, Point1.transform.position.y);
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    	//screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    	EndPoint.transform.position = new Vector2(EndPoint.transform.position.x, transform.position.y);
    	start = transform.position;
        Point = new Vector2(EndPoint.transform.position.x, EndPoint.transform.position.y);
        Difference = Point - start;
    }

    void Update()
    {
    	// transform.position = new Vector2(Point1.transform.position.x + (speed += Time.deltaTime),Point1.transform.position.y); //Moving

    	// if(transform.position.x > Point2.transform.position.x)
    	// {
    	// 	Destroy(this.gameObject); //Temporary
    	// 	//GAME OVER SCREEN
    	// }

    	if (timer <= seconds) 
    	{
            // basic timer
            timer += Time.deltaTime;
            // percent is a 0-1 float showing the percentage of time that has passed on our timer!
            percent = timer / seconds;
            // multiply the percentage to the difference of our two positions
            // and add to the start
            transform.position = start + Difference * percent;
        }

        if(timer > seconds)
        {
        	Destroy(this.gameObject);
            SceneManager.LoadScene("GameOver");
        	//GAMEOVER SCENE
        }
    }
}