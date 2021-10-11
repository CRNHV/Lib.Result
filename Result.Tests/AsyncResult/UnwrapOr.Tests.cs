﻿namespace System1Group.Lib.Result.Tests.AsyncResult
{
    using System.Threading.Tasks;
    using NUnit.Framework;

    [TestFixture]
    public class AsyncResult_UnwrapOrAsync_Tests
    {
        [Test]
        public async Task Success_Ok()
        {
            var obj = new { field = "field" };
            var success = new Success<object, string>(obj);
            var async = success.ToAsyncResult();
            var obj2 = await async.UnwrapOrAsync(new object());

            Assert.That(obj, Is.EqualTo(obj2));
        }

        [Test]
        public async Task Failure_Ok()
        {
            var error = "error message";
            var obj = new { field = "field" };
            var failure = new Failure<object, string>(error);
            var async = failure.ToAsyncResult();

            var obj2 = await async.UnwrapOrAsync(obj);
            Assert.That(obj, Is.EqualTo(obj2));
        }

        [Test]
        public async Task Success_WithFunc_Ok()
        {
            var obj = new { field = "field" };
            var success = new Success<object, string>(obj);
            var async = success.ToAsyncResult();
            var obj2 = await async.UnwrapOrAsync(() => new object());

            Assert.That(obj, Is.EqualTo(obj2));
        }

        [Test]
        public async Task Failure_WithFunc_Ok()
        {
            var error = "error message";
            var obj = new { field = "field" };
            var failure = new Failure<object, string>(error);
            var async = failure.ToAsyncResult();

            var obj2 = await async.UnwrapOrAsync(() => obj);
            Assert.That(obj, Is.EqualTo(obj2));
        }

        [Test]
        public async Task Success_WithArgumentFunc_Ok()
        {
            var obj = new { field = "field" };
            var success = new Success<object, string>(obj);
            var async = success.ToAsyncResult();
            var obj2 = await async.UnwrapOrAsync(failure => new { field = failure });

            Assert.That(obj, Is.EqualTo(obj2));
        }

        [Test]
        public async Task Failure_WithArgumentFunc_Ok()
        {
            var error = "error message";
            var failure = new Failure<object, string>(error);
            var async = failure.ToAsyncResult();
            var obj = await async.UnwrapOrAsync(err => err);
            Assert.That(error, Is.EqualTo(obj));
        }
    }
}
