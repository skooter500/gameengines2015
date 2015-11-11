# DT228/DT211 Game Engines 1

## Resources
* [Slack](http://gameengines2015.slack.com)
* [Webcourses](http://dit.ie/webcourses)
* [Download Unity](http://processing.org)
* [Download Visual Studio 2015](http://processing.org/reference/)
* [Download Visual Studio Tools for Unity](http://processing.org/reference/)
* [Games Fleadh](http://www.gamesfleadh.ie/)
* [The Imagine Cup](https://www.imaginecup.com/)
* [The git manual - read the first three chapters](http://git-scm.com/documentation)
* [A video tutorial all about git/github](https://www.youtube.com/watch?v=p_PGUltnB6w)
* [Lecture notes](https://onedrive.live.com/redir?resid=AB603D769EDBF24E!263984&authkey=!AE-BvjCbphg3dOs&ithint=folder%2clnk)

## Contact the lecturer
* Email: bryan.duggan@dit.ie
* Twitter: [@skooter500](http://twitter.com/skooter500)
* Slack: https://gameengines2015.slack.com

# Assignment
- [Assignment](assignment.md)

# Week 1

##Lecture

- [Processing with GameComponents example](processing/YASCComponents)
- [Starter Unity3D project](unity/Vectors1)
- [The Processing sketch  we wrote in class to demonstrate game components](processing/YASCComponents)
- [A whole series of lectures I made about OOP in C# a few years ago](https://www.youtube.com/playlist?list=PL1n0B6z4e_E5aB2FqwrNRrhLyO0aqW_Bo)
- Objects & classes in C#

	[![YouTube](http://img.youtube.com/vi/9hqcvnTuESo/0.jpg)](https://www.youtube.com/watch?v=9hqcvnTuESo)

- Objects classes & references in C#

	[![YouTube](http://img.youtube.com/vi/tAodHmlY7oI/0.jpg)](https://www.youtube.com/watch?v=tAodHmlY7oI)

- C# Inheritance, static & dynamic binding

	[![YouTube](http://img.youtube.com/vi/IMa3hXM1Rx8/0.jpg)](https://www.youtube.com/watch?v=IMa3hXM1Rx8)

- Procedurally generated minnowforms video:

	[![YouTube](http://img.youtube.com/vi/5G-PKD5GnlE/0.jpg)](https://www.youtube.com/watch?v=5G-PKD5GnlE)

- NILL One of my game jam games. It uses lots of nice come sci stuff like Perlin noise, procedural generation and binary search

	[![YouTube](http://img.youtube.com/vi/RkReFavQbQo/0.jpg)](https://www.youtube.com/watch?v=RkReFavQbQo)

	[NILL Sourcecode](http://github.com/skooter500/NILL)

- Assignments from previous years:

	[![YouTube](http://img.youtube.com/vi/ii049d7UFrg/0.jpg)](https://www.youtube.com/watch?v=ii049d7UFrg)

	[![YouTube](http://img.youtube.com/vi/5BPxM--x-7M/0.jpg)](https://www.youtube.com/watch?v=5BPxM--x-7M)

	[![YouTube](http://img.youtube.com/vi/ii049d7UFrg/0.jpg)](https://www.youtube.com/watch?v=ii049d7UFrg)

## Lab
- No lab this week

## Week 2
## Lab
### Learning outcomes
- Clone a git repository
- Become familiar with the Unity3D editor
- Create game objects from 3D models
- Attach a new game component to a game objects
- Implement some simple keyboard handling code
- Know where the Unity3D tutorial videos are

The aim of this lab is to set up a new scene in Unity3D that looks like the scene below and to implement some simple keyboard handling behaviour to move a 3D model around the scene:

![Scene1](images/scene1.png)

Firstly clone the repository for the course. To do this, fire up the bash shell and type:

```bash
git clone https://github.com/skooter500/gameengines2015
```

-Now start Unity3D and open the project folder Vectors1 from the unity folder in the repo you just cloned. All the assets you need to complete the project are included.
-Create a new, empty scene
-Add a camera and attach the FPSController component. Its a C# component that is one of the project assets.
-Add a plane. 
-Scale the plane to 50 1 50, so that it fills a flat area of 50 x 50
-Texture the plane with the ground texture. To do this you can just drag the texture from the project assets onto the plane.
-Now create the two models. To do this, you have to just drag the models from the project assets onto the scene. The model on the left is cobra mk3 and the model on the right is called a ferdelance.
-Attach a new game component to the cobra mk3 and have a go at implementing a behaviour that causes the ship to move forward, backward, left and right in response to the arrow keys. This is how you detect key presses in Unity3D:

```C#
if (Input.GetKey(KeyCode.W))
{
	// Do some stuff
}
```

So I havent explained in class how to do most of this stuff, so use code completion in Visual Studio and just try some stuff and see if you can figure it out. At some stage you should watch the [Unity3D tutorial videos](https://unity3d.com/learn/tutorials/) to get started.

I suggest you watch at least these videos:
- [The Unity3D interface & essentials](https://unity3d.com/learn/tutorials/topics/interface-essentials)
- [Scripting](https://unity3d.com/learn/tutorials/topics/scripting)

Additional stuff from the lab:

- [The ship moving around the screen and firing bullets Processing example](processing/ShipAndBullets)

## Lecture
- Check out the ShipMovement code in the Vectors1 project

# Week 3

## Lab
- No lab this week

## Lecture
- [Lecture notes 2](https://onedrive.live.com/redir?resid=AB603D769EDBF24E!263984&authkey=!AE-BvjCbphg3dOs&ithint=folder%2clnk)

# Week 4
## Lab
- Procedurally generated content in Unity

## Lecture
- Trigonometry
- [Lecture notes 3](https://onedrive.live.com/redir?resid=AB603D769EDBF24E!263984&authkey=!AE-BvjCbphg3dOs&ithint=folder%2clnk)

# Week 5
## Lab

- [Take a minute to fill out this survey](https://www.surveymonkey.com/r/Y5YRJTV)

- For this lab, we will be making a patrol/stealth AI for the ferdelance ship in the scene. You should start by checking out the master branch of the repo for the course:

```bash
git clone https://github.com/skooter500/gameengines2015 
```  
	If you already have a clone of the lab, then you canjust pull the latest version:
	
```bash
git pull 
```  

Here is what the finished project should look like (click the image for the video):

	[![YouTube](http://img.youtube.com/vi/ic173YnL-ss/0.jpg)](https://www.youtube.com/watch?v=ic173YnL-ss)
	
	
- First thing to do is to set the ferdelance initial position to be (0, 0, 0), the cobramk2 position to be (20, 0, 0) and the camera to be (5, 20, 0)
- Now add a new component to the Ferdelance and call it Guard.
- Add the following fields:

	```C#
	public List<Vector3> waypoints = new List<Vector3>();
	public bool randomPath = true;
	public float radius = 100;
	public float speed = 0.5f;    

	public GameObject player;
	public float fov = 45.0f;
	public float range = 20.0f;
	int currentWaypoint = 0;
	```
- Add code to the Start method to add 9 random positions to the waypoints array. You should also add the current position. You can use ```.Add``` to add something to a list. You can use Random.randomInsideUnitSphere to generate a random Vector3 inside the unit sphere. Dont forget to set the y to be 0 and multiply by the radius. Also you iterate a List the same way as an array and you can use [] to get an emelemnt out of the List and .Count to return the number of elements in the List
- Add a method called ```void OnDrawGizmos()```. Gizmos in Unity get drawn in the Scene window. You can display them in the Game window by toggling the option in the game window toolbar. Gizmos are things that don't get drawn in the game, but are useful in helping you to develop your game. In this method, you should draw the path. Here are some methods you will want to use:

	```C#
	Gizmos.color = Color.cyan;
    Gizmos.DrawLine(v1, v2); // v1 and v2 are the start and end points of the line. They are of type Vector3
	Gizmos.DrawWireSphere(c, r); // Draws a sphere centered at c with radius r
	``` 	
- In the Update method, put code to have the Ferdelance follow the path. If the distance to the next waypoint < 0.1f, you should advance to the next waypoint. You can use ```Vector3.Distance``` to return the distance between two Vector3's. Make sure your Ferdelance follows the path before moving on to the next part
- Drag the cobramk2 onto the player field of the Guard
- Now add code to the Update method to get the Ferdelance to switch behaviour if the cobramk2 is in range and inside the fov. When this happens, the Ferdelance should move to intercept the cobramk2
- If the cobramk2 goes out of range or out of the fov of the Ferdelance, the Ferdelance should return to following it's path 

## Lecture
- Video of the lecture about Newtonian Physics:
	[![YouTube](http://img.youtube.com/vi/SmMXvnIFiTU/0.jpg)](https://www.youtube.com/watch?v=SmMXvnIFiTU)
	   	
# Week 6
## Lab 

Modify the program we made in the lab/class last week to use forces/physics to move instead of movement with uniform velocity. The code we made last week is scene3 in the vectors1 project. To fake rotations, you can just make the velocity vector the forward vector at the end of the Update method. You can just try to get it working yourself first, and if you can't get it to work, then look up how to implement the [seek steering behaviour](http://natureofcode.com/book/chapter-6-autonomous-agents/).

Once you have it working, make a game component called PathFollower that you can attach to any game object. Then make some procedural paths, instantiate some game objects from a prefab and have them follow the path. 

You could try using sin and cos to make:

- A circle
- A figure of 8
- A zigzag pattern

This is one I made yesterday for some inspiration:

[![YouTube](http://img.youtube.com/vi/BuyQNxwLC9g/0.jpg)](https://www.youtube.com/watch?v=BuyQNxwLC9g)

When you are done, [upload some pictures/videos of your creations to the slack](http://gameengines2015.slack.com).

- How I made the path in the video is in Scene4 in the Vectors1 Unity Project now.

## Lecture
- The procedural mesh generation in Unity example we made is in in Scene5 in the Vectors1 Unity Project now. The class ProcMesh is the class we made on Friday. ProcGen is a more complete example with colliders.

## Week 7
### Lecture
- Check out the Vector1 one project for lots of examples of procedurally generated stuff in Unity. In the class we went through how to make an infinitely regenerating terrain using tiling

## Week 8
### Lab

Today you could work on your assignment. If you still need some inspiration, you can try and draw the following geometric shapes by programming a mesh procedurally:
- A cube
- A pyramid
- A sphere (you can look up how to make an icosphere)

You could also try drawing the texture procedurally with some patterns like a spiral. 

### Lecture
- Video of the class:

	[![YouTube](http://img.youtube.com/vi/IXySkVFNhdk/0.jpg)](https://www.youtube.com/watch?v=IXySkVFNhdk)

- Check out the [lecture notes on Quaternions (Game Engines 5)]((https://onedrive.live.com/redir?resid=AB603D769EDBF24E!263984&authkey=!AE-BvjCbphg3dOs&ithint=folder%2clnk))

## Week 9

### Lab

In this lab we will be making quaternions!

Clone the repo for the course and switch to the lab9 branch:

```
git clone https://github.com/skooter500/gameengines2015
git checkout lab9
```

Open up the lab9 scene

When you build and run the project, you will see the the Cobra Mk III  and the Ferdelance. You can control the movement of the Ferdelance using the arrow keys. You can also get the Ferdelance to go up and down using the O and L keys. To complete this lab you will need to make use of the following API calls:

```C#
Vector3.Dot
Vector3.Cross
Vector3.Normalize
Mathf.Acos
Mathf.Deg2Rad
Quaternion.AxisAngle // Make a quaternion. Dont forget the angle parameter is in degrees!
Quaternion.Slerp // This slerps between two quaternions
```
### Part 1

Generate a quaternion for the Cobra Mk III so that it always faces the Ferdelance.

### Part 2

Modify your code so that when you press the space key, the Cobra Mk III gradually turns to face the Ferdelance. Use the ```Quaternion.Slerp``` method to achieve this. ```Quaternion.Slerp``` slerps between two quaternions depending on the value of the *t* parameter. If *t* is 0, the first quaternion is returned. If *t* is 1 the second quaternion is returned. If *t* = 0.5f then a quaternion half way between the first and second quaternions is returned and so on. This is what it should look like:

[![Video](http://img.youtube.com/vi/lkD9tAo9T7s/0.jpg)](http://www.youtube.com/watch?v=lkD9tAo9T7s)

You can use the ```startQuaternion```, ```endQuaternion``` and slerping fields to help achieve this. Just add the ```timeDelta``` to *t* for now.

### Part 3
Have the speed of rotation controlled by the ```turnRate``` field. This field is given in radians per second. To complete this you will have to:

- Calculte the angle that the Cobra Mark III needs to rotate. Use ```Mathf.Acos``` and ```Vector3.Dot```
- Calculate the time required to do this (angle / turnRate)
- Calculate what you need to add to t based on the time required and the time delta.

If you would like to try a different challenge, try implementing the snake-like harmonic motion andfrom this video apply it to some cubes:

[![Video](http://img.youtube.com/vi/SdKvWnG8gI/0.jpg)](http://www.youtube.com/watch?v=SdKvWnG8gI)

Here is the [source code in Processing](https://github.com/skooter500/DT228-OOP-2015/tree/master/processing/harmonic) that implements this

How I would do it is:

- Make an empty game object
- Attach a new game component
- Expose a field for the number of segments and the speed of the animation
- In ```Start```, instantiate cubes for the segments and scale them on the y so that they are flatter
- Position the segments appropriately
- Instantiate empty game objects for the rotation points and position them appropriately
- Parent the cubes to them  
- In update, make quaternions for each of the rotation points