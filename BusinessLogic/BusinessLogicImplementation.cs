using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using TP.ConcurrentProgramming.Data;

namespace TP.ConcurrentProgramming.BusinessLogic
{
    public class BusinessLogicImplementation : BusinessLogicAbstractAPI
    {
        #region ctor

        public BusinessLogicImplementation() : this(null) { }

        internal BusinessLogicImplementation(DataAbstractAPI? underneathLayer)
        {
            layerBellow = underneathLayer ?? DataAbstractAPI.GetDataLayer();
            balls = new List<Ball>();
            updateTimer = new Timer(MoveAllBalls, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(16));
        }

        #endregion ctor

        #region BusinessLogicAbstractAPI

        public override void Start(int numberOfBalls, Action<IPosition, IBall> upperLayerHandler)
        {
            if (Disposed)
                throw new ObjectDisposedException(nameof(BusinessLogicImplementation));
            if (upperLayerHandler == null)
                throw new ArgumentNullException(nameof(upperLayerHandler));

            layerBellow.Start(numberOfBalls, (startingPosition, dataBall) =>
            {
                var logicBall = new Ball(dataBall);
                balls.Add(logicBall);
                upperLayerHandler(new Position(startingPosition.x, startingPosition.y), logicBall);
            });
        }

        public override void Dispose()
        {
            if (Disposed)
                throw new ObjectDisposedException(nameof(BusinessLogicImplementation));
            updateTimer.Dispose();
            layerBellow.Dispose();
            Disposed = true;
        }

        #endregion BusinessLogicAbstractAPI

        #region private

        private readonly DataAbstractAPI layerBellow;
        private readonly List<Ball> balls;
        private readonly Timer updateTimer;
        private bool Disposed = false;

        private void MoveAllBalls(object? state)
        {
            foreach (var ball in balls)
            {
                ball.Move(diameter: 20, boardWidth: 400, boardHeight: 420, borderThickness: 8);
            }
        }

        #endregion private

        #region TestingInfrastructure

        [Conditional("DEBUG")]
        internal void CheckObjectDisposed(Action<bool> returnInstanceDisposed)
        {
            returnInstanceDisposed(Disposed);
        }

        #endregion TestingInfrastructure
    }
}
