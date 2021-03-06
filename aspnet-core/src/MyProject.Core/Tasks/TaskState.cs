﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Tasks
{
        public enum TaskState : byte
        {
            /// <summary>
            /// The task is active.
            /// </summary>
            Active = 1,

            /// <summary>
            /// The task is completed.
            /// </summary>
            Completed = 2
        }
}
