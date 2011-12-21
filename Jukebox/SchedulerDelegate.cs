﻿using System;
using System.Diagnostics;
using Kayak;

namespace Jukebox
{
    class SchedulerDelegate : ISchedulerDelegate
    {
        public void OnException(IScheduler scheduler, Exception e)
        {
            Debug.WriteLine("Error on scheduler.");
            e.DebugStackTrace();
        }

        public void OnStop(IScheduler scheduler)
        {

        }
    }


}
