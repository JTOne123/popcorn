﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Skyward.Popcorn
{
    /// <summary>
    /// This attribute is used to mark classes for automatic Mapping by PopcornConfiguration
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ExpandFromAttribute : Attribute
    {
        Type source;
        string includes;

        public ExpandFromAttribute(Type source, string defaultIncludes = null)
        {
            this.source = source;
            this.includes = defaultIncludes;
        }

        public Type SourceType { get { return source; } }
        public string Includes { get { return includes; } }
    }
}
