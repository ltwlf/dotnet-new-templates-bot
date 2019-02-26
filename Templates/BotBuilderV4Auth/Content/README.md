# Setup
Please register an App in Azure AD as described here:
https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-authentication?view=azure-bot-service-4.0&tabs=csharp

Configure the appId and the appPassword in the BotConfiguration.bot file.

In the Channel Registration's OAuth Connection Settings choose Azure AD v1 or v2 and name the connection "AzureAD"

Don't forget to configure ngrok in the bot emulator v4. If you haven't configured ngrok you will not receive the callback when the user has signed in.

Enjoy!
