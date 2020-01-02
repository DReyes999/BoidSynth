# BoidSynth
 A procedural synthesizer instrument played with a flock of boids
 
 The name "boid" corresponds to a shortened version of "bird-oid object", which refers to a bird-like object which follows a pattern of behaviors that result in flocking. These flocking behaviors were developed by Craig Reynolds in 1986.
 
 The Boidsynth is a WIP developed in Unity version 2018.2.4f1. The program creates a flock of boids and uses their behavior to drive a synthesizer plugin built in PureData. 
 
 To run: clone or download the project and open it in Unity 2018.2.4f1 or later. Select a scene from assets > scenes. Press Play. 
 
 **On the master branch:**
 The project currently has 3 scenes found in Assets > Scenes. 
 Each scene has distinctly different boid behavior which has a different effect on the way the synth is played. 
 
#Scenes:#
**Everybody Just Wander**
Boids do not flock. Instead they pick a random target on the screen every x number of seconds and path to that target. 

**Flock With No Leader**
Boids Flock. They follow a set of rules for alignment, avoidance, cohesion, and staying within a certain radius so as to remain  in the viewable area. There is currently a known issue where the boids may settle into a pattern of circling along the edge of the boundary radius.

**FollowtheLeader_Boidsynth07**
Boids pseudo-flock. A leader is spawned which follows the same behavior as from 'Everybody just wander'. It picks a random target on screen and paths to that target. The follower boids path to the leader. 

 
 
 
 
 
