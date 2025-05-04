//____________________________________________________________________________________________________________________________________
//
//  Copyright (C) 2024, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community by pressing the `Watch` button and get started commenting using the discussion panel at
//
//  https://github.com/mpostol/TP/discussions/182
//
//_____________________________________________________________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TP.ConcurrentProgramming.BusinessLogic;
using TP.ConcurrentProgramming.Data;

namespace TP.ConcurrentProgramming.BusinessLogic.Test
{
  [TestClass]
  public class BusinessLogicImplementationUnitTest
  {
    [TestMethod]
    public void ConstructorTestMethod()
    {
      using BusinessLogicImplementation newInstance = new(new DataLayerConstructorFixture());
      bool newInstanceDisposed = true;
      newInstance.CheckObjectDisposed(x => newInstanceDisposed = x);
      Assert.IsFalse(newInstanceDisposed);
    }

    [TestMethod]
    public void DisposeTestMethod()
    {
      DataLayerDisposeFixture dataLayerFixture = new();
      BusinessLogicImplementation newInstance = new(dataLayerFixture);

      Assert.IsFalse(dataLayerFixture.Disposed);

      bool newInstanceDisposed = true;
      newInstance.CheckObjectDisposed(x => newInstanceDisposed = x);
      Assert.IsFalse(newInstanceDisposed);

      newInstance.Dispose();
      newInstance.CheckObjectDisposed(x => newInstanceDisposed = x);
      Assert.IsTrue(newInstanceDisposed);

      Assert.ThrowsException<ObjectDisposedException>(() => newInstance.Dispose());
      Assert.ThrowsException<ObjectDisposedException>(() => newInstance.Start(0, (_, _) => { }));
      Assert.IsTrue(dataLayerFixture.Disposed);
    }

    [TestMethod]
    public void StartTestMethod()
    {
      DataLayerStartFixture dataLayerFixture = new();
      using BusinessLogicImplementation newInstance = new(dataLayerFixture);
      int callbackCount = 0;
      int expectedBallCount = 10;

      newInstance.Start(expectedBallCount, (pos, ball) =>
      {
        callbackCount++;
        Assert.IsNotNull(pos);
        Assert.IsNotNull(ball);
      });

      Assert.AreEqual(1, callbackCount);
      Assert.IsTrue(dataLayerFixture.StartCalled);
      Assert.AreEqual(expectedBallCount, dataLayerFixture.NumberOfBallsCreated);
    }

    #region Fixtures

    private class DataLayerConstructorFixture : TP.ConcurrentProgramming.Data.DataAbstractAPI
    {
      public override void Dispose() { }

      public override void Start(int numberOfBalls, Action<TP.ConcurrentProgramming.Data.IVector, TP.ConcurrentProgramming.Data.IBall> upperLayerHandler)
      {
        throw new NotImplementedException();
      }
    }

    private class DataLayerDisposeFixture : TP.ConcurrentProgramming.Data.DataAbstractAPI
    {
      internal bool Disposed = false;

      public override void Dispose() => Disposed = true;

      public override void Start(int numberOfBalls, Action<TP.ConcurrentProgramming.Data.IVector, TP.ConcurrentProgramming.Data.IBall> upperLayerHandler)
      {
        throw new NotImplementedException();
      }
    }

    private class DataLayerStartFixture : TP.ConcurrentProgramming.Data.DataAbstractAPI
    {
      internal bool StartCalled = false;
      internal int NumberOfBallsCreated = -1;

      public override void Dispose() { }

      public override void Start(int numberOfBalls, Action<TP.ConcurrentProgramming.Data.IVector, TP.ConcurrentProgramming.Data.IBall> upperLayerHandler)
      {
        StartCalled = true;
        NumberOfBallsCreated = numberOfBalls;
        upperLayerHandler(new DataVectorFixture(), new DataBallFixture());
      }

      private record DataVectorFixture : TP.ConcurrentProgramming.Data.IVector
      {
        public double x { get; init; } = 5.0;
        public double y { get; init; } = 10.0;
      }

      private class DataBallFixture : TP.ConcurrentProgramming.Data.IBall
      {
        public TP.ConcurrentProgramming.Data.IVector Velocity { get; set; } = new TP.ConcurrentProgramming.Data.Vector(1.0, 1.0);
        public TP.ConcurrentProgramming.Data.IVector Position { get; private set; } = new TP.ConcurrentProgramming.Data.Vector(0.0, 0.0);

        public event EventHandler<TP.ConcurrentProgramming.Data.IVector>? NewPositionNotification;

        public void UpdatePosition(TP.ConcurrentProgramming.Data.Vector newPosition)
        {
          Position = newPosition;
          NewPositionNotification?.Invoke(this, newPosition);
        }
      }
    }

    #endregion Fixtures
  }
}
