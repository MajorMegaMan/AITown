GOAP System written by Lucas Petersen, AIE, Complex Game systems, 2021

This System uses a GOAP algorithim for agents to plan a queue of actions to perform in order for the goal state
to be reached.

There are a few intended ideas behind many of these classes.

-There should be 1 "Global" GOAPWorld State. 
 Each agent would have it's own "SelfishNeeds" GOAPWorld State.

 The global state will contain the values that don't require any specific value based on an agent.
 For example, to pick up a ball an agent will need to know from the global values that there is a "ball available".
 But when it does pick up the ball, it now needs to know that the global state "ball available" is false and it's
 selfish state "holding ball" is true.
 Other agents now know that the ball is not available and they are not holding it.

-Actions and Behaviours are intended to only be created once. This isn't explicitly a problem as there wouldn't be
 errors, but it would create unneccesary classes where the planner may have added 2 or more of the same class type,
 and this would drastically slow down performance.

Unity Version.

The AIAgent class derives from MonoBehaviour and is mostly an example of how an Agent might be set up.
All the required Setters for the GOAPAgent are setup for the user in this example. 
The member variables that the AIAgent could be changed or removed at the user's discretion.

The AIBehaviour Class derives from GOAPBehaviour<UnityEngine.GameObject>.
This is also mostly an example of how a behaviour might be set up, however, I made a small component system for
this specific class in an effort to simplify creating behaviours.
Like actions Behaviours are intended to only be made once.

Inheritables:
	BehaviourComponent
	This is an abstract class that the user can inherit make a component for their desired behaviours.
	Behaviour Components are also only intended to be made once as one component can be added to many
	behaviours.

	AIAgentAction
	This is an abstract class that extends GOAPAgentAction<GameObject>.
	Again more of an example to show that there are Unity specifc variables that an action may need to carry.
	In this case this one has an Animation Trigger.

Editor:
Both inheritables have been added to the Asset/Create Menu, where a template will be created in the user's Assets
folder. These templates are designed to "guide" the user in making the associated class.

There is a config file in the Editor folder where the user can change certain variables, such as the
creation directory.

This Package also includes a small tool in Window/GOAP Inspector.
As the name suggests it will display the Targeted script if it is a GOAPAction or BehaviourComponent.
This works by creating an instance of the class Type using a default constructor and reading the
effects GOAPWorldState.