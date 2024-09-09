using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ChoppableTree : MonoBehaviour
{

    public bool PlayerInRange;
    public bool canBeChopped;
    public float treeMaxHealth;
    public float treeHealth;

    public Animator animator;

    public float caloriesSpent = 20f;

    private void Start()
    {
        treeHealth = treeMaxHealth;
        animator = transform.parent.transform.parent.GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = false;
        }
    }
    #region [calculating animation hit and damage]
    public void GetHit()
    {
        animator.SetTrigger("Shake");
        treeHealth -= 1;

        PlayerStatus.Instance.CurrentFood -= caloriesSpent;

        if (treeHealth <= 0)
        {
            TreeIsDead();
        }

    }

    void TreeIsDead()
    {
        
        Vector3 treePosition = transform.position;

        Destroy(transform.parent.transform.parent.gameObject);
        canBeChopped = false;
        SelectionManager.Instance.selectedTree = null;
        SelectionManager.Instance.chopHolder.gameObject.SetActive(false);

        GameObject brokenTree = Instantiate(Resources.Load<GameObject>("ChoppedTree"),
            new Vector3(treePosition.x+1, treePosition.y+1, treePosition.z+1), Quaternion.Euler(0, 0, 0)
            );

    }
    #endregion

    private void Update()
    {
        if (canBeChopped)
        {
            GlobalState.Instance.resourceHealth = treeHealth;
            GlobalState.Instance.resourceMaxHealth = treeMaxHealth;
        }
    }

}
