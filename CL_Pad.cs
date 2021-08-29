using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CL_Pad : MonoBehaviour {

	public float mouseSensitivity = 1.0f;
	protected SpriteRenderer _sp;
	protected LineRenderer _lr;
	protected Camera _mainCamera;	// カメラの座標を取得するため
	protected Vector2 _initPosition;	// カメラ座標を0としたときの最初にタップした場所
	protected Vector2 _mousePosition;	// initPositionを0としたときの今のマウスの場所

	/**
	 * Use this for initialization
	 */
	protected void Start () {
		_sp = GetComponent<SpriteRenderer> ();
		_mainCamera = Camera.main;
		createLineRenderer ();
		setVisiblePad (false);
	}
	
	/**
	 * Update is called once per frame
	 */
	protected void Update () {
		// 画面をタップしたときパッドが出る
		if (Input.GetMouseButtonDown (0)) {
			setVisiblePad (true);
		}
		// 画面をアンタップしたときパッドが消える
		if (Input.GetMouseButtonUp (0)) {
			setVisiblePad (false);
			setLineRenderer (false);
		}
		// パッドが出ている場合
		if (getVisiblePad ()) {
			_mousePosition = new Vector2 (_mousePosition.x + Input.GetAxis ("Mouse X") * 0.25f *  mouseSensitivity, _mousePosition.y + Input.GetAxis ("Mouse Y") * 0.25f * mouseSensitivity);
			CL_GameParameter.setHV (_mousePosition.x, _mousePosition.y);
			gameObject.transform.localPosition = new Vector3(_mainCamera.transform.position.x + _initPosition.x + CL_GameParameter.getH(), _mainCamera.transform.position.y + _initPosition.y + CL_GameParameter.getV(), 0);
			setLineRenderer (true);
		}
	}

	protected bool getVisiblePad(){
		if (_sp.maskInteraction == SpriteMaskInteraction.None) {
			return true;
		}
		return false;
	}

	/**
	 * パッド表示時と非表示時の処理 
	 */
	protected void setVisiblePad(bool flg){
		if (flg) {	// 出る
			_sp.maskInteraction = SpriteMaskInteraction.None;
			_initPosition = new Vector2 (getMousePosition().x - _mainCamera.transform.position.x, getMousePosition().y - _mainCamera.transform.position.y);
			_mousePosition = new Vector2 (0, 0);
			gameObject.transform.localPosition = new Vector3 (_initPosition.x, _initPosition.y, 0);
		} else {	// 消える
			_sp.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
			_initPosition = new Vector2 (0, 0);
			_mousePosition = new Vector2 (0, 0);
			CL_GameParameter.setHV (0, 0);
		}
	}

	protected Vector3 getMousePosition(){
		Vector3 ret = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		return new Vector3(ret.x, ret.y, 0);
	}

	/**
	 * 線を引くためのLineRendererを作成
	 */
	protected void createLineRenderer(){
		_lr = gameObject.AddComponent<LineRenderer> ();
		_lr.startWidth = 0.1f;
		_lr.endWidth = 0.1f;
	}

	/**
	 * 線を引く
	 */
	protected void setLineRenderer(bool isVisible){
		if(isVisible){	// 線を出す
			// 頂点の数を2にする
			_lr.positionCount = 2;	
			_lr.SetPosition (0, new Vector2(_initPosition.x + _mainCamera.transform.position.x, _initPosition.y + _mainCamera.transform.position.y));
			_lr.SetPosition (1, gameObject.transform.localPosition);

		} else {	// 線を消す
			// 頂点の数を1にする
			_lr.positionCount = 1;	
		}
	}


}
