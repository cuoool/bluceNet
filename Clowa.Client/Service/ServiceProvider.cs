﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Clowa.Client.Interface; 

namespace Clowa.Client.Service
{
    public class ServiceProvider
    {
        public static IServiceLocator Instance { get; private set; }
        public static void RegisterServiceLocator(IServiceLocator s)
        {
            Instance = s;
        }
    }
}
