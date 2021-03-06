﻿using Durak.Common;
using Durak.Common.Cards;
using Durak.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurakGame.Rules
{
    /// <summary>
    /// Handles checking to see if the battle is over (ie. round = 6), and updates the game state accordingly
    /// </summary>
    public class VerifyBattleWin : IGameStateRule
    {
        /// <summary>
        /// Gets or sets whether this rule is enabled
        /// </summary>
        public bool IsEnabled
        {
            get;
            set;
        }
        /// <summary>
        /// Gets the human readable name for this rule
        /// </summary>
        public string ReadableName
        {
            get
            {
                return "Check won battle";
            }
        }

        /// <summary>
        /// Handles validating the server state. If the round is 6, this handles moving to the next round, giving the defender the win
        /// </summary>
        /// <param name="server">The server to execute on</param>
        public void ValidateState(GameServer server)
        {
            // Todo check won battle

            if (server.GameState.GetValueInt(Names.CURRENT_ROUND) == 6)
            {
                // Get the current round and the discard pile from the state
                int round = server.GameState.GetValueInt(Names.CURRENT_ROUND);
                CardCollection discard = server.GameState.GetValueCardCollection(Names.DISCARD);

                // Iterate over over all the previous rounds, as this round has no attacking or defending cards
                for (int index = 0; index < round; index++)
                {
                    // Add the cards to the discard pile
                    discard.Add(server.GameState.GetValueCard(Names.ATTACKING_CARD, index));
                    discard.Add(server.GameState.GetValueCard(Names.DEFENDING_CARD, index));

                    // Remove the cards from the state
                    server.GameState.Set<PlayingCard>(Names.ATTACKING_CARD, index, null);
                    server.GameState.Set<PlayingCard>(Names.DEFENDING_CARD, index, null);
                }

                // Update the discard pile
                server.GameState.Set(Names.DISCARD, discard);

                // Move to the next duel
                Utils.MoveNextDuel(server);
            }
        }
    }
}
