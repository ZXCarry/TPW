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
  public class VectorUnitTest
  {
    [TestMethod]
    public void ConstructorTestMethod()
    {
      Random random = new();
      double expectedX = random.NextDouble();
      double expectedY = random.NextDouble();

      Vector vector = new(expectedX, expectedY);

      Assert.AreEqual(expectedX, vector.x, 0.000001);
      Assert.AreEqual(expectedY, vector.y, 0.000001);
    }
  }
}
