using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Animator m_anim;
	private bool isMoving;
	public float m_moveSpeed = 0.5f;
	private float m_weapons = 0;
	public float horizontalSpeed = 3.0F;
     public float verticalSpeed = 3.0F;
	Vector3 m_MousePosition;
	public AudioSource[] sounds;
	Rigidbody clone;
	Rigidbody clone1;
	Rigidbody clone2;
	Rigidbody clone3;
	Rigidbody clone4;
	Rigidbody clone5;


	public Rigidbody pistolBullet;
 	public Transform Spawnpoint;

	
	//float speed = 1.0f;
	void Start () {
		m_anim = gameObject.GetComponent<Animator>();
		sounds = GetComponents<AudioSource>();
		//Debug.Log(sounds.Length);
	}

	// Update is called once per frame
	void Update () {
	
		m_MousePosition  = Input.mousePosition;
		m_MousePosition.z = 5.23f;
		Vector3 tmpPos = Camera.main.WorldToScreenPoint(transform.position);
		m_MousePosition.x = m_MousePosition.x - tmpPos.x; 
		m_MousePosition.y = m_MousePosition.y - tmpPos.y;
		float angle = Mathf.Atan2(m_MousePosition.y, m_MousePosition.x) * Mathf.Rad2Deg;
		transform.localRotation = Quaternion.Euler(new Vector3(90, 0, angle));
		 
		if(Input.GetAxis("Horizontal") != 0) {
			Vector3 pos = gameObject.transform.position;
			pos.x += Input.GetAxis("Horizontal") * m_moveSpeed;
			gameObject.transform.position = pos;
			isMoving = true;
		} 

		if(Input.GetAxis("Vertical") != 0) {
			Vector3 pos = gameObject.transform.position;
			pos.z += Input.GetAxis("Vertical") * m_moveSpeed;
			gameObject.transform.position = pos;
			isMoving = true;
			} else if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0) {
			isMoving = false;
		}
		UpdateAnim();

		if(Input.GetKeyDown(KeyCode.E)) {
			m_weapons++;
			if(m_weapons>2){
				m_weapons=0;
			}
		} if(Input.GetKeyDown(KeyCode.Q)) {
			m_weapons--;
			if(m_weapons<0){
				m_weapons=3;
			}
		}

		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			Debug.Log("StartShoot");
			m_anim.SetBool("Shoot", true);
			ShootWeapon();
		} if(Input.GetKeyUp(KeyCode.Mouse0)) {
			m_anim.SetBool("Shoot", false);
		}

		if(Input.GetKeyDown(KeyCode.R) ||Input.GetKeyDown(KeyCode.Mouse1) ) {
			StartCoroutine("WeaponReload");
		}
	}

	void ShootWeapon() {
		if(m_weapons == 0){
			Rigidbody clone;
			sounds[0].Play();
			clone = (Rigidbody)Instantiate(pistolBullet, Spawnpoint.position, pistolBullet.rotation);
			clone.velocity = Spawnpoint.TransformPoint (Input.mousePosition);
		} 
		else if(m_weapons == 1){
			sounds[1].Play();
			clone = (Rigidbody)Instantiate(pistolBullet, Spawnpoint.position, pistolBullet.rotation);
			clone.velocity = Spawnpoint.TransformPoint (Input.mousePosition);
		} 
		else if(m_weapons == 2){
			sounds[2].Play();
		}
	}


	IEnumerator WeaponReload(){
		if(m_weapons == 0){
			Debug.Log("Start");
			m_anim.SetBool("Reload", true);
			yield return new WaitForSeconds(3);
			Debug.Log("End");
			m_anim.SetBool("Reload", false);
		} 
		else if(m_weapons == 1){
			Debug.Log("Start");
			m_anim.SetBool("Reload", true);
			yield return new WaitForSeconds(2.5f);
			Debug.Log("End");
			m_anim.SetBool("Reload", false);
		} 
		else if(m_weapons == 2){
			Debug.Log("Start");
			m_anim.SetBool("Reload", true);
			yield return new WaitForSeconds(5);
			Debug.Log("End");
			m_anim.SetBool("Reload", false);
		}
	}

	void UpdateAnim () {
		//If weapon is pistol5
		if(m_weapons == 0){
			if(isMoving==true) {
				//Set PistolMove Animation
				m_anim.SetInteger("Action", 1);

			} else {
				//Set PistolIdle Animation
				m_anim.SetInteger("Action", 0);
			}
		}

		//If weapon is Rifle
		if(m_weapons == 1) {
			if(isMoving == true) {
				//Set Rifle Move Animation
				m_anim.SetInteger("Action", 4);
			} else {
				//Set Rifle Idle Animation
				m_anim.SetInteger("Action", 3);
			}
		}

		//If weapon is Shotgun
		if(m_weapons == 2) {
			if(isMoving == true) {
				//Set ShotgunMove Animation
				m_anim.SetInteger("Action", 6);
			} else {
				//Set ShotgunIdle Animation
				m_anim.SetInteger("Action", 5);
			}
		}
	}
}





 /*
 public float m_moveSpeed = 1.0f;
	public float m_jumpForce = 5.0f;
private bool m_onGround = false;
private float m_coins = 0f;
private float m_bonus = 0f;
public float PlayerScore = 0f;
private float m_diamonds= 0f;
private bool m_dead;
private bool m_stoppedJumping = true;
private float m_origJumpForce;
private Rigidbody m_rb;


	void Awake () {
		m_origJumpForce = m_jumpForce;
		m_rb = gameObject.GetComponent<Rigidbody>();
		m_coins = 0;
		m_diamonds = 0;
		m_anim = gameObject.GetComponent<Animator>();


	}
	
	// Update is called once per frame
	void Update () {
		//player is moving Left/Right
		if(Input.GetAxis("Horizontal") != 0) {
			Vector3 pos = gameObject.transform.position;
			pos.x += Input.GetAxis("Horizontal") * m_moveSpeed * Time.deltaTime;
			gameObject.transform.position = pos;
			m_anim.SetBool("isRunning", true);
		} else if (Input.GetAxis("Horizontal") == 0) {
				m_anim.SetBool("isRunning", false);
		}

		if(Input.GetButtonDown("Jump") && m_onGround) {
			m_anim.SetBool("isJumping", true);
			m_onGround = false;
			m_stoppedJumping = false;
			m_rb.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
		} else {
			
		}
		if(Input.GetButtonUp("Jump") && !m_stoppedJumping) {
			if(m_rb.velocity.y > 0){
				Vector3 velocity = m_rb.velocity;
				velocity.y = 0;
				m_rb.velocity = velocity;
			}
			m_anim.SetBool("isJumping", false);
			m_stoppedJumping = true;
			m_jumpForce = m_origJumpForce;
		}

		if (m_dead == true) {
			Debug.Log("IF");
		} else if (m_dead == false) {
			PlayerScore = (GameManager.Instance.TimeScore*10)+(m_coins * 5) + (m_diamonds * 25);
			//Debug.Log(PlayerScore);
		}
	}

	void OnTriggerEnter(Collider other) {

		if (other.gameObject.tag == "Coin") {
			//Destroy(other.gameObject);
			m_coins++;
		//} else if (other.gameObject.tag == "Diamond") {
		//	Destroy(other.gameObject);
		//	m_diamonds++;
		} else if (other.gameObject.tag == "Destroy"){
			m_dead = true;
			Debug.Log("Hit Player");
		}
	}
	void OnCollisionEnter(Collision other) {
		//tag foe collision
		if(other.gameObject.tag == "Ground" || other.gameObject.tag == "Keep") {
			m_onGround = true;
		} else if(other.gameObject.tag == "Player") {
			Debug.Log("collided with player");
		} else {
			//TODO: check for other collisions
		}
	}


}
  */