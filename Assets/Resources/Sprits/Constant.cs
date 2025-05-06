using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant : MonoBehaviour
{
    // Input keys
    public const string KEY_MOVE_LEFT = "a";
    public const string KEY_MOVE_RIGHT = "d";
    public const string KEY_JUMP = "space";
    //public const string KEY_RUN = "left shift";
    public const string KEY_ATTACK = "r";
    public const string KEY_DASH = "f";

    // Animator states
    public const string ANIM_IDLE = "Idle";
    public const string ANIM_RUN = "Run";
    public const string ANIM_JUMP = "Jump";
    public const string ANIM_DOUBLE_JUMP = "Double_Jump";
    public const string ANIM_ATTACK = "Hit";
    public const string ANIM_DASH = "Dash";
    public const string ANIM_FALL = "Fall";
    public const string ANIM_WALL = "Wall";

    // Tags
    public const string TAG_PLAYER = "Player";
    public const string TAG_ENEMY = "Enemy";
    public const string TAG_GROUND = "Ground";

    // Layers
    public const string LAYER_GROUND = "Ground";
    public const string LAYER_ENEMY = "Enemy";

    // Scene names
    public const string SCENE_MAIN_MENU = "MainMenu";
    public const string SCENE_GAMEPLAY = "Gameplay";
    public const string SCENE_GAMEOVER = "GameOver";
}
