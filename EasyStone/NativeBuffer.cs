using System;
namespace EasyStone
{
    class NativeBuffer<T>
    {
        private const int InitialSpace = 1000;
        private const int DefaultResizeStep = 1000;

        T[] internalBuffer;
        int internalIndex;
        int bufferSize;

        public NativeBuffer()
        {
            bufferSize = InitialSpace;
            internalBuffer = new T[bufferSize];
            internalIndex = 0;
        }

        private void ResizeBuffer(int additionalSpace)
        {
            T[] newBuffer = new T[bufferSize + additionalSpace];
            internalBuffer.CopyTo(newBuffer, 0);
            internalBuffer = newBuffer;

            this.bufferSize += additionalSpace;
        }

        public void Add(T f)
        {
            if (internalIndex >= bufferSize)
            { ResizeBuffer(DefaultResizeStep); }

            internalBuffer[internalIndex] = f;
            internalIndex++;
        }

        public void Add(params T[] f1)
        {
            if (internalIndex + f1.Length > bufferSize)
            { ResizeBuffer(Math.Max(DefaultResizeStep, (internalIndex + f1.Length) - bufferSize)); }

            f1.CopyTo(internalBuffer, internalIndex);
            internalIndex += f1.Length;
        }

        public void Clear()
        {
            internalIndex = 0;
        }

        public T[] Data
        { get { return internalBuffer; } }

        public int Count
        { get { return internalIndex; } }
    }
}
