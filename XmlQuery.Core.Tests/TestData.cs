using System.Diagnostics.Contracts;

namespace XmlQuery.Core.Tests
{
    public class TestData
    {
        public static TestData<T1, TResult> Create<T1, TResult>(T1 parameter, TResult result)
        {
//            Contract.Ensures(Contract.Result<TestData<T1, TResult>>() != null);

            return new TestData<T1, TResult>(parameter, result);
        }
    }

    public class TestData<T1, TResult>
    {
        public T1 Parameter { get; private set; }
        public TResult Result{ get; private set; }

        public TestData(T1 parameter, TResult result)
        {
            Parameter = parameter;
            Result = result;
        }
    }
}