//____________________________________________________________________________________________________________________________________
//
//  Copyright (C) 2024, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community by pressing the `Watch` button and get started commenting using the discussion panel at
//
//  https://github.com/mpostol/TP/discussions/182
//
//_____________________________________________________________________________________________________________________________________

namespace TP.ConcurrentProgramming.BusinessLogic
{
  internal class Ball : IBall
  {

    private readonly Data.IBall ball;

    public Ball(Data.IBall ball)
    {
    this.ball = ball;
    ball.NewPositionNotification += RaisePositionChangeEvent;
    }

    #region IBall

    public event EventHandler<IPosition>? NewPositionNotification;

    #endregion IBall

    #region private

    private void RaisePositionChangeEvent(object? sender, Data.IVector e)
    {
      NewPositionNotification?.Invoke(this, new Position(e.x, e.y));
    }

    #endregion private

    public void Move(float diameter, float boardWidth, float boardHeight, float borderThickness)
    {
    var pos = ball.Position;
    var vel = ball.Velocity;

    var newX = pos.x + vel.x;
    var newY = pos.y + vel.y;

    if (newX <= 0 || newX >= boardWidth - diameter - borderThickness)
        vel = new Data.Vector(-vel.x, vel.y);

    if (newY <= 0 || newY >= boardHeight - diameter - borderThickness)
        vel = new Data.Vector(vel.x, -vel.y);

    ball.Velocity = vel;
    ball.UpdatePosition(new Data.Vector(newX, newY));
    }
  }
}