# Myo Armband added into unity game
- Gareth Lynskey - G00312651
- Patrick Griffin - G00314635
- Module: Gesture Based UI

## Project Overview:
We are converting a project created last year on Unity to incorporate the use of gestures in the game. Here is the link to last years Project which we wish to convert https://github.com/lynskey08/Unity-Mobile-Application-Game .
The Game itself is a space shooter survival game. 
Hardware and Technologies 
-	Myo armband 
-	Unity
-	GitHub 
-	Visual studio instead of mono for debugging.

## Purpose of the application
The purpose of the application is to allow the user to control a spaceship with the Myo Armband while avoiding/shooting rocks and enemies and to demonstrate the use of gestures using the Myo Armband. 
Gestures identified as appropriate for this application
Gestures that we are working on and hope to complete. We thought that these gestures made most sense in our opinion for the game. We wanted to use as many gestures as possible with significance to the game.




# Unity Space Shooter Game (Before converting to myo)
## MobileAppFixed
The 13th commit in my original repository 'MobileApp' was corrupt and I wasn't able to commit stuff after that. I re-comitted everything here. Here is the link to the previous repository with my commits up until i was unable to continue: https://github.com/lynskey08/MobileApp

## Gareth Lynskey - G00312651

## Space Shooter Windows 10 Game

##### Game:
- This is a simple space shooter game. On open, the game consists of a main menu with a Play game button, Settings button and a Highscores button each linking to its corresponding page.
- You are a spaceship and the aim of the game is to stay alive as long as possible and destroy the asteroids and enemy hazards spawning above(10 points for asteroids and 30 points for enemies), and the score is kept in the top left-hand corner of the screen.
- When the player ship is destroyed a text pop-up is displayed on screen saying " gameover".
- The user is prompted to enter a player name in the input field and click enter which saves their score to a database I created on my virtual machine which then displays their score in the Highscores page located on the main menu which only displays the top 10 scores.
- User is also given the option to restart the game or return to the main menu.
- On the Settings Page there is a volume slider to increase or decrease volume and there is also a mute button.

##### How to Play:
- In order to navigate the spaceship, the w,a,s, and d keys are used. 
- The left mouse botton is used to shoot the spaceship's laser.

##### Bugs:
- Very seldom some asteroids disappear randomly without a collision happenning.
- The background audio keeps playing even when it's not in playmode(in unity).
- Sometimes the DestroyByBoundary script doesn't destroy the enemy laser leaving the game view, resulting in the extra laser audio being heard.
- The mute button sets the volume to 0 but it doesn't save the image state of the mute button, meaning the button can be checked(and the audio is muted) and unchecked(and the audio is still muted).

### Project References:
- https://www.assetstore.unity3d.com/en/#!/content/39964
- https://unity3d.com/learn/tutorials/projects/space-shooter/introduction?playlist=17147
- http://wiki.unity3d.com/index.php?title=Server_Side_Highscores
- https://www.youtube.com/watch?v=9hPYXi5aXzw

