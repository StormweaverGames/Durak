﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Durak.Common.Cards
{
  public class CardEventArgs : EventArgs
  {
    public PlayingCard Card { get; set; }
  }
}
