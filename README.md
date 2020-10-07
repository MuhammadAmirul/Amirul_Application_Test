# Mixed Realms Programmer Application Test

## Tools
- Unity (2020.1.7f1)
- Any tweening asset of your choice (DOTween is included)

## Deliverables
- A fork of this project with the following features implemented :

## Required features
- Setup a menu scene with a UI canvas with 3 buttons that load to 3 different scenes.
- Ensure a Vertical Layout Group is used.
- Press F1 to return to the menu scene.
- Press F2 to reload the current scene.
- The buttons are titled "Red Scene", "Blue Scene", and "Green Scene". The contents of each scene are listed respectively below.

### Red Scene
*To demonstrate your foundations by displaying and modifying a simple object.*

- Contains a cube with a texture of your choice that slowly spins on its Y axis. 
- The cube's scale pingpongs between 1 and 2 over 2 seconds (1 second to go from 1 to 2, and another 1 second to go back). Use an InOutExpo easing in the tweening asset of your choice.
- The cube material's X offset changes over time (value is at your discretion).

### Blue Scene
*To test your understanding of the game physics and user interaction.*

- The idea is to have a cube jump around with physics.
- Contains a cube as a floor, 10 units wide and long, and 1 unit high.
- Contains a cube with a crate texture in the middle of the area. It has a rigidbody and collider, with a weight of 2. Scale is (1,1,1).
- The camera must follow and look at this crate.
- Create a loop that spawns 20 other crates in the scene in random positions. They have a weight of 1. Scale is (1,1,1).
- Ensure all crates have a physics material that makes them a little bouncy (value is at your discretion).
- Press Space to apply a random force and torque to the cube. The intended outcome is that the cube jumps and collides around with other, bouncing about.

### Green Scene
*With the popularity of Fall Guys, I want you to capture the experience of watching characters competing in a race.*

- A random number of characters, with a minimum of 6, has to participate in the race.
- Press a button labelled "Start Race" on a UI canvas to begin.
- Each character has their own lane.
- Each character runs from point A to point B, then back to point A (more waypoints are fine, but must return to point A)
- Every character must complete their lap eventually.
- The characters should look different from each other, the bigger the differences the better.
- In addition to visual differences, consider other factors that allow the characters to stand apart from each other (e.g random avatar names).
- Once the race is complete, the UI canvas with "Start Race" appears again, allowing the race to be restarted. Each race should look different and exciting each time.
- Consider using free Asset store models and animations to accomplish this.

## Stretch goals
*If you breeze through the above, consider the following as well.*

- Use TextMeshPro instead of the default Text component where relevant.
- Scenes load and unload asynchronously, with a loading scene to keep the player in (fake a 1 second load time).
- Garbage collect in load scene.
