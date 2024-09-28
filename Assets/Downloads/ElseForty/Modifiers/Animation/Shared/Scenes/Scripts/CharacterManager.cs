using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ElseForty.splineplus.animation;
using ElseForty.splineplus.animation.api;
using ElseForty.splineplus.animation.model;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public float animationFadeTime = 0.1f;
    public BaseFollowerClass baseFollowerClass;
    public List<CharacterModel> Characters = new List<CharacterModel>();
    public static CharacterManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        for (int i = 0; i < Characters.Count; i++)
        {
            var character = Characters[i];
            if (character.go != null)
            {
                character.animator = character.go.GetComponent<Animator>();
                var baseFollower = baseFollowerClass.GetByGameObject(character.go);

                if (baseFollower == null) Debug.Log("Can't find follower");
                else character.baseFollower = baseFollower;

                ChangeAnimation(character, "IDLE");
            }
            else
            {
                Debug.LogError("Unassigned character game object!");
            }
        }
    }

    public void ChangeAnimation(CharacterModel character, string animation)
    {
        if (character.currentAnimation != animation)
        {
            character.currentAnimation = animation;
            character.animator.CrossFade(animation, animationFadeTime);
        }
    }

    public void Stop(GameObject gameObject)
    {
        var character = FindCharacter(gameObject);
        StartCoroutine(ResumeAfterDelay(character));
    }

    IEnumerator ResumeAfterDelay(CharacterModel character)
    {
        character.baseFollower.Stop();
        yield return new WaitForSeconds(character.stopTime);
        character.baseFollower.Animate();
    }

    void Update()
    {
        for (int i = 0; i < Characters.Count; i++)
        {
            var character = Characters[i];
            var baseFollower = character.baseFollower;
            var currentSpeed = baseFollower.GetCurrentSpeed();
            var currentAnimation = character.currentAnimation;
            var minRunSpeed = character.minRunSpeed;

            if (currentSpeed == 0)
            {
                ChangeAnimation(character, "IDLE");
            }
            else if (currentSpeed >= minRunSpeed)
            {
                if (currentAnimation == "IDLE") ChangeAnimation(character, "RUN");
                else if (currentAnimation == "WALK") ChangeAnimation(character, "RUN");
                else if (currentAnimation == "WALK_LEFT") ChangeAnimation(character, "RUN_LEFT");
                else if (currentAnimation == "WALK_RIGHT") ChangeAnimation(character, "RUN_RIGHT");
            }

            else if (currentSpeed < minRunSpeed)
            {
                if (currentAnimation == "IDLE") ChangeAnimation(character, "WALK");
                else if (currentAnimation == "RUN") ChangeAnimation(character, "WALK");
                else if (currentAnimation == "RUN_LEFT") ChangeAnimation(character, "WALK_LEFT");
                else if (currentAnimation == "RUN_RIGHT") ChangeAnimation(character, "WALK_RIGHT");
            }
        }
    }

    public void Walk(GameObject gameObject)
    {
        var character = FindCharacter(gameObject);
        character.baseFollower.SetSpeed(character.targetWalkSpeed);
        ChangeAnimation(character, "WALK");
    }

    public void WalkLeft(GameObject gameObject)
    {
        var character = FindCharacter(gameObject);
        ChangeAnimation(character, "WALK_LEFT");
    }

    public void WalkRight(GameObject gameObject)
    {
        var character = FindCharacter(gameObject);
        ChangeAnimation(character, "WALK_RIGHT");
    }

    public void Idle(GameObject gameObject)
    {
        var character = FindCharacter(gameObject);
        ChangeAnimation(character, "IDLE");
    }
    public void Run(GameObject gameObject)
    {
        var character = FindCharacter(gameObject);
        character.baseFollower.SetSpeed(character.targetRunSpeed);
        ChangeAnimation(character, "RUN");
    }

    public void RunLeft(GameObject gameObject)
    {
        var character = FindCharacter(gameObject);
        ChangeAnimation(character, "RUN_LEFT");
    }

    public void RunRight(GameObject gameObject)
    {
        var character = FindCharacter(gameObject);
        ChangeAnimation(character, "RUN_RIGHT");
    }

    public void Jump(GameObject gameObject)
    {
        var character = FindCharacter(gameObject);
        ChangeAnimation(character, "JUMP");
    }

    private CharacterModel FindCharacter(GameObject go)
    {
        return Characters.Find(x => x.go == go);
    }

    public void SetTargetSpeed(GameObject gameObject, float targetSpeed)
    {
        var character = FindCharacter(gameObject);
        character.baseFollower.SetSpeed(targetSpeed);
    }
}

[Serializable]
public class CharacterModel
{
    public GameObject go;

    // minimum speed to consider the charater running, if it is above then character is running , if bellow then character is walking
    public float minRunSpeed = 3;
    public float targetRunSpeed = 7;
    public float targetWalkSpeed = 2.5f;

    // the character stop time when calling the stop function
    public float stopTime = 3;

    [HideInInspector] public string currentAnimation;
    [HideInInspector] public Animator animator;
    [HideInInspector] public BaseFollowerModel baseFollower;

    public CharacterModel()
    {
        minRunSpeed = 3;
        targetRunSpeed = 7;
        targetWalkSpeed = 2.5f;
        stopTime = 3;
    }
}