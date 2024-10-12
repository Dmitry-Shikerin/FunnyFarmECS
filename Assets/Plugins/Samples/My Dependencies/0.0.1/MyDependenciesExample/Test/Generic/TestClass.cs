using System;

namespace MyDependencies.Sources.Test.Generic
{
    public class TestClass<T> : ITestClass<T>
    {
        private readonly T _value;

        public TestClass(T value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public T Value => _value;
    }
}