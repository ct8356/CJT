﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJT {
    public class MessageEventArgs : EventArgs {
        public string Message { get; private set; }

        public MessageEventArgs(string message) {
            Message = message;
        }

    }
}
