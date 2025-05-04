//____________________________________________________________________________________________________________________________________
//
//  Copyright (C) 2024, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community by pressing the `Watch` button and get started commenting using the discussion panel at
//
//  https://github.com/mpostol/TP/discussions/182
//
//_____________________________________________________________________________________________________________________________________

using System;

namespace TP.ConcurrentProgramming.BusinessLogic
{
  public abstract class BusinessLogicAbstractAPI : IDisposable
  {
    #region Layer Factory

    public static BusinessLogicAbstractAPI GetBusinessLogicLayer()
    {
      return modelInstance.Value;
    }

    #endregion Layer Factory

    #region Layer API

    public static readonly Dimensions DefaultDimensions = new(20.0, 420.0, 400.0);

    public abstract void Start(int numberOfBalls, Action<IPosition, IBall> upperLayerHandler);

    public abstract void Dispose();

    #endregion Layer API

    #region private

    private static Lazy<BusinessLogicAbstractAPI> modelInstance =
        new(() => new BusinessLogicImplementation());

    #endregion private
  }

  /// <summary>
  /// Immutable type representing table dimensions
  /// </summary>
  public record Dimensions(double BallDimension, double TableHeight, double TableWidth);

  public interface IPosition
  {
    double x { get; init; }
    double y { get; init; }
  }

  public interface IBall
  {
    event EventHandler<IPosition> NewPositionNotification;
  }
}
