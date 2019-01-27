using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {

    [SerializeField]
    List<AnimationCurve> FlickerCurves = new List<AnimationCurve>();

    AnimationCurve ChosenFlickerCurve;

    List<Light> _lights = new List<Light>();

    float StartingIntensity;

    bool IsFlickering = false;

    float TimeCounter = 0f;

	// Use this for initialization
	void Start () {
        foreach (var _light in this.gameObject.GetComponentsInChildren<Light>())
        {
            _lights.Add(_light);
        }

        if (this.GetComponent<Light>() != null) {
            _lights.Add(this.GetComponent<Light>());
        }


    }
	
	// Update is called once per frame
	void Update () {
        if (IsFlickering)
        {
            float newIntensity = ChosenFlickerCurve.Evaluate(TimeCounter);

            foreach (var _light in _lights)
            {
                _light.intensity = newIntensity;
            }

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
        foreach (var _light in _lights)
        {
            _light.intensity = 1f;
        }
        
    }

}
