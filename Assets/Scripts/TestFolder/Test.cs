using System;
using UnityEngine;

namespace TestFolder
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class Test : PropertyAttribute
    {
        public Test()
        {
        }
    }
}