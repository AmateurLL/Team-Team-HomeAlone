using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {

    [SerializeField]
    List<AnimationCurve> FlickerCurves = new List<AnimationCurve>();

    AnimationCurve ChosenFlickerCurve;

    Light _light;

    float StartingIntensity;

    bool IsFlickering = false;

    float TimeCounter = 0f;

	// Use this for initialization
	void Start () {
        _light = this.gameObject.GetComponent<Light>();
        StartingIntensity = _light.intensity;

    }
	
	// Update is called once per frame
	void Update () {
        if (IsFlickering)
        {
            float newIntensity = ChosenFlickerCurve.Evaluate(TimeCounter);
            _light.intensity = newIntensity;
            this.GetComponent<AudioSource>().volume = newIntensity;
            TimeCounter += Time.deltaTime;
        }
        else
            TimeCounter = 0f;

    }


    public void StartFlickering() {
        if (!IsFlickering)
        {
            IsFlickering = true;
            int chosenCurve = Random.Range(0, FlickerCurves.Count);
            ChosenFlickerCurve = FlickerCurves[chosenCurve];
        }
    }

    public void StopFlickering() {
        IsFlickering = false;
        _light.intensity = StartingIntensity;
    }

}
