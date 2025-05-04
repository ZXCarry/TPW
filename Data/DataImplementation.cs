using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TP.ConcurrentProgramming.Data
{
  public class DataImplementation : DataAbstractAPI
  {
    #region ctor

    public DataImplementation()
    {
      BallsList = new List<IBall>();
      RandomGenerator = new Random();
    }

    #endregion ctor

    #region DataAbstractAPI

    public override void Start(int numberOfBalls, Action<IVector, IBall> upperLayerHandler)
    {
      if (Disposed)
        throw new ObjectDisposedException(nameof(DataImplementation));
      if (upperLayerHandler == null)
        throw new ArgumentNullException(nameof(upperLayerHandler));

      for (int i = 0; i < numberOfBalls; i++)
      {
        Vector startingPosition = new(RandomGenerator.Next(100, 400 - 100), RandomGenerator.Next(100, 400 - 100));
        Vector startingVelocity = new((RandomGenerator.NextDouble() - 0.5) * 10, (RandomGenerator.NextDouble() - 0.5) * 10);
        IBall newBall = new Ball(startingPosition, startingVelocity);
        BallsList.Add(newBall);
        upperLayerHandler(startingPosition, newBall);
      }
    }

    #endregion DataAbstractAPI

    #region IDisposable

    protected virtual void Dispose(bool disposing)
    {
      if (!Disposed)
      {
        if (disposing)
        {
          BallsList.Clear();
        }
        Disposed = true;
      }
      else
      {
        throw new ObjectDisposedException(nameof(DataImplementation));
      }
    }

    public override void Dispose()
    {
      Dispose(disposing: true);
      GC.SuppressFinalize(this);
    }

    #endregion IDisposable

    #region private

    private bool Disposed = false;
    private readonly Random RandomGenerator;
    private readonly List<IBall> BallsList;

    #endregion private

    #region TestingInfrastructure

    [Conditional("DEBUG")]
    internal void CheckBallsList(Action<IEnumerable<IBall>> returnBallsList)
    {
      returnBallsList(BallsList);
    }

    [Conditional("DEBUG")]
    internal void CheckNumberOfBalls(Action<int> returnNumberOfBalls)
    {
      returnNumberOfBalls(BallsList.Count);
    }

    [Conditional("DEBUG")]
    internal void CheckObjectDisposed(Action<bool> returnInstanceDisposed)
    {
      returnInstanceDisposed(Disposed);
    }

    #endregion TestingInfrastructure
  }
}
