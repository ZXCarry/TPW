//____________________________________________________________________________________________________________________________________
//
//  Copyright (C) 2024, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community by pressing the `Watch` button and get started commenting using the discussion panel at
//
//  https://github.com/mpostol/TP/discussions/182
//
//_____________________________________________________________________________________________________________________________________

namespace TP.ConcurrentProgramming.BusinessLogic.Test
{
  [TestClass]
  public class BallUnitTest
  {
    [TestMethod]
    public void MoveTestMethod()
    {
      DataBallFixture dataBallFixture = new();
      Ball newInstance = new(dataBallFixture);
      int numberOfCallBackCalled = 0;

      newInstance.NewPositionNotification += (sender, position) =>
      {
        Assert.IsNotNull(sender);
        Assert.IsNotNull(position);
        numberOfCallBackCalled++;
      };

      dataBallFixture.SimulateMove();
      Assert.AreEqual(1, numberOfCallBackCalled);
    }

        #region testing instrumentation

        private class DataBallFixture : Data.IBall
        {
            public DataBallFixture()
            {
                Velocity = new Data.Vector(1.0, 1.0);
                Position = new Data.Vector(0.0, 0.0);
            }

            public Data.IVector Velocity { get; set; }
            public Data.IVector Position { get; private set; }

            public event EventHandler<Data.IVector>? NewPositionNotification;

            public void UpdatePosition(Data.Vector newPosition)
            {
                Position = newPosition;
                NewPositionNotification?.Invoke(this, newPosition);
            }

            internal void SimulateMove()
            {
                double newX = Position.x + Velocity.x;
                double newY = Position.y + Velocity.y;
                UpdatePosition(new Data.Vector(newX, newY));
            }
        }

        private class VectorFixture : Data.IVector
    {
      public VectorFixture(double X, double Y)
      {
        x = X;
        y = Y;
      }

      public double x { get; init; }
      public double y { get; init; }
    }

    #endregion testing instrumentation
  }
}
