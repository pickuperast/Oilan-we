using System.Collections.Generic;
using System;
using UnityEngine;
namespace Temirlan
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class |
        AttributeTargets.Struct, Inherited = true)]
    public class ConditionallyHideAttribute : PropertyAttribute
    {
        public string ConditionalSourceField = "";
        public System.Type _type;
        public GameType gameType;
        public Type type;
        public bool boolean;
        public ConditionallyHideAttribute(string ConditionalSourceField, Type type)
        {
            this.ConditionalSourceField = ConditionalSourceField;
            this.type = type;
            _type = typeof(Type);
        }
        public ConditionallyHideAttribute(string ConditionalSourceField, GameType gameType)
        {
            this.ConditionalSourceField = ConditionalSourceField;
            this.gameType = gameType;
            _type = typeof(GameType);
        }
    }

}