# WitAI-DiscordBot
A Discord Bot that integrates [WitAi](https://wit.ai/) for advanced text recognition

## What is this project?
This is a discord bot template that uses WitAI for advanced text recognition. You can download the release and run it bare-bones, or clone the project and setup custom logic using WitAI. NOTE: This project doesn't provide the "training" functionality. It just sends/receives discord messages to and from WitAi. Using these messages you can train your AI and make your bot respond accordingly.

## How does it work?
The first time you run/build the project it will ask for your Discord Bot Token and WitAI Project token. Upon successfully providing them, the info will be saved to the EXE directory and won't be asked for again. From this point on the discord bot will read every message that is sent in the servers it is in. It will take those messages and send them to your WitAI project so you can train it with those messages. After sending the messages a response from WitAI is also sent back to the bot. This response is the "text recognition" and you can use it to add advanced custom logic to your bot. In addition to all of this, the messages that the bot receives from discord are stored in a local database, so you can reuse the data and train other WitAI projects off of it.

## How to use it?
If you simply want to send training data to your WitAI project and don't care about the discord bot responding, then just download the latest release of the project and run ``WitAI Discord Bot.exe``. If you want to add custom logic to your project, clone the project and modify it as you wish to create your dream discord bot. To keep things easy and prevent programmers from accidentally modifying things they shouldn't, I've created the [ModifyableBot](https://github.com/gurrenm3/WitAI-DiscordBot/blob/master/WitAI%20DiscordBot.Console/Modifyable%20Bot.cs) class where you can put your code. This will help keep your bot's custom logic separate from the rest of the project and help prevent accidental issues. 

If you want to view the message database, go to [https://inloop.github.io/sqlite-viewer/](https://inloop.github.io/sqlite-viewer/) and drag and drop the ``DiscordMsgs.sqlite`` file into the window

## More info on WitAI
To get more info on WitAI, go [here](https://wit.ai/)
