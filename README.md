----

Tower Defense Framework (Unity)

This project is a modular Tower Defense framework built in Unity, designed to be easily extendable and beginner-friendly while still supporting scalable gameplay systems such as waves, turrets, upgrades, and UI feedback.

The framework focuses on clean separation of responsibilities, allowing each system (waves, enemies, turrets, bullets, UI) to be developed and extended independently.

----

Core Systems Overview
1. Wave System

Responsible for:

Managing wave progression

Spawning enemies in defined compositions

Scaling difficulty over time

Key Components:

WaveManager – Controls wave flow, spawning, and UI updates

Wave – Defines enemy composition per wave

EnemyCount – Pairs enemy prefabs with spawn counts

Flow:
Player starts a wave → enemies spawn at intervals → wave completes → next wave becomes available.

2. Enemy System

Responsible for:

Enemy movement and health

Taking damage from bullets

Rewarding the player on death

Key Components:

Enemy – Base class for all enemy types

Enemies are designed to be data-driven, allowing new enemy types to be created by adjusting stats or extending the base class.

3. Turret System

Responsible for:

Targeting enemies in range

Firing bullets

Handling upgrades and sell values

Key Components:

Turrent – Core turret behavior (range detection, targeting, shooting)

ITurrent – Interface for standardizing turret behavior

Flow:
Enemy enters range → turret selects target → turret rotates → bullet is fired → damage is applied.

4. Bullet System

Responsible for:

Moving toward a target

Applying damage on hit

Playing hit effects and cleaning up objects

Key Components:

Bullet – Handles projectile movement, impact logic, and effects

Bullets are target-driven, allowing for homing behavior and flexible damage handling.

5. Upgrade System

Responsible for:

Upgrading turret stats

Managing player currency costs

Updating upgrade UI

Key Components:

UpgradeManager – Handles turret upgrades and UI updates

Player – Tracks player currency

Upgrades are separated from turret logic to keep turrets focused on combat behavior.

6. UI System

Responsible for:

Displaying wave numbers

Toggling panels

Showing player feedback (errors, warnings)

Key Components:

UIManager – Centralized UI control

TextMeshPro UI elements

The UI system is lightweight and reusable across all gameplay systems.

----
