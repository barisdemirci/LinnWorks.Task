using System;
using System.Collections.Generic;
using System.Text;

namespace LinnWorks.Task.Common.Base
{
    public abstract class DisposableObject : IDisposable
    {
        private bool disposed;

        ~DisposableObject()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            disposed = true;
        }

        protected void ThrowIfDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException("The object is disposed", (Exception)null);
            }
        }
    }
}