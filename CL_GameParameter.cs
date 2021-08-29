using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CL_GameParameter {
	private static float _maxPadRange = 1.5f;
	private static float _horizontal;
	private static float _vertical;

	public static void setHV(float h, float v){
		if (Mathf.Pow (h, 2) + Mathf.Pow (v, 2) > Mathf.Pow (_maxPadRange, 2)) {
			float radian = Mathf.Atan2 (v, h);
			_horizontal = _maxPadRange * Mathf.Cos (radian);
			_vertical = _maxPadRange * Mathf.Sin (radian);
		} else {
			_horizontal = h;
			_vertical = v;
		}
	}

	public static float getH(){
		return _horizontal;
	}

	public static float getV(){
		return _vertical;
	}
}
