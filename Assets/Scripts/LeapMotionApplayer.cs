using LeapVR;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeapMotionApplayer : MonoBehaviour
{
    [SerializeField]
    private LeapMotion leapMotion;

    [SerializeField] private CharacterController characterController;
    [SerializeField] private float speed = 0.001f;
    [SerializeField] private Transform rotationTarget;
    private float jumpHeight = 1;
    [SerializeField] private float jumpSpeed = 0.5f;
    [SerializeField] private Image jumpBar;

    [SerializeField] private ParticleSystem hightJumpParticles;
    [SerializeField] private ParticleSystem normalJumpParticles;
    [SerializeField] private Text pointsText;
    [SerializeField] private AudioSource flameSound;

    private Vector3 movePos;
    private Animator animtor;
    private float curY;
    private bool inJump;
    private bool hightJump = false;
    private float curJumpDuration;
    private Vector3 velocity;
    private float points;
    private Player playerScript;

    private const float HeightJumpSpeed = 10;
    private const float HeightJumpDuration = 3;
    private const float DeJumpDuration = 30;
    private const int BonusPerSecond = 2;

    // Use this for initialization
	void Start ()
	{
	    animtor = characterController.gameObject.GetComponent<Animator>();
	    playerScript = characterController.GetComponent<Player>();

    }

    // Update is called once per frame
    void Update () {

	    Vector2 LeapPosition = leapMotion.PalmOnViewportXZ();

	    var velocityX = 0.5f - LeapPosition.x;
	    var velocityY = 0.5f - LeapPosition.y;

        if (playerScript.IsDead && leapMotion.ExtendedFingers() == 4)
        {
            SceneManager.LoadScene("1");
        }

	    if (characterController.gameObject.activeInHierarchy)
	    {
	        points += (BonusPerSecond * Time.deltaTime * velocity .sqrMagnitude * 10) + BonusPerSecond * Time.deltaTime/5;
	        pointsText.text = Mathf.RoundToInt(points).ToString();
	    }

        if (!leapMotion.IsUsed() && LeapPosition.x == -1 && LeapPosition.y == -1)
	    {
	        animtor.SetFloat("Speed", 0);

            characterController.Move(Vector3.down * Time.deltaTime* DeJumpDuration);

	        return;
	    }

	    velocityX *= -1;
	    velocity = new Vector3(velocityX, 0, velocityY) * speed;

        characterController.transform.rotation = new Quaternion(velocityY/15,-velocityX/15,-velocityX/13,1);
	    rotationTarget.rotation = new Quaternion(velocityY*5,-velocityX*5,-velocityX*5,1);

        if(!inJump)
	        inJump = leapMotion.ExtendedFingers() == 0;
        else
        {
            hightJump = leapMotion.ExtendedFingers() > 0 && leapMotion.ExtendedFingers() < 3 && curJumpDuration != 0;

            inJump = hightJump || leapMotion.ExtendedFingers() == 0;
        }

	    if (hightJump)
	        curJumpDuration = Mathf.Clamp(curJumpDuration - Time.deltaTime, 0, HeightJumpDuration);
        else
            if(inJump)
                curJumpDuration = Mathf.Clamp(curJumpDuration - Time.deltaTime/2, 0, HeightJumpDuration);
            else
                curJumpDuration = Mathf.Clamp(curJumpDuration + Time.deltaTime, 0, HeightJumpDuration);


	    animtor.SetBool("IsJumping", inJump);

	    if (hightJump)
	    {
	        flameSound.mute = false;

            hightJumpParticles.Play();
            flameSound.volume = 1;
	    }
	    else
	    {
	        if (inJump)
	        {
                normalJumpParticles.Play();
	            flameSound.mute = false;
                flameSound.volume = 0.3f;
                hightJumpParticles.Stop();
            }
            else
	        {
	            normalJumpParticles.Stop();
	            hightJumpParticles.Stop();
	            flameSound.mute = true;

            }
        }

	    if (!hightJump)
	    {
	        curY = inJump ? jumpHeight : -jumpHeight * DeJumpDuration;

	        if (characterController.transform.position.y < 0.1f && !inJump)
	        {
	            curY = 0;
	        }
	    }
        else
	    {
	        curY += HeightJumpSpeed * Time.deltaTime;
	    }

	    //characterController.transform.position = new Vector3(characterController.transform.position.x, curY, characterController.transform.position.z);

	    velocity += Vector3.up * curY * Time.deltaTime;
	    Debug.Log(leapMotion.ExtendedFingers() + " curY: " + curY);


        characterController.Move(velocity);

        animtor.SetFloat("Speed",velocity.magnitude);

	    jumpBar.fillAmount = curJumpDuration / HeightJumpDuration;

	    
	}
}

