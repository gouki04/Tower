using System;

namespace Tower
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DialogueCategoryAttribute : Attribute
    {
        public string Path { get; set; }
    }
}
