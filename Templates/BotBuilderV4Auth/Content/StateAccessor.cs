// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;

namespace Bot_Builder_Auth_Bot_V4
{
    /// <summary>
    /// Stores counter state for the conversation.
    /// Stored in <see cref="Microsoft.Bot.Builder.ConversationState"/> and
    /// backed by <see cref="Microsoft.Bot.Builder.MemoryStorage"/>.
    /// </summary>
   public class StateAccessor
    {
        // The name of the dialog state.
        public static readonly string DialogStateName = $"{nameof(StateAccessor)}.DialogState";

        /// <summary>
        /// Gets or Sets the DialogState accessor value.
        /// </summary>
        /// <value>
        /// A <see cref="DialogState"/> representing the state of the conversation.
        /// </value>
        public IStatePropertyAccessor<DialogState> ConversationDialogState { get; set; }
    }
}
