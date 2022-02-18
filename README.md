# Block Puzzle Suite
How to trick Block Puzzle and learn something in the process.![screenshot.jpg](Img/art_screenshot.jpg)

This project is a simple yet functional integration of several matters such as game trees, command pattern, state machines and event based components.

**Introduction**
 
Block Puzzle is a popular, very addictive and deceitfully simple puzzle game.
![game-screen-01.jpg](Img/game-screen-01.jpg)

Lots of people use it everyday just for fun or to exercise their brains. It has a 10 by 10 cell matrix, a set of shapes made of one up to nine colored squares and three random shapes to play with. The goal is to complete rows/columns to remove from the board and keep playing (if you are concentrated enough) forever. After losing some month-lenght games, I started wondering how feasible it was to build a program to find possible solutions to specific situations in the game. Following careful thought and doing some research I decided I've found a good reason to spend time integrating things like game trees, state machines, behavioral patterns and event based components, to create such a program. Throughout this article, the guidelines to build a desktop application using C#, Windows Forms and .NET Framework will be presented. It has been a real challenge to find the balance between a trivial explanation and a complex lengthy treatment of the subject, and soon you'll find out if this objective was achieved.

**Design decisions**

It was clear that a GUI was absolutely needed. I've seen and coded a lot of command line prototypes, but to this project, displaying and manipulating of geometric colored shapes was paramount. As a direct consequence, to handle event orchestration complexity the sane way, a state machine will be implemented to control the interface behavior. On the same line of thought, to allow the user recover from mistaken actions, the command pattern with undo will be included. Data trees are used to register the sequence of states in a game and also to explore the possible solutions for a particular state. Given the foreseen increase in developing time caused by the former decisions, the reuse of existing components is neccesary.  

Let's move on to a high, architectural level. Some of Ted Faison and Ralf Westphal's ideas are used to build independent, low coupled, easy to test components. Those components, properly grouped, fit into MVC pattern. 

![fig01_MVC.jpg](Img/fig01_MVC.jpg)

The GUI -as View- is isolated in its own assembly. Its interactions with the components that Model the game and find solutions are serviced by the Controller, acting as mediator. All components provide functionality defined in its given interface and the signaling/switching mechanism is implemented with delegates and late binding.
TBC...