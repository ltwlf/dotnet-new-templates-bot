using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;

namespace Bot_Builder_Auth_Bot_V4
{
    public class AuthDialog : ComponentDialog
    {
        private const string initialId = "waterfall";
        private const string loginPromptName = "login";

        public AuthDialog(string dialogId, string connectionName) : base(dialogId)
        {
            InitialDialogId = initialId;
            AddDialog(Prompt(connectionName));
            AddDialog(new WaterfallDialog(initialId, new WaterfallStep[] { PromptStepAsync, LoginStepAsync }));
        }

        /// <summary>
        /// This <see cref="WaterfallStep"/> prompts the user to log in.
        /// </summary>
        /// <param name="step">A <see cref="WaterfallStepContext"/> provides context for the current waterfall step.</param>
        /// <param name="cancellationToken" >(Optional) A <see cref="CancellationToken"/> that can be used by other objects
        /// or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task"/> representing the operation result of the operation.</returns>
        private static async Task<DialogTurnResult> PromptStepAsync(WaterfallStepContext step, CancellationToken cancellationToken)
        {
            return await step.BeginDialogAsync(loginPromptName, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// In this step we check that a token was received and prompt the user as needed.
        /// </summary>
        /// <param name="step">A <see cref="WaterfallStepContext"/> provides context for the current waterfall step.</param>
        /// <param name="cancellationToken" >(Optional) A <see cref="CancellationToken"/> that can be used by other objects
        /// or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task"/> representing the operation result of the operation.</returns>
        private static async Task<DialogTurnResult> LoginStepAsync(WaterfallStepContext step, CancellationToken cancellationToken)
        {
            // Get the token from the previous step. Note that we could also have gotten the
            // token directly from the prompt itself. There is an example of this in the next method.
            var tokenResponse = (TokenResponse)step.Result;
            if (tokenResponse != null)
            {
                await step.Context.SendActivityAsync("You are logged in now :)", cancellationToken: cancellationToken);
                return Dialog.EndOfTurn;
            }

            await step.Context.SendActivityAsync("Error! Please try again...", cancellationToken: cancellationToken);
            return Dialog.EndOfTurn;
        }

        /// <summary>
        /// Prompts the user to login using the OAuth provider specified by the connection name.
        /// </summary>
        /// <param name="connectionName"> The name of your connection. It can be found on Azure in
        /// your Bot Channels Registration on the settings blade. </param>
        /// <returns> An <see cref="OAuthPrompt"/> the user may use to log in.</returns>
        private static OAuthPrompt Prompt(string connectionName)
        {
            return new OAuthPrompt(
                loginPromptName,
                new OAuthPromptSettings
                {
                    ConnectionName = connectionName,
                    Text = "Please sign in",
                    Title = "Sign in",
                    Timeout = 300000, // User has 5 minutes to login (1000 * 60 * 5)
                });
        }

    }
}