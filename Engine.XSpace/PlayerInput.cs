using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XSpace
{
    public class PlayerInput
    {
        private bool up;
        private bool down;
        private bool left;
        private bool right;

        private bool isInputToProcess;

        public PlayerInput()
        {
            this.up = false;
            this.down = false;
            this.left = false;
            this.right = false;
            this.isInputToProcess = false;
        }

        public void Reset()
        {
            this.up = false;
            this.down = false;
            this.left = false;
            this.right = false;
            this.isInputToProcess = false;
        }

        public bool Up
        {
            get { return up; }
            set
            {
                up = value;
                isInputToProcess = true;
            }
        }

        public bool Down
        {
            get { return down; }
            set
            {
                down = value;
                isInputToProcess = true;
            }
        }

        public bool Left
        {
            get { return left; }
            set
            {
                left = value;
                isInputToProcess = true;
            }
        }

        public bool Right
        {
            get { return right; }
            set
            {
                right = value;
                isInputToProcess = true;
            }
        }

        public bool IsInputToProcess
        {
            get { return isInputToProcess; }
        }
    }
}
