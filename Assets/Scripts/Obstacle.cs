using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float speed = 10;

    [SerializeField] private bool isObstacle = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.position += Vector3.back * speed * Time.deltaTime;

	    if (transform.position.z < -100)
	        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(!isObstacle)
            return;        

        if (collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<Player>().OnObstacleCollision();
        }


    }
}
