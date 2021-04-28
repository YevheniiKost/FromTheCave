using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
	[SerializeField] private float _amount;
	[SerializeField] private float _speed;

	private Light2D _localLight;
	private float _intensity;
	private float _offset;

	private void Awake()
	{
		_localLight = GetComponentInChildren<Light2D>();
	}

	void Start()
	{
		_intensity = _localLight.intensity;
		_offset = Random.Range(0, 10000);
	}

	void Update()
	{
		float amount = Mathf.PerlinNoise(Time.time * _speed + _offset, Time.time * _speed + _offset) * _amount;
		_localLight.intensity = _intensity + amount;
	}
}
