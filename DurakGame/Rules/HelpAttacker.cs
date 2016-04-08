﻿using Durak.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurakGame.Rules
{
    class HelpAttacker
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
                return "Verify attacking player needs help";
            }
        }

        public bool AttackHelp(PlayerCollection players, GameMove move, GameState currentState, ref string reason)
        {
            if (currentState.GetValueBool("player_req_help"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}