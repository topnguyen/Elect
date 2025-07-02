namespace Elect.Core.ObjUtils
{
    /// <inheritdoc />
    public abstract class ElectDisposableModel : IDisposable
    {
        private bool IsDisposed { get; set; }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool isDisposing)
        {
            if (IsDisposed)
            {
                return;
            }
            if (isDisposing)
            {
                DisposeUnmanagedResources();
            }
            IsDisposed = true;
        }
        protected virtual void DisposeUnmanagedResources() { }
        ~ElectDisposableModel()
        {
            Dispose(false);
        }
    }
}
