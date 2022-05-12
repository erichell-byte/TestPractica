using System;
using UnityEngine;

public class SplineWalker : MonoBehaviour {

	public BezierSpline spline;

	public float duration;

	public bool lookForward;

	public SplineWalkerMode mode;
	[SerializeField] private ParseConfig config;
	private float progress;
	private bool goingForward = true;
	private bool _keyIsPressed = false;

	

	private void Update ()
	{
		if (Input.GetKey(KeyCode.A))
		{
			_keyIsPressed = true;
		}
		if (config._isParse && duration == 0)
			FillConfig();
		if (_keyIsPressed && config._isParse && spline.isReadyToWalk)
			BeginWalking();
		if (Input.GetKey(KeyCode.D))
			Application.Quit();
	}

	void FillConfig()
	{
		if (config.isLoop)
			mode = SplineWalkerMode.Loop;
		else
			mode = SplineWalkerMode.Once;
		duration = config.walkingTime;
	}

	void BeginWalking()
	{
		if (goingForward) {
			progress += Time.deltaTime / duration;
			if (progress > 1f) {
				if (mode == SplineWalkerMode.Once) {
					progress = 1f;
				}
				else if (mode == SplineWalkerMode.Loop) {
					progress -= 1f;
				}
				else {
					progress = 2f - progress;
					goingForward = false;
				}
			}
		}
		else {
			progress -= Time.deltaTime / duration;
			if (progress < 0f) {
				progress = -progress;
				goingForward = true;
			}
		}

		Vector3 position = spline.GetPoint(progress);
		transform.localPosition = position;
		if (lookForward) {
			transform.LookAt(position + spline.GetDirection(progress));
		}
	}
}