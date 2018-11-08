# TheFloorIsLava
The Floor is Lava is a competitive platformer based off the childhood game of the same name where furniture is your only safety and everything is dangerous. We are attempting to bring it to life in a digital way.

# Controls
WASD to move

Space bar to jump --> double jump engaged by pressing twice.

Scroll wheel to cycle abilities --> ability engaged by clicking either the left or right mouse button.

# Feature List
<b>4 Levels:</b>

Tutorial, Levels 1-3

# Abilities
<b>Double Jump</b> In game we currently have implemented a support ability called Double Jump, just wait till I tell you what it does you’d never guess. Double jump, makes players jump twice. This jump can be used mid air for player to extend a jump if they aren't going to make it or course correct mid air. Double Jump is on a cooldown of 3.5 seconds, which is of course subject to change with playtesting- as well as the force used for jumping could change. 

<b>Low Gravity</b> The low gravity support ability is one of two (this and Emergency Platform) bound to mouse 1. The scroll wheel will cycle between the two. Low gravity is an Area of Effect(AoE) ability that once throw will grow up to full size, hold that size, and then shrink away into nothingness. Within the AoE players will have, you guessed it, low gravity. They will  fall slower and jump higher within the AoE bubble. The bubble can be seen in the image below. 

The HUD element for low gravity is the green icon with upward sprouts. It isnt a finalized icon but i think it works for giving the idea of an upward draft or force. The circle will grey out (like the icon to its left) when not active. On top of this it also has a cooldown timer shown by a shadow overlay.

<b>Emergency Platform</b> The emergency platform ability is another thrown ability bound to mouse 1, and cycled between with scroll. Emergency Platform is a pillow that can be thrown onto the lava to create a new temporary platform. This platform lasts for a little while, but not forever, and once stepped on it will sink into the lava. So really it's good for a hop to save oneself, or bridge a tricky gap.

The HUD element for the emergency platform is a purple circle with a sot of arc icon and a platform at its bottom. While holding mouse 1 and aiming the throw the screen will go black and white as seen above. This was originally to support the idea that its an emergency but because it can be used outside of that it's also to be a reminder to players of which ability they are using at the moment. 

# Known Bugs / Issues
• Character model gets stuck halfway in the ground.

• All players stack on top of each other when starting after Lobby.

• Players do not animate on strafe.

• Timer starts when spawning into the lobby as opposed to when starting the level.

• Gravity ability stacks.
