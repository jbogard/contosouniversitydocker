using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xunit;

namespace ContosoUniversity.IntegrationTests
{
    public abstract class IntegrationTestBase : IAsyncLifetime
    {
        private static readonly AsyncLazy<bool> Mutex = new AsyncLazy<bool>(() => Initialize());

        public virtual async Task InitializeAsync()
        {
            await Mutex;
        }

        public virtual Task DisposeAsync() => Task.CompletedTask;

        private static async Task<bool> Initialize() 
        {
            await SliceFixture.ResetCheckpoint();

            return true;
        }
    }

    class AsyncLazy<T> : Lazy<Task<T>> 
    { 
        public AsyncLazy(Func<T> valueFactory) : 
            base(() => Task.Factory.StartNew(valueFactory)) { }

        public AsyncLazy(Func<Task<T>> taskFactory) : 
            base(() => Task.Factory.StartNew(() => taskFactory()).Unwrap()) { } 

        public TaskAwaiter<T> GetAwaiter() => Value.GetAwaiter();
    }
}