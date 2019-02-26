// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;

namespace Bot_Builder_Auth_Bot_V4
{
    /// <summary>
    /// Represents a bot that processes incoming activities.
    /// For each user interaction, an instance of this class is created and the OnTurnAsync method is called.
    /// This is a Transient lifetime service.  Transient lifetime services are created
    /// each time they're requested. For each Activity received, a new instance of this
    /// class is created. Objects that are expensive to construct, or have a lifetime
    /// beyond the single turn, should be carefully managed.
    /// For example, the <see cref="MemoryStorage"/> object and associated
    /// <see cref="IStatePropertyAccessor{T}"/> object are created with a singleton lifetime.
    /// </summary>
    /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1"/>
    public class Bot : IBot
    {
        private readonly ConversationState _conversationState;
        private readonly StateAccessor _stateAccessor;
        private readonly DialogSet _dialogs;
        private readonly ILogger _logger;
        private const string ConnectionName = "AzureAD";

        /// <summary>
        /// Initializes a new instance of the <see cref="Bot"/> class.
        /// </summary>
        /// <param name="accessors">A class containing <see cref="IStatePropertyAccessor{T}"/> used to manage state.</param>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory"/> that is hooked to the Azure App Service provider.</param>
        /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-2.1#windows-eventlog-provider"/>
        public Bot(ConversationState conversationState, StateAccessor stateAccessor, ILoggerFactory loggerFactory)
        {
            if (loggerFactory == null)
            {
                throw new System.ArgumentNullException(nameof(loggerFactory));
            }

            _logger = loggerFactory.CreateLogger<Bot>();
            _logger.LogTrace("Bot turn started");
            _conversationState = conversationState;
            _stateAccessor = stateAccessor;

            _dialogs = new DialogSet(_stateAccessor.ConversationDialogState);
            _dialogs.Add(new AuthDialog(nameof(AuthDialog), ConnectionName));
        }

        /// <summary>
        /// Every conversation turn for our Echo Bot will call this method.
        /// There are no dialogs used, since it's "single turn" processing, meaning a single
        /// request and response.
        /// </summary>
        /// <param name="turnContext">A <see cref="ITurnContext"/> containing all the data needed
        /// for processing this conversation turn. </param>
        /// <param name="cancellationToken">(Optional) A <see cref="CancellationToken"/> that can be used by other objects
        /// or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task"/> that represents the work queued to execute.</returns>
        /// <seealso cref="BotStateSet"/>
        /// <seealso cref="ConversationState"/>
        /// <seealso cref="IMiddleware"/>
        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            DialogContext dc = null;

            switch (turnContext.Activity.Type)
            {
                case ActivityTypes.Message:
                    await HandleMessageAsync(turnContext, cancellationToken);
                    break;
                case ActivityTypes.Event:
                case ActivityTypes.Invoke:
                    // This handles the Microsoft Teams Invoke Activity sent when magic code is not used.
                    dc = await _dialogs.CreateContextAsync(turnContext, cancellationToken);
                    await dc.ContinueDialogAsync(cancellationToken);
                    break;
                case ActivityTypes.ConversationUpdate:
                    if (turnContext.Activity.MembersAdded != null)
                    {
                        await turnContext.SendActivityAsync("ConversationUpdate");
                        await turnContext.SendActivityAsync("Type \"Sign in\" to log in.");
                    }
                    break;
            }

            await _conversationState.SaveChangesAsync(turnContext);
        }

        public async Task HandleMessageAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var dc = await _dialogs.CreateContextAsync(turnContext, cancellationToken);

            var botAdapter = (BotFrameworkAdapter)turnContext.Adapter;
            var token = await botAdapter.GetUserTokenAsync(turnContext, ConnectionName, null, cancellationToken);

            if (turnContext.Activity.Text.ToLower() == "sign in")
            {
                await dc.BeginDialogAsync(nameof(AuthDialog));
            }
            else
            {
                await turnContext.SendActivityAsync("Echo: " + turnContext.Activity.Text);
                if (token == null)
                {
                    await turnContext.SendActivityAsync("Type \"Sign in\" to log in.");
                }
                else
                {
                    await turnContext.SendActivityAsync("your token:\n" + token.Token);
                }
            }

        }
    }
}
