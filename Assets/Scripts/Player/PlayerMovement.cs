using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f; // The speed the player will move at 

    Vector3 movement; // Vector to store the direction of the player's movement
    Animator anim; // Reference to the animator component
    Rigidbody playerRidgidbody; // Reference to the player's rigidbody
    int floorMask; // A layer mask so that a ray can be cast just at gameobjects on the floor layer
    float camRayLength = 100f; // Length of the ray from the camera into the scene

    void Awake() 
    {
    	floorMask = LayerMask.GetMask ("Floor");

    	// Setting up reference
    	anim = GetComponent<Animator> (); //looking for animator
    	playerRidgidbody = GetComponent<Rigidbody> (); // looking foor Rigidbody
    }

    // Grabs initial position horizontal & vertical
    // Updates the character at each frame
    void FixedUpdate() 
    {
    	float h = Input.GetAxisRaw("Horizontal");
    	float v = Input.GetAxisRaw("Vertical");

    	Move (h, v);
    	Turning();
    	Animating(h, v);
    }

    // Move the player around the scene
    void Move(float h, float v) 
    {
    	// Set the movement vector based on the axis input
    	movement.Set (h, 0f, v);

    	//makes it so by moving diagonal doesn't make you go farther
    	// Normalise the movement vector and make it proportional to the speed per second
    	movement = movement.normalized * speed * Time.deltaTime;

    	// Move the player to it's current position plus the movement
    	playerRidgidbody.MovePosition (transform.position + movement);
    }

    // Turn the player to face the mouse cursor
    void Turning()
    {
    	// Creates a ray from the mouse cursor on screen in the directioon of the camera
    	Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

    	// Creates a RaycastHit variable to store information about what was hit by the ray
    	RaycastHit floorHit;

    	// If ray hits something on floor...
    	if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) 
    	{
    		// point it hit the floor - transform position of player
    		Vector3 playerToMouse = floorHit.point - transform.position;

    		// Ensures ray is entirely on the floor plane
    		playerToMouse.y = 0f;

    		// Create a quaternion (rotation) based on looking down the vector from the player to the mouse
    		Quaternion newRotation = Quaternion.LookRotation (playerToMouse);

    		// Set the player's rotation to this new rotation
    		playerRidgidbody.MoveRotation (newRotation);
    	}
    }

    // If we are walking set IsWalking variable to true animate the character
    void Animating (float h, float v)
    {
    	bool walking = h != 0f || v != 0f; // did we press the horizontal or vertical component?
    	anim.SetBool ("IsWalking", walking);
    }

}
