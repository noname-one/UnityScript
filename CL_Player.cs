using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CL_Player : MonoBehaviour {

	public float _speed;
	protected Animator _anim;
	protected Rigidbody2D _rb;

	// Use this for initialization
	protected void Start () {
		_anim = GetComponent<Animator> ();
		createRigidBody ();
	}

	// Update is called once per frame
	protected void FixedUpdate () {
		moveCtrl ();
	}

	private void moveCtrl(){
		// 左右の移動
		if (CL_GameParameter.getH() > 0) {	// 右
			transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			_rb.velocity = new Vector2 (calcMoveSpeed(CL_GameParameter.getH()), _rb.velocity.y);
			_anim.SetInteger ("Status", 1);
		} else if(CL_GameParameter.getH() < 0){	// 左
			transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			_rb.velocity = new Vector2 (calcMoveSpeed(CL_GameParameter.getH()), _rb.velocity.y);
			_anim.SetInteger ("Status", 1);
		} else {	// 移動無し
			_rb.velocity = new Vector2 (0, _rb.velocity.y);
		}
		// 上下の移動
		if (CL_GameParameter.getH() != 0) {	// 移動アリ
			_rb.velocity = new Vector2 (_rb.velocity.x, calcMoveSpeed(CL_GameParameter.getV()));
			_anim.SetInteger ("Status", 1);
		} else {	// 移動無し
			_rb.velocity = new Vector2 (_rb.velocity.x, 0);
		}
		// 移動しない
		if (CL_GameParameter.getH () == 0 && CL_GameParameter.getV () == 0) {
			_anim.SetInteger ("Status", 0);
		}

	}

	private float calcMoveSpeed(float spd){
		return spd * _speed * Time.deltaTime * 150;
	}

	private void createRigidBody(){
		_rb = gameObject.AddComponent<Rigidbody2D> ();
		_rb.gravityScale = 0.0f;
		_rb.freezeRotation = true;
	}
}
