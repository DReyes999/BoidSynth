# BoidSynth
 A procedural synthesizer instrument played with a flock of boids
 
 The name "boid" corresponds to a shortened version of "bird-oid object", which refers to a bird-like object which follows a pattern of behaviors that result in flocking. These flocking behaviors were developed by Craig Reynolds in 1986.
 
 The Boidsynth is a WIP developed in Unity version 2018.2.4f1. The program creates a flock of boids and uses their behavior to drive a synthesizer plugin built in PureData. 
 
 To run: clone or download the project and open it in Unity 2018.2.4f1 or later. Select a scene from assets > scenes. Press Play. 
 
 **On the master branch:**
 The project currently has 3 scenes found in Assets > Scenes. 
 Each scene has distinctly different boid behavior which has a different effect on the way the synth is played. 
 
# Scenes:
## **Everybody Just Wander**
Boids do not flock. Instead they pick a random target on the screen every x number of seconds and path to that target. 

## **Flock With No Leader**
Boids Flock. They follow a set of rules for alignment, avoidance, cohesion, and staying within a certain radius so as to remain  in the viewable area. There is currently a known issue where the boids may settle into a pattern of circling along the edge of the boundary radius.

## **FollowtheLeader_Boidsynth07**
Boids pseudo-flock. A leader is spawned which follows the same behavior as from 'Everybody just wander'. It picks a random target on screen and paths to that target. The follower boids path to the leader. 

# Options:

I'm working on creating a UI which will make adjusting each of these parameters much more user-friendly. However in the meantime if you wish to tweak some behavior, here are some easy ones to start with:

## Number of boids

In each scene you will find a 'flock' object responsible for spawning the agents. You can adjust the public 'starting count' parameter to change the number of boids spawned upon play. The volume of the audio source on each individual boid is adjusted based on this paramter 

## Note Mode

In each scene you will find a 'flock' object responsible for spawning the agents. You can adjust the public 'Note Mode' parameter to change how each agent selects the pitch it will play. 

* Note Mode 1: 
   *The agent will select a random midi note from an array of notes representing an octave of the C major scale
* Note Mode 2: 
   *A specific midi note from the C major scale is assigned to the agent depending on which angle the boid is facing in a 360 degree circle

 
 
 
 
 
