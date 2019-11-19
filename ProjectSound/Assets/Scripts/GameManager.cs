﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** <summary>
    Singleton component managing and exposing global aspects of the game.
    </summary>
*/
public class GameManager : MonoBehaviour {
    #region Singleton
    public static GameManager instance;

    private static void InitSingleton(GameManager thisInstance) {
        if(instance != null && instance != thisInstance) {
            throw new System.Exception("Hay al menos dos instancias de " + thisInstance.GetType().Name);
        } else {
            instance = thisInstance;
        }
    }
    #endregion

    /** <summary>
        Reference to the player entity.
        </summary>
    */
    public Player player;

    /** <summary>
        List containing the Z-coordinate of all layers available in the stage.
        </summary>
    */
    public List<float> layers;

    /** <summary>
        Event that triggers whenever transitioning into or out of the pause state.
        </summary>
    */    
    public event System.Action<bool> onPauseChanged;

    /** <summary>
        Indicates whether the game is paused.
        </summary>
    */
    private bool paused;

    #region Unity
    void Awake() {
        GameManager.InitSingleton(this);
        this.player.onPlayerDead += this.OnPlayerDead;
    }

    void Update() {
        if(Input.GetButtonDown("Pause")) {
            this.SetPaused(!this.paused);
        }
    }
    #endregion

    #region Getters & Setters
    /** <summary>
        Returns whether the game is paused.
        </summary>
    */
    public bool IsPaused() {
        return paused;
    }

    /** <summary>
        Sets the paused state of the game, and triggers the event if changed.
        </summary>
    */
    public void SetPaused(bool paused) {
        if(paused != this.paused) {
            this.onPauseChanged.Invoke(paused);
        }
        this.paused = paused;
    }

    /** <summary>
        Returns the Z-coordinate of the indicated existing layer, or the nearest existing layer
        if the given layer does not exist.
        </summary>
    */
    public float GetLayer(int layer) {
        var listIndex = Mathf.Clamp(layer, 0, this.layers.Count - 1);
        return this.layers[listIndex];
    }
    #endregion

    private void OnPlayerDead() {
        //TODO Ir al menú de selección de niveles
    }

    #region Music
    public void PlayMusic(AudioClip audio) {
        //TODO Reproducir AudioSource de Camera.main
    }

    public void StopMusic() {
        //TODO Detener AudioSource de Camera.main
    }
    #endregion
}
