using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public abstract class RagdollBase : MonoBehaviour
{
    #region Ragdoll Components
    protected Collider[] ragdollColliders;

    protected Rigidbody[] ragdollRigidbodies;
    #endregion

    Animator animator;
    Animator Animator { get { return animator == null ? animator = ragdollRoot.GetComponentInParent<Animator>() : animator; } }

    protected Rigidbody mainRigidbody;
    protected Collider mainCollider;

    public bool IsRagdollActive { get; set; }
    public Transform ragdollRoot;
    protected void ActivateRagdoll()
    {
        Animator.enabled = false;
        SetRigidbodies(false);
        SetColliders(true);
        IsRagdollActive = true;
    }

    protected void DisableRagdoll()
    {
        InitializeRagdoll();
        Animator.enabled = true;
        SetRigidbodies(true, false);
        SetColliders(false, false);
        IsRagdollActive = false;
    }
    
    protected void AddForceToRagdollObject(Vector3 direction, float force)
    {
        foreach (Rigidbody rigidbody in ragdollRigidbodies)
        {
            rigidbody.AddForce(direction * force, ForceMode.Impulse);
        }
    }
    private void InitializeRagdoll()
    {
        mainRigidbody = GetComponent<Rigidbody>();
        mainCollider = GetComponentInParent<Collider>();
        ragdollRigidbodies = ragdollRoot.GetComponentsInChildren<Rigidbody>();
        ragdollColliders = ragdollRoot.GetComponentsInChildren<Collider>();
    }
    private void SetRigidbodies(bool state, bool setMain = true)
    {
        foreach (Rigidbody rigidbody in ragdollRigidbodies)
        {
            rigidbody.isKinematic = state;
        }
       
    }

    private void SetColliders(bool state, bool setMain = true)
    {
        foreach (var item in ragdollColliders)
        {
            item.enabled = state;
        }
    }
}