using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public enum TouchDir {
    None,
    Left,
    Right,
    Top,
    Bottom
}

public class PlayerMove : MonoBehaviour {
    public AudioSource footLand;
    public float jumpHeight = 30f;
    public float speed = 10;
    public float horizontalMoveSpeed = 0.2f;
    public float jumpSpeed = 0.2f;
    public float minTouchLength = 50;
    public Transform prisoner;

    public bool isJumping = false;
	private float haveJumped = 0f;
	private bool isUp = true;
    public float slideTime = 1.4f;

    public int nowLaneIndex = 1;
    public int targetLaneIndex = 1;

    public bool isSliding = false;

    private float slideTimer=0;
    //private Animation animation;
    private float targetJumpHeight = 0;
    //private EnvGenerator env;
	private Forest forest;
    private Vector3 moveDownPos = Vector3.zero;
    private float moveHorizontal = 0;
	public ForestControl fc;
	[SerializeField]
	private int pointIndex = 0;
	private Animator anim;

	public TouchDir touchDir = TouchDir.None;

//    void Awake() {
//
//    }
	void Start(){
		//env = Camera.main.GetComponent<EnvGenerator>();
		forest = GameObject.FindGameObjectWithTag("Forest1").GetComponent<Forest>();
		// animation = transform.Find("Prisoner").GetComponent<Animation>();
		pointIndex = fc.GetCurrentForestWPNum ()-1;
		anim = this.GetComponent<Animator> ();
	}
	// Update is called once per frame
	void Update () {
		
        if (GameController.gameState == GameState.Playing) {

            Vector3 pos = transform.position;
			Vector3 nextWayPoint = fc.GetNextWayPoint(pointIndex);
			if (fc.curForest.tag == "Forest4" && this.transform.localPosition.z <-389.4) {
				//Debug.Log(this.transform.localPosition.z);
				SceneManager.LoadScene ("Test");
			}
			if(Vector3.Distance(this.transform.position, nextWayPoint) < 1.0f){
				//Debug.Log("pointIndex:" + pointIndex);
				if (pointIndex != 0) {
					pointIndex--;
					nextWayPoint = fc.GetNextWayPoint (pointIndex);
				} else {
					//change forest
					fc.ChangeForest();
					pointIndex = fc.GetCurrentForestWPNum ()-1;
				}

			}

            nextWayPoint = new Vector3(nextWayPoint.x + GameController.xOffsets[targetLaneIndex], nextWayPoint.y, nextWayPoint.z);
            Vector3 dir = nextWayPoint - transform.position;
            Vector3 moveDir = dir.normalized * speed * Time.deltaTime;
			//Debug.Log ("moveDir z:" + moveDir.z);
            this.transform.position += moveDir;
            transform.rotation = Quaternion.LookRotation(new Vector3(nextWayPoint.x, transform.position.y, nextWayPoint.z)-transform.position, Vector3.up);
            //transform.LookAt(nextWayPoint);

            if (targetLaneIndex != nowLaneIndex) {
                float move = moveHorizontal * horizontalMoveSpeed;
                moveHorizontal -= moveHorizontal * horizontalMoveSpeed;
                this.transform.position = new Vector3(transform.position.x + move, transform.position.y, transform.position.z);
                if (Mathf.Abs(moveHorizontal) < 0.5f) {
                    this.transform.position = new Vector3(transform.position.x + moveHorizontal, transform.position.y, transform.position.z);
                    nowLaneIndex = targetLaneIndex;
                }
            }				
			if(isJumping){
				anim.SetBool("RunToJump", true);
			}else{
				anim.SetBool("RunToJump", false);
			}
			if (isJumping) {
				Debug.Log ("jump move");
				float yMove = jumpSpeed * Time.deltaTime;
				if (isUp) {
					Debug.Log ("isUp true");
					this.prisoner.position = new Vector3 (prisoner.position.x, prisoner.position.y + yMove, prisoner.position.z);
					haveJumped += yMove;
					if (Mathf.Abs (jumpHeight - haveJumped) < 0.5) {
						this.prisoner.position = new Vector3 (prisoner.position.x, prisoner.position.y + jumpHeight - haveJumped, prisoner.position.z);
						isUp = false;
						haveJumped = jumpHeight;
					}
				} else {
					//isUp = false
					Debug.Log ("isUp false");
					if(prisoner.position.y - yMove > 0){
						this.prisoner.position = new Vector3 (prisoner.position.x, prisoner.position.y - yMove, prisoner.position.z);
					}
					yMove = 0.7f*jumpSpeed * Time.deltaTime;
					haveJumped -= yMove;
					if (haveJumped < 0.3 && haveJumped > 0) {
						this.prisoner.position = new Vector3 (prisoner.position.x, 0, prisoner.position.z);
						isJumping = false;
						haveJumped = 0;
						if (!footLand.isPlaying) {
							footLand.Play();
						}
					}
					//isUp = true;
				}
				touchDir = TouchDir.None;
			}

            if(isSliding){
                slideTimer += Time.deltaTime;
                if (slideTimer > slideTime) {
                    isSliding = false;
                    slideTimer = 0;
                }
            }

            MoveControll();
            
        }
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Obstacles") {
			GameController.gameState = GameState.End;
		}

	}

    void MoveControll() {
		if (GvrViewer.Instance.VRModeEnabled = true) {

		} else {
			touchDir = GetTouchDir();
		}

        switch (touchDir) {
            case TouchDir.None:
                break;
            case TouchDir.Right:
                if (targetLaneIndex < 2) {
                    targetLaneIndex++;
                    moveHorizontal = 3;
                }
                break;
            case TouchDir.Left:
                if (targetLaneIndex > 0) {
                    targetLaneIndex--;
                    moveHorizontal = -3;
                }
                break;
            case TouchDir.Top:
                if (isJumping == false) {
                    isJumping = true;
					isUp = true;
                   // targetJumpHeight = jumpHeight;
                }
                break;
            case TouchDir.Bottom:
                if (!isJumping) {
                    isSliding=true;
                }
                break;
        }

    }


    TouchDir GetTouchDir() {
        if (Input.GetMouseButtonDown(0)) {
            moveDownPos = Input.mousePosition;
            return TouchDir.None;
        }
        if (Input.GetMouseButtonUp(0)) {
            Vector3 moveOffset = Input.mousePosition - moveDownPos;
            if( Mathf.Abs( moveOffset.y ) > Mathf.Abs( moveOffset.x ) && moveOffset.y > minTouchLength){
				isUp = true;
				haveJumped = 0f;
                return TouchDir.Top;
            }

            if (Mathf.Abs(moveOffset.y) > Mathf.Abs(moveOffset.x) && moveOffset.y < -minTouchLength) {
                return TouchDir.Bottom;
            }

//            if (Mathf.Abs(moveOffset.y) < Mathf.Abs(moveOffset.x) && moveOffset.x > minTouchLength) {
//				return TouchDir.Left;
//            }
//
//            if (Mathf.Abs(moveOffset.y) < Mathf.Abs(moveOffset.x) && moveOffset.x < -minTouchLength) {
//				return TouchDir.Right;
//            }

        }
            return TouchDir.None;
    }

}
