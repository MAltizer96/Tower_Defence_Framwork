----

The Turrent script defines the behavior and attributes of a turret in a Unity tower defense game. It manages turret targeting, firing, range detection, upgrades, and interaction with enemies.

Features

Targeting and Shooting
Automatically selects the nearest enemy in range and rotates the turret to face it. Fires bullets at the target based on the turret's fire rate.

Enemy Detection
Uses a BoxCollider2D trigger to detect enemies entering and exiting its range.

Upgrades
Supports speed and damage upgrades, with corresponding costs and tracking of upgrade levels.

UI and Visual Feedback
Displays a range indicator using a child GameObject (RangeGO).

Customizable Attributes
Configure turret damage, range, fire rate, bullet speed, buy/sell values, and prefab references via the Inspector.

Dependencies

Unity (tested with Unity 2021+)

Bullet Script
Must include a Fire(Transform target, int damage, float speed) method for the bullets.

Enemy GameObjects
Must be tagged as Enemy for range detection.

ITurrent Interface
Implements standard turret methods and properties.

Usage

Attach the Turrent script to a turret GameObject.

Assign the following in the Inspector:

Bullet Prefab → Prefab used for shooting.

Ghost Prefab → Prefab for visual placement feedback (optional).

Turret Prefab → Original turret prefab reference.

Gun Base → Transform representing the rotating part of the turret.

Range → Float representing detection range.

Damage, Fire Rate, Buy Value, Sell Value → Numeric attributes.

Ensure a child GameObject named Range exists for the range visualization.

Key Methods

Update()
Handles selecting a target, rotating the turret, and firing at enemies if ready.

SelectNewTarget()
Sets the first enemy in range as the current target.

LookAt()
Rotates the turret gun base to face the current target.

Shoot()
Instantiates a bullet and fires it at the target.

OnTriggerEnter2D(Collider2D other)
Adds enemies to the EnemiesInRange list when they enter the turret's detection area.

OnTriggerExit2D(Collider2D other)
Removes enemies from the EnemiesInRange list when they leave the detection area.

Properties

Target → Current enemy target.

Damage → Damage per shot.

Range → Detection radius.

FireRate → Shots per second.

EnemiesInRange → List of enemies currently in range.

SpeedUpgrades / DamageUpgrades → Tracks applied upgrades.

SpeedUpgradeCost / DamageUpgradeCost → Cost to upgrade turret.


----


Bullet Script

The Bullet script defines the behavior of a projectile fired by turrets. It handles movement towards a target, applying damage, and displaying hit effects upon impact.

Features

Targeted Movement
Moves the bullet towards a specified target at a defined speed.

Hit Detection & Damage
Calls HitTargetLogic() when hitting the target to apply damage (can be extended to interact with an Enemy script).

Particle Effects
Plays a particle effect on impact to provide visual feedback.

Automatic Destruction
Destroys the bullet after hitting a target or if the target no longer exists.

Dependencies

Unity (tested with Unity 2021+)

Requires a SpriteRenderer component for the bullet visuals.

Requires a ParticleSystem component on the same GameObject for the hit effect.

Usage

Attach the Bullet script to a bullet prefab.

Ensure the prefab has:

SpriteRenderer → For bullet visuals.

Collider2D → For detecting collisions.

ParticleSystem → For hit effect.

Call the Fire() method to launch the bullet:


----

The Rocket_Turrent and the Basic_Turrent inherit the Turrent class, to allow for customization of each new turrent created.
