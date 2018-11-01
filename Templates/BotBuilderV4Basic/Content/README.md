# basic-bot
This bot has been created using [Microsoft Bot Framework](https://dev.botframework.com),
- Use [LUIS](https://luis.ai) to implement core AI capabilities
- Implement a multi-turn conversation using Dialogs
- Handle user interruptions for such things as Help or Cancel
- Prompt for and validate requests for information from the user

# Install Bot Tools
To install all Bot Builder tools - 
```bash
npm i -g msbot chatdown ludown qnamaker luis-apis botdispatch luisgen
```

# To try this sample

```bash
dotnet new -i "Ltwlf.BotBuilderV4.Basic"  
dotnet new bot-echo -n "My.BasicBot"     
```
- [Optional] Update the `appsettings.json` file with your botFileSecret. Read more about [Bot Secrets](https://github.com/Microsoft/botbuilder-tools/blob/master/packages/MSBot/docs/bot-file-encryption.md). 
  - For Azure Bot Service bots, you can find the botFileSecret under application settings.
  - If you use [MSBot CLI](https://github.com/microsoft/botbuilder-tools) to encrypt your bot file, the botFileSecret will be written out to the console window.
  - If you used [Bot Framework Emulator **V4**](https://github.com/microsoft/botframework-emulator) to encrypt your bot file, the secret key will be available in bot settings. 

## Prerequisites
### Set up LUIS via Command Line
- Navigate to [LUIS portal](https://www.luis.ai).
- Click the `Sign in` button.
- Click on your name in the upper right hand corner and the `Settings` drop down menu.
- Note your `Authoring Key` as you will need this later.
- In a command line  session:
Navigate in the shell to the project's root folder    
    - Install LUIS command line tool:
`npm install -g luis-apis`  
    - Import the LUIS application:
`luis import application --authoringKey [authoringKey] --region [region] --in .\CognitiveModels\basic-bot.luis --msbot | msbot connect luis --stdin`  

## Visual Studio Code
- Debug with Crtl+Shit+D 

## Testing the bot using Bot Framework Emulator
[Microsoft Bot Framework Emulator](https://aka.ms/botframework-emulator) is a desktop application that allows bot developers to test and debug
their bots on localhost or running remotely through a tunnel.
- Install the Bot Framework Emulator from [here](https://aka.ms/botframework-emulator).
### Connect to bot using Bot Framework Emulator
- Launch the Bot Framework Emulator
- File -> Open bot and navigate to `BotBuilder-Samples\samples\csharp_dotnetcore\13.Basic-Bot-Template` folder
- Select `BotConfiguration.bot` file

# Deploy this bot to Azure
You can use the [MSBot](https://github.com/microsoft/botbuilder-tools) Bot Builder CLI tool to clone and configure any services this sample depends on. 

To clone this bot, run
```
msbot clone services -f deploymentScripts/msbotClone -n <BOT-NAME> -l <Azure-location> --subscriptionId <Azure-subscription-id>
```
# Further reading
- [Bot Framework Documentation](https://docs.botframework.com)
- [Bot basics](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-basics?view=azure-bot-service-4.0)
- [Activity processing](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-concept-activity-processing?view=azure-bot-service-4.0)
- [LUIS](https://luis.ai)
- [Prompt Types](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-prompts?view=azure-bot-service-4.0&tabs=javascript)
- [Azure Bot Service Introduction](https://docs.microsoft.com/en-us/azure/bot-service/bot-service-overview-introduction?view=azure-bot-service-4.0)
- [Channels and Bot Connector Service](https://docs.microsoft.com/en-us/azure/bot-service/bot-concepts?view=azure-bot-service-4.0)
- [QnA Maker](https://qnamaker.ai)

