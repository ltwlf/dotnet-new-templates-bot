# Dotnet new templates for Bot Builder SDK 4.0

## Install the published nuget packages with:

dotnet new -i "Ltwlf.BotBuilderV4.Basic"    
dotnet new -i "Ltwlf.BotBuilderV4.Echo"
dotnet new -i "Ltwlf.BotBuilderV4.Auth"

## Create an echo bot

dotnet new bot-echo -n "My.EchoBot"     

## Create a basic bot

dotnet new bot-echo -n "My.BasicBot"     

## Create an auth bot

dotnet new bot-auth -n "My.AuthBot"     