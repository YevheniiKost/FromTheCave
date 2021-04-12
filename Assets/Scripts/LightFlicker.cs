using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
	public float amount;    //The amount of light flicker
	public float speed;     //The speed of the flicker

	Light2D localLight;       //Reference to the light component
	float intensity;        //The collective intensity of the light component
	float offset;           //An offset so all flickers are different


	void Start()
	{
		//Get a reference to the Light component on the child game object
		localLight = GetComponentInChildren<Light2D>();

		//Record the intensity and pick a random seed number to start
		intensity = localLight.intensity;
		offset = Random.Range(0, 10000);
	}

	void Update()
	{
		//Using perlin noise, determine a random intensity amount
		float amt = Mathf.PerlinNoise(Time.time * speed + offset, Time.time * speed + offset) * amount;
		localLight.intensity = intensity + amt;
	}
}
