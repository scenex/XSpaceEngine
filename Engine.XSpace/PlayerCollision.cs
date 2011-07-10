using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XSpace
{
    public class PlayerCollision
    {
        #region Fields

        private bool topLeftCornerDirectionLeft;
        private bool topLeftCornerDirectionUp;

        private bool topRightCornerDirectionRight;
        private bool topRightCornerDirectionUp;

        private bool bottomLeftCornerDirectionLeft;
        private bool bottomLeftCornerDirectionDown;

        private bool bottomRightCornerDirectionRight;
        private bool bottomRightCornerDirectionDown;

        #endregion

        #region Constructor

        public PlayerCollision()
        {
            topLeftCornerDirectionLeft = false;
            topLeftCornerDirectionUp = false;

            topRightCornerDirectionRight = false;
            topRightCornerDirectionUp = false;

            bottomLeftCornerDirectionLeft = false;
            bottomLeftCornerDirectionDown = false;

            bottomRightCornerDirectionRight = false;
            bottomRightCornerDirectionDown = false;
        }

        #endregion

        #region Properties

        public bool BottomLeftCornerDirectionLeft
        {
            get { return bottomLeftCornerDirectionLeft; }
            set { bottomLeftCornerDirectionLeft = value; }
        }

        public bool BottomRightCornerDirectionRight
        {
            get { return bottomRightCornerDirectionRight; }
            set { bottomRightCornerDirectionRight = value; }
        }

        public bool TopLeftCornerDirectionUp
        {
            get { return topLeftCornerDirectionUp; }
            set { topLeftCornerDirectionUp = value; }
        }

        public bool TopRightCornerDirectionUp
        {
            get { return topRightCornerDirectionUp; }
            set { topRightCornerDirectionUp = value; }
        }

        public bool BottomLeftCornerDirectionDown
        {
            get { return bottomLeftCornerDirectionDown; }
            set { bottomLeftCornerDirectionDown = value; }
        }

        public bool BottomRightCornerDirectionDown
        {
            get { return bottomRightCornerDirectionDown; }
            set { bottomRightCornerDirectionDown = value; }
        }

        public bool TopLeftCornerDirectionLeft
        {
            get { return topLeftCornerDirectionLeft; }
            set { topLeftCornerDirectionLeft = value; }
        }

        public bool TopRightCornerDirectionRight
        {
            get { return topRightCornerDirectionRight; }
            set { topRightCornerDirectionRight = value; }
        }

        #endregion

        #region Methods
        
        public bool MoveLeftPossible()
        {
            if (!this.BottomLeftCornerDirectionLeft && !this.TopLeftCornerDirectionLeft)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool MoveRightPossible()
        {
            if (!this.BottomRightCornerDirectionRight && !this.TopRightCornerDirectionRight)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool MoveUpPossible()
        {
            if (!this.TopLeftCornerDirectionUp && !this.TopRightCornerDirectionUp)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool MoveDownPossible()
        {
            if (!this.BottomRightCornerDirectionDown && !this.BottomLeftCornerDirectionDown)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            topLeftCornerDirectionLeft = false;
            topLeftCornerDirectionUp = false;

            topRightCornerDirectionRight = false;
            topRightCornerDirectionUp = false;

            bottomLeftCornerDirectionLeft = false;
            bottomLeftCornerDirectionDown = false;

            bottomRightCornerDirectionRight = false;
            bottomRightCornerDirectionDown = false;
        }

        #endregion
    }
}
