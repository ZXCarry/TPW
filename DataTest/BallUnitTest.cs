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

namespace TP.ConcurrentProgramming.Data.Test
{
  [TestClass]
  public class BallUnitTest
  {
    [TestMethod]
    public void ConstructorTestMethod()
    {
      Vector initial = new Vector(0.0, 0.0);
      Ball ball = new(initial, initial);

      Assert.AreEqual(initial, ball.Position);
      Assert.AreEqual(initial, ball.Velocity);
    }

    [TestMethod]
    public void UpdatePosition_ShouldTriggerEvent()
    {
      Vector initial = new(10.0, 10.0);
      Ball ball = new(initial, new Vector(0.0, 0.0));

      bool eventCalled = false;
      IVector? received = null;

      ball.NewPositionNotification += (sender, newPos) =>
      {
        eventCalled = true;
        received = newPos;
      };

      Vector updated = new(20.0, 30.0);
      ball.UpdatePosition(updated);

      Assert.IsTrue(eventCalled);
      Assert.AreEqual(updated, received);
      Assert.AreEqual(updated, ball.Position);
    }

    [TestMethod]
    public void Velocity_CanBeChanged()
    {
      Vector initial = new(0, 0);
      Ball ball = new(initial, new Vector(1, 1));

      Vector newVelocity = new(2.5, -3.0);
      ball.Velocity = newVelocity;

      Assert.AreEqual(newVelocity, ball.Velocity);
    }
  }
}
