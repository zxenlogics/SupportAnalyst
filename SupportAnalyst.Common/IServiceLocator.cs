﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportAnalyst.Common
{
    public interface IServiceLocator
    {
        T GetInstance<T>() where T : class;
    }
}
