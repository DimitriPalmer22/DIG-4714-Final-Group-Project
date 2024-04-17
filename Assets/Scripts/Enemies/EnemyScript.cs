using System;
using UnityEngine;

public abstract class EnemyScript : Actor
{
 
    #region Fields
    
    [SerializeField] protected float chaseSpeed;

    [SerializeField] protected int scoreValue;

    protected Rigidbody2D rb;

    #endregion Fields
    
    #region Properties
    
    public GameObject Player { get; protected set; }
    
    public override float Speed => chaseSpeed;
    
    #endregion Properties
    
    #region Unity Methods

    protected virtual void Awake()
    {
        Player = FindObjectOfType<PlayerController>().gameObject;
    }

    protected override void Start()
    {
        base.Start();
        
        // Get the rigid body component
        rb = GetComponent<Rigidbody2D>();
        
        // Subscribe to the OnDeath event
        OnDeath += DestroyOnDeath;
        OnDeath += AddToScoreOnDeath;
    }
    
    protected virtual void Update()
    {
    }

    protected virtual void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<Character>().ChangeHealth(-1);
    }

    #endregion Unity Methods
    
    #region Methods

    protected abstract void MoveTowardPlayer();

    private void DestroyOnDeath()
    {
        Destroy(gameObject);
    }

    private void AddToScoreOnDeath()
    {
        GlobalLevelScript.Instance.ChangeScore(scoreValue);
    }

    #endregion Methods

}