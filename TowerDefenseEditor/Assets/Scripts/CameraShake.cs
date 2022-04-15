/*
 * Copyright (c) 2016 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	public float shake_decay_start = 0.002f;
	public float shake_intensity_start = 0.03f;

	private float shake_decay;
	private float shake_intensity;

	private Vector3 originPosition;
	private Quaternion originRotation;
	private bool shaking;
	private Transform transformAtOrigin;

	void OnEnable() {
		transformAtOrigin = transform;
	}

	//	void OnGUI () {
	//		if (GUI.Button (new Rect (20,40,80,20), "Shake")){
	//			Shake ();
	//		}
	//	}

	void Update () {
		if(!shaking)
			return;
		if (shake_intensity > 0f){
			transformAtOrigin.localPosition = originPosition + Random.insideUnitSphere * shake_intensity;
			transformAtOrigin.localRotation = new Quaternion(
				originRotation.x + Random.Range (-shake_intensity,shake_intensity) * .2f,
				originRotation.y + Random.Range (-shake_intensity,shake_intensity) * .2f,
				originRotation.z + Random.Range (-shake_intensity,shake_intensity) * .2f,
				originRotation.w + Random.Range (-shake_intensity,shake_intensity) * .2f);
			shake_intensity -= shake_decay;
		} else {
			shaking = false;
			transformAtOrigin.localPosition = originPosition;
			transformAtOrigin.localRotation = originRotation;
		}
	}

	public void Shake() {
		if(!shaking) {
			originPosition = transformAtOrigin.localPosition;
			originRotation = transformAtOrigin.localRotation;
		}
		shaking = true;
		shake_decay = shake_decay_start;
		shake_intensity = shake_intensity_start;
	}
	
}