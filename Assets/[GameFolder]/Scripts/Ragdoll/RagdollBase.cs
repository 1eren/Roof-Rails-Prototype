using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public abstract class RagdollBase : MonoBehaviour
{
    #region Ragdoll Components
    private Collider[] ragdollColliders;
    public Collider[] RagdollColliders { get { return ragdollColliders == null ? ragdollColliders = ragdollRoot.GetComponentsInChildren<Collider>() : ragdollColliders; } }

    private Rigidbody[] ragdollRigidbodies;
    public Rigidbody[] RagdollRigidbodies { get { return ragdollRigidbodies == null ? ragdollRigidbodies = ragdollRoot.GetComponentsInChildren<Rigidbody>() : ragdollRigidbodies; } }
    #endregion

    Animator animator;
    Animator Animator { get { return animator == null ? animator = ragdollRoot.GetComponentInChildren<Animator>() : animator; } }

    private Rigidbody mainRigidbody;
    public Rigidbody MainRigidbody { get { return mainRigidbody == null ? mainRigidbody = GetComponentInParent<Rigidbody>() : mainRigidbody; } }
    private Collider mainCollider;
    public Collider MainCollider { get { return mainCollider == null ? mainCollider = GetComponentInParent<Collider>() : mainCollider; } }
    public bool IsRagdollActive { get; set; }
    public Transform ragdollRoot;
    protected void ActivateRagdoll()
    {
        //gameObject.transform.parent = null;
        Animator.enabled = false;
        SetRigidbodies(false);
        SetColliders(true);
        IsRagdollActive = true;
    }

    protected void DisableRagdoll()
    {
        Animator.enabled = true;
        SetRigidbodies(true, false);
        SetColliders(false, false);
        IsRagdollActive = false;
    }

    protected void AddForceToRagdollObject(Vector3 direction, float force)
    {
        foreach (Rigidbody rigidbody in RagdollRigidbodies)
        {
            rigidbody.AddForce(direction * force, ForceMode.Impulse);
        }
    }

    private void SetRigidbodies(bool state, bool setMain = true)
    {
        foreach (Rigidbody rigidbody in RagdollRigidbodies)
        {
            rigidbody.isKinematic = state;
        }

        if (MainRigidbody != null && setMain) 
        {            
            if (!state)
            {
                MainRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            }
            else
            {
                MainRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            }

            MainRigidbody.isKinematic = !state;
        }
    }

    private void SetColliders(bool state, bool setMain = true)
    {
        foreach (var item in RagdollColliders)
        {
            item.enabled = state;
            //item.isTrigger = !state;
        }
        if (MainCollider != null && setMain) 
            MainCollider.enabled = !state;

    }
}