/// Wes Rupert - wkr3
/// EECS 290   - Project 03
/// Purgatory  - Shooter.cs
/// Script to control general tower behavior.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A tower that shoots. Relatively cheap, 
/// </summary>
public class Archer : Tower {
#region Constants

    private const string TOWERNAME = "Archer";

#endregion

#region Archer Stats

    public static float[] ArcherShootSpeed;
    public static uint[] ArcherMaxHP;

    private float timeToShoot;

#endregion

#region Abstract Implementations

    /// <summary>
    /// The name of the tower.
    /// </summary>
    public override string Name {
        get {
            return TOWERNAME;
        }
    }

    /// <summary>
    /// The buying price of the tower.
    /// </summary>
    public override uint BuyPrice {
        get {
            // TODO: Implement.
            return 0;
        }
    }

    /// <summary>
    /// The sellng price of the tower.
    /// </summary>
    public override uint SellPrice {
        get {
            // TODO: Implement.
            return 0;
        }
    }

    /// <summary>
	/// Use this for initialization.
    /// </summary>
	public override void Start() {
        setTowerID();
        setArcherStats();

        HP = MaxHP;
        Targets = new HashSet<GameObject>();
        timeToShoot = ShootSpeed;
    }
	
    /// <summary>
	/// Update is called once per frame.
    /// </summary>
	public override void Update() {
        // Count down to being able to shoot again.
        if (timeToShoot > 0) {
            timeToShoot -= Time.deltaTime;
        }

        // Always look at target, if any.
        if (Target != null) {
            transform.LookAt(Target.transform);

            // Shoot the target, if applicable.
            if (timeToShoot < 0) {
                Shoot();
                timeToShoot = ShootSpeed;
            }
        }
    }

    /// <summary>
    /// Disables the tower.
    /// </summary>
    public override void Disable() {

    }

    public override void Shoot() {
        Projectile shot = Instantiate(ShotPrefab, transform.position, transform.rotation) as Projectile;
        shot.ParentTower = this;
        shot.Level = Level;
    }

    /// <summary>
    /// Enables the tower.
    /// </summary>
    public override void Enable() {

    }


#endregion

    private void setArcherStats() {
        MaxHP = ArcherMaxHP[Level - 1];
        ShootSpeed = ArcherShootSpeed[Level - 1];
    }
}