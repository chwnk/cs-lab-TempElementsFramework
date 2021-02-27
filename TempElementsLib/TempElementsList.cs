using System;
using System.Collections.Generic;
using TempElementsLib.Interfaces;

namespace TempElementsLib
{
    public class TempElementsList : ITempElements
    {
        private bool disposed;
        private readonly List<ITempElement> elements = new List<ITempElement>();

        public IReadOnlyCollection<ITempElement> Elements => elements;

        ~TempElementsList() => Dispose(false);

        public T AddElement<T>() where T : ITempElement, new()
        {
            var el = new T();
            elements.Add(el);
            return el;
        }
        
        public void DeleteElement<T>(T element) where T : ITempElement, new()
        {
            element.Dispose();
            elements.Remove(element);
        }

        public void RemoveDestroyed() => elements.RemoveAll((el) => el.IsDestroyed);

        public bool IsEmpty => ((ITempElements)this).IsEmpty;


        #region Dispose section ==============================================
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing) {
                    // TODO: dispose managed state (managed objects)
                    foreach(ITempElement el in elements) {
                        el.Dispose();
                        elements.Remove(el);
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                foreach(ITempElement el in elements) {
                    elements.Remove(el);
                }
                // TODO: set large fields to null
                disposed = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~TempDirsAndFolders()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
