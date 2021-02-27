using System;
using System.Collections.Generic;
using TempElementsLib.Interfaces;

namespace TempElementsLib
{
    public class TempElementsStack : ITempElements
    {
        private Node current = null;

        private class Node : IDisposable
        {
            public readonly ITempElement El;
            public Node Prev = null;
            public Node(ITempElement el, Node prev = null)
            {
                El = el;
                Prev = prev;
            }

            public void Dispose()
            {
                El.Dispose();
            }
        }
        public IReadOnlyCollection<ITempElement> Elements { get
            {
                var list = new List<ITempElement>();
                var node = current;
                if(node != null) {
                    list.Add(node.El);
                    while(node.Prev != null)
                    {
                        node = node.Prev;
                        list.Add(node.El);
                    }
                }
                return list;
            }
        }

        private int count = 0;
        private int maxCount = 10;
        public int Count => count;

        public T AddElement<T>() where T : ITempElement, new()
        {
            Node node = new Node(new T(), current);
            current = node;
            count++;
            if(Count > maxCount)
            {
                var list = Elements;
                Node prevFirst = current.Prev, newFirst = current;

                while(prevFirst.Prev != null)
                {
                    newFirst = prevFirst;
                    prevFirst = prevFirst.Prev;
                }
                prevFirst.Dispose();
                newFirst.Prev = null;

            }
            return (T)current.El;
        }

        public T Pop<T>()
        {
            var el = current.El;
            current = current.Prev;
            return (T)el;
        }

        ~TempElementsStack()
        {
            Console.WriteLine("test");

            Dispose();
        }

        public void Dispose()
        {
            if(current != null)
            {
                current.Dispose();
                while(current.Prev != null)
                {
                    current = current.Prev;
                    current.Dispose();
                }
                current = null;
            }
        }

        public void RemoveDestroyed()
        {
            while(current.El.IsDestroyed)
            {
               current = current.Prev;
            }
            while(current.Prev != null)
            {
                if(current.Prev.El.IsDestroyed)
                {
                    current.Prev = current.Prev.Prev;
                }
            }
        }
    }
}
