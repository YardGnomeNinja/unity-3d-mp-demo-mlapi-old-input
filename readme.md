This is a barebones example of how to use the MLAPI multiplayer library for Unity. The majority is ripped straight from a video from https://www.youtube.com/c/StewHeckenberg/featured. I highly recommend his tutorials.

Created using this multiplayer library:

https://github.com/MidLevel/MLAPI/releases/

Created using a combination of these videos:

https://www.youtube.com/watch?v=miMCu5796KM

https://www.youtube.com/watch?v=-nS1gqSk458

Quick rundown:
1. Install MLAPI by downloading and running the install Unity Package from the URL above
2. Add plane to scene
3. Add character to scene
4. Add CharacterController component to character
5. Create and add script to character
6. Add NetworkedObject component to character via the Inspector window (This tells MLAPI to send info about this object over the network)
7. Add NetworkedTransform component to character via the Inspector window (This tells MLAPI to send object location information over the network)
8. Create a prefab of the character by dragging it to a folder under Assets in the Project window
9. Add empty object named Networking Manager to scene
10. Add component NetworkingManager to the Networking Manager object via the Inspector window
11. Choose a Network Transport via the Inspector window (I chose UnetTransport but plan to experiment with others)
12. Click the '+' under NetworkedPrefabs to add a new prefab
13. Drag the character prefab from the Project window into the control that appears (or click the dot-circle button to select the prefab)
14. Check the 'Default Player Prefab' checkbox
15. Delete the character from the scene (When you host or join a game, the default prefab will be created in the scene for you)

At the time of this writing, this was sufficient to get things going. You don't even really need to play with any values in the MLAPI components. You can test locally by following the instructions below.

To test locally:

Windows

1. Pull repository
2. Create a copy of the folder (We need to have two copies of Unity running at once)
3. Delete the following folders from the copy: Assets, ProjectSettings (We're going to symlink these to the original so updates to one affect the other)
4. Open a command prompt as administrator and navigate to the copy folder
5. Run the following commands:
6. mklink /D Assets ..\unity-3d-mp-demo-mlapi-old-input\Assets
7. mklink /D ProjectSettings ..\unity-3d-mp-demo-mlapi-old-input\ProjectSettings
8. Add and open both projects via Unity Hub and move the windows so you can see each at the same time
9. Start the game in both projects
10. In one, find the DontDestroyOnLoad object in the scene and expand to see NetworkingManager, click it
11. In the Inspector Window scroll down the NetworkingManager to find 3 buttons: Start Host, Start Server, Start Client
12. Click Start Host
13. In the other project, follow the same process from step 10. on, but click Start Client
14. You should now have two players in each game. Focusing on either window and using WASD and Spacebar, you should be able to navigate the scene and see it update both games.

Finally, Stew Heckenberg has this video https://youtu.be/ZiKW5vXPGz0 showing how to get animation to sync. The basic gist is you also need to add a NetworkedAnimator component to your player. But don't let me stop you from watching the vid.