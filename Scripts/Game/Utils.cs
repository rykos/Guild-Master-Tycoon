using System;

namespace Utils
{
    ///<summary>Takes care of input, firing provided Actions</summary>
    public class Input
    {
        private static float epsilon = UnityEngine.Mathf.Epsilon;
        private float holdTreshold;

        private float clickedTime = 0;
        public bool isClicked = false;
        private Action click, hold;

        public Input(float holdTreshold, Action click, Action hold)
        {
            this.holdTreshold = holdTreshold;
            this.click = click;
            this.hold = hold;
        }
        public void Tick(float time)
        {
            if (this.isClicked)
            {
                clickedTime += time;
                if (this.clickedTime > this.holdTreshold) { this.isClicked = false; }
            }
            else if (clickedTime > epsilon)
            {
                if (clickedTime > this.holdTreshold)
                {
                    this.hold.Invoke();
                }
                else
                {
                    this.click.Invoke();
                }
                this.clickedTime = 0;
            }
        }
    }
}