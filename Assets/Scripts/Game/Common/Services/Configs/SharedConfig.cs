using System;

namespace Game.Common.Services.Configs
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class SharedConfig : Attribute
    {
        public string ReferenceTag = "$ref:";
    }
}