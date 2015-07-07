#pragma strict
public var moveSpeed : float = 5.0;										// move speed of the bird when input received
private var idlePos : boolean = true;									//check idle state 
private var startFeed : boolean = false;								//check feed state
private var anim : Animator; 											//used to set Animator parametters

function Awake(){
 anim = GetComponent(Animator);											//initialise anim var with Animator Parameters values
}

function Update () {
if(!startFeed)															//if not feeding 
{
	if (Input.GetKey("right"))
	{
		idlePos = false;												//get from idle to move when input received
		gameObject.transform.position.x += moveSpeed * Time.deltaTime;	//move skeleton on X axis over time while input received
		anim.SetBool("idle", false);									//set "idle" Animator Param to false to switch animations
		gameObject.transform.rotation.y = 0;							//if moving right skeleton faces right
	}
	if (Input.GetKey("left"))
	{
		idlePos = false;
		gameObject.transform.position.x -= moveSpeed * Time.deltaTime;	// move skeleton on X axis to the left
		anim.SetBool("idle", false);
		gameObject.transform.rotation.y = 180;							//if moving left skeleton faces left
	}
	if (Input.GetKeyUp("right") || Input.GetKeyUp("left"))				//if no input play idle anim
	{
		idlePos = true;
		anim.SetBool("idle", true);
	}
}

if (Input.GetKey("space") && idlePos)									//if Space is pressed and idle anim is playing switch anim to feed
{
	startFeed = true;
	anim.SetBool("feed", true);
} else if (Input.GetKeyUp("space") && idlePos)							//if space released play idle anim
{
	startFeed = false;
	anim.SetBool("feed", false);
}




}