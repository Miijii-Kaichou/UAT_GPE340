﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour, ISpawnable
{
    public CharacterController charController;

    public List<Weapons> weapons = new List<Weapons>();

    public Weapons equippedWeapon = null;

    public bool isEquipped;

    public Transform weaponAttachedPoint;

    public Controller controller;

    //Movement
    public float movementSpeed;
    public float rollingSpeed = 1;

    public float vSpeed = 0f;
    public float gravity = 90f;

    public Animator animator;

    public virtual void Move(Vector3 worldDirectionToMove) { }

    public virtual void EquipWeapon(Weapons weapon)
    {
        equippedWeapon = Instantiate(weapon) as Weapons;
        equippedWeapon.GetComponent<Transform>().SetParent(weaponAttachedPoint);
        equippedWeapon.GetComponent<Transform>().position = weaponAttachedPoint.position;
        equippedWeapon.GetComponent<Transform>().rotation = weaponAttachedPoint.rotation;
    }

    public virtual void UnequipWeapon()
    {
        Destroy(equippedWeapon.gameObject);
        equippedWeapon = null;
        isEquipped = false;
    }

    public virtual void OnSpawn() {

        animator = GetComponent<Animator>();
    }

    protected virtual void OnAnimatorIK()
    {
        if (!equippedWeapon)
            return;
        if (equippedWeapon.RightHandIKTarget)
        {
            animator.SetIKPosition(AvatarIKGoal.RightHand, equippedWeapon.RightHandIKTarget.position);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
            animator.SetIKRotation(AvatarIKGoal.RightHand, equippedWeapon.RightHandIKTarget.rotation);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
        } else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0f);
        }

        if (equippedWeapon.LeftHandIKTarget)
        {
            animator.SetIKPosition(AvatarIKGoal.LeftHand, equippedWeapon.LeftHandIKTarget.position);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, equippedWeapon.LeftHandIKTarget.rotation);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0f);
        }
    }

    public virtual GameObject GetGameObject() { return null; }
}
