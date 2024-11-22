using System;
using UnityEngine;
using UnityEngine.UI;

public enum StateAnimation { Idle, Walk }
public class ImageAnimation : MonoBehaviour
{
    private int _stateIndex => Convert.ToInt32(StateAnimation);
    private int _spriteIndex;
    private float _animationTimer;
    public Image Image;
    public State[] state;
    public float AnimationSpeed;
    public StateAnimation StateAnimation;
    private float _width, _height;

    private void FixedUpdate()
    {
        _animationTimer += Time.deltaTime;
        if (_animationTimer >= AnimationSpeed / state[_stateIndex].Sprites.Length)
        {
            _animationTimer = 0;
            _spriteIndex = (_spriteIndex + 1) % state[_stateIndex].Sprites.Length;
            Image.sprite = state[_stateIndex].Sprites[_spriteIndex];
        }
    }
}
[Serializable]
public class State
{
    public Sprite[] Sprites;
}