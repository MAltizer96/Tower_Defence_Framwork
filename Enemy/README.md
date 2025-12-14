-----
Enemy.CS

The Enemy class is a base MonoBehaviour used to define enemy behavior in the game. Attaching this script to a GameObject turns it into a fully functional enemy that can move along a path, take damage, and reward the player when defeated.

Features & Behavior

Configurable enemy stats via the Inspector:

Max health

Movement speed

Enemy name

Coin reward on death

UI health bar (Slider)

Path-based movement

Automatically follows a sequence of path points found under a GameObject named Path

Moves toward each point in order and is destroyed when it reaches the end

Health & damage handling

Tracks current health and updates the health bar in real time

Takes damage when colliding with objects tagged as Bullet

Calls the bullet’s hit logic before applying damage

Death logic

When health reaches zero, the enemy:

Rewards the player with coins

Destroys itself

Player integration

Automatically finds the Player object and increments the player’s coin count on enemy death

Intended Usage

This class is designed to be extended or reused for different enemy types by:

Adjusting Inspector values

Swapping sprites and animations

Overriding or extending movement, damage, or death behavior in derived classes

-----

The SquareEnemy and TriangleEnemy files inherit the base Enemy file, allowing for customization of each enemy type

-----
