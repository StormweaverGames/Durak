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
    class AttackerLose : IGameStateRule
    {
        public bool IsEnabled
        {
            get;
            set;
        }

        public string ReadableName
        {
            get
            {
                return "Verify attacking players gets battling cards";
            }
        }

        public void ValidateState(PlayerCollection players, GameState state)
        {
            // Only run this state update if the attacker is forfeiting
            if (state.GetValueBool(Names.ATTACKER_FORFEIT))
            {
                // Get the current round and the discard pile from the state
                int round = state.GetValueInt(Names.CURRENT_ROUND);
                CardCollection discard = state.GetValueCardCollection(Names.DISCARD);

                // Iterate over over all the previous rounds, as this round has no attacking or defending cards
                for (int index = 0; index < round; index++)
                {
                    // Add the cards to the discard pile
                    discard.Add(state.GetValueCard(Names.ATTACKING_CARD, index));
                    discard.Add(state.GetValueCard(Names.DEFENDING_CARD, index));

                    // Remove the cards from the state
                    state.Set<PlayingCard>(Names.ATTACKING_CARD, index, null);
                    state.Set<PlayingCard>(Names.DEFENDING_CARD, index, null);
                }

                // Update the discard pile
                state.Set(Names.DISCARD, discard);

                // Move to next match
                Utils.MoveNextDuel(state, players);
            }
        }
    }
}