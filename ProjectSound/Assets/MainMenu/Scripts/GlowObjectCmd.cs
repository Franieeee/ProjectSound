﻿using UnityEngine;
using System.Collections.Generic;

public class GlowObjectCmd : MonoBehaviour
{
	public Color GlowColor;
	public float LerpFactor = 10;

    

	public Renderer[] Renderers
	{
		get;
		private set;
	}

	public Color CurrentColor
	{
		get { return _currentColor; }
	}

	private Color _currentColor;
	private Color _targetColor;

	void Start()
	{
		Renderers = GetComponentsInChildren<Renderer>();
		GlowController.RegisterObject(this);
	}

	private void OnMouseEnter()
	{
		_targetColor = GlowColor;
		enabled = true;
	}

    private void OnMouseDown()
    {
        SceneController sceneController = gameObject.GetComponent<SceneController>();
        if(sceneController != null)
        {
            sceneController.changeScene();
        }
    }

    private void OnMouseExit()
	{
		_targetColor = Color.black;
		enabled = true;
	}

	/// <summary>
	/// Update color, disable self if we reach our target color.
	/// </summary>
	private void Update()
	{
		_currentColor = Color.Lerp(_currentColor, _targetColor, Time.deltaTime * LerpFactor);

		if (_currentColor.Equals(_targetColor))
		{
			enabled = false;
		}
	}

    public void Glow()
    {
        _targetColor = GlowColor;
        enabled = true;
    }

    public void StopGlowing()
    {
        _targetColor = Color.black;
        enabled = true;
    }
}
