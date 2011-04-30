using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace XmlQuery.Core.Tests.Extensions
{
    public static class AssertExtensions
    {
        public static void ShouldMatch<T>(this IEnumerable<T> source, IEnumerable<T> expected, IEqualityComparer<T> comparer)
        {
            Contract.Requires(source != null);
            Contract.Requires(expected != null);
            Contract.Requires(comparer != null);

            int i = 0;
            using (IEnumerator<T>
                sourceEnumerator = source.GetEnumerator(),
                expectedEnumerator = expected.GetEnumerator())
            {
                while (true)
                {

                    if (!sourceEnumerator.MoveNext())
                    {
                        if (!expectedEnumerator.MoveNext()) return;
                        throw new Exception("source collection has less items than expected");
                    }
                    if (!expectedEnumerator.MoveNext())
                        throw new Exception("source collection has more items than expected");

                    if (!comparer.Equals(sourceEnumerator.Current, expectedEnumerator.Current))
                        throw new Exception("Items do not match at index " + i);
                    i++;
                }
            }
        }

        public static void AssertDataDrivenTest<TInput, TResult, TActual>(this IEnumerable<TestData<TInput, TActual>> parameters, Func<TInput, TResult> resultSelector, params Func<TActual, IResolveConstraint>[] constraints)
        {
            Contract.Requires(parameters != null);
            Contract.Requires(Contract.ForAll(parameters, p => p != null));
            Contract.Requires(resultSelector != null);
            Contract.Requires(constraints != null);
            Contract.Requires(Contract.ForAll(constraints, c => c != null));

            int i = 0;
            var exceptions = new List<Exception>();
            foreach (var input in parameters)
            {
                i++;
                TResult result;
                try
                {
                    result = resultSelector(input.Parameter);
                }
                catch (Exception e)
                {
                    Console.WriteLine(string.Format("Test {0} failed. Input: {1}", i, input.Parameter));
                    exceptions.Add(e);
                    continue;
                }
                foreach (var constraint in constraints)
                {
                    var resolvedConstraint = constraint(input.Result);
                    try
                    {
                        Assert.That(result, resolvedConstraint);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(string.Format("Test {0} failed. Input: {1}. Constraint: {2}", i, input.Parameter, resolvedConstraint));
                        exceptions.Add(e);
                    }
                }
            }
            if (exceptions.Any())
            {
                Assert.Fail(string.Join(Environment.NewLine + new string('-', 50) + Environment.NewLine, exceptions));
            }
        }

        public static CollectionEquivalentResolveConstraint<T> EquivalentTo<T>(this Is _, IEnumerable<T> expected, IEqualityComparer<T> comparer)
        {
            Contract.Requires(expected != null);
            Contract.Requires(comparer != null);

            return new CollectionEquivalentResolveConstraint<T>(expected, comparer);
        }
    }
}