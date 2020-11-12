using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class EnemyLockOn : MonoBehaviour
{
    public float range;
    public Transform emptyTarget;

    public CinemachineTargetGroup group;
    public int enemyCount;
    public List<Transform> enemiesToLock;
    public Transform closestEnemy;
    public Transform selectedEnemy;
    public Transform priorityEnemy;
    public bool foundPriorityEnemy;

    public float xScaleIncrement;
    public float yScaleIncrement;
    public float zScaleIncrement;
    public float xScaleMax;
    public float zScaleMax;
    public GameObject targetingCone;
    public GameObject targetingConePivot;
    public Transform coneHolder;
    private Vector3 selectorDirection;
    private bool parentChangeInitializationPerformed;


}
