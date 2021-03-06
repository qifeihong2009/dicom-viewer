﻿using Entities;
using System.Windows;

namespace Viewing
{
    public class RotateInteractor : IMouseInteractor
    {
        private readonly Space _space;
        private bool _isMouseDown;
        private Matrix _beginMatrix;
        private Point _beginPosition;
        private Vector3 _centerPosition;

        public RotateInteractor(Space space)
        {
            _space = space;
        }

        public void MouseDown(Point position, Viewport viewport)
        {
            _isMouseDown = true;
            _beginMatrix = _space.TransformationToParent;
            _beginPosition = position;
            _centerPosition = _space.TransformationToParent.Translation();
        }

        public bool MouseMove(Point position, Viewport viewport)
        {
            if (!_isMouseDown) { return false; }

            var delta = position - _beginPosition;
            var angleX = -Double.DegreesToRadians((delta.X / 4));
            var angleY = -Double.DegreesToRadians((delta.Y / 4));
            var yAxis = _beginMatrix * new Vector3(0, 1, 0) - _beginMatrix * new Vector3(0, 0, 0);
            var xAxis = _beginMatrix * new Vector3(1, 0, 0) - _beginMatrix * new Vector3(0, 0, 0);

            _space.TransformationToParent = 
                Matrix.Translation(_centerPosition) * 
                Matrix.RotationAngleAxis(angleY, xAxis.Normalized()) * 
                Matrix.RotationAngleAxis(angleX, yAxis.Normalized()) * 
                Matrix.Translation(-_centerPosition) * 
                _beginMatrix;

            _beginMatrix = _space.TransformationToParent;
            _beginPosition = position;

            return true;
        }

        public void MouseUp(Point position, Viewport viewport)
        {
            _isMouseDown = false;
        }
    }
}
