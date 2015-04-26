/*
	OpenSauceBox: SDK for Xbox User Modding

	See license\Xbox\Xbox for specific license information
*/
using System;
using System.Windows.Forms;
using Microsoft.DirectX.DirectInput;

using Yelo.Debug;
using Yelo.Shared;
using System.Threading;
using System.Drawing;

namespace Yelo.Controller
{
	public partial class XBoxController : Form
	{
		public XBoxController()
		{
			InitializeComponent();

            Text = XBoxIO.XBox.DebugName + " - " + XBoxIO.XBox.DebugIP + " - Yelo Controller v" + Cache.Version;

            input = new Input(this);
            inputState = new InputState();
            previousMousePosition = Cursor.Position;

            ControllerThread = new Thread(ControllerLoop);
            ControllerThread.Start();

            sendTimer.Start();
		}

        private void XBoxController_FormClosing(object sender, FormClosingEventArgs e)
        {
            running = false;
            XBoxIO.XBox.Disconnect();
        }

        private void sendTimer_Tick(object sender, EventArgs e)
        {
            sendTimer.Stop();
            if (XBoxIO.XBox.Connected)
            {
                if (XBoxIO.XBox.Gamepad == null)
                {
                    running = false;
                    MessageBox.Show("Unable To Access Gamepad", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    InputState inputStateCopy = new InputState();
                    inputStateCopy.AssignState(inputState);
                    XBoxIO.XBox.Gamepad.SetState(0, inputStateCopy);
                    inputState = new InputState();
                    sendTimer.Start();
                }
            }
        }

        Input input;
        InputState inputState;
        Point previousMousePosition;
        Thread ControllerThread;
        bool running = false;
        void ControllerLoop()
        {
            if (XBoxIO.FindXBox())
            {
                if (XBoxIO.XBox.Gamepad == null)
                {
                    MessageBox.Show("Unable To Access Gamepad", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    return;
                }
                else
                {
                    XBoxIO.XBox.Gamepad.InitializeControllerHook();
                    XBoxIO.XBox.Gamepad.OverrideControllers(true);
                }
            }
            else
            {
                Application.Exit();
                return;
            }

            running = true;
            while (running)
            {
                AcceptKeyboardInput(input, inputState);
                AcceptMouseInput(input, inputState);
            }
        }

        public void AcceptKeyboardInput(Input input, InputState inputState)
        {
            //gets keyboard input
            KeyboardState Keyboard = input.GetKeyboardState();
            if (Keyboard != null)
            {
                if (Keyboard[Key.UpArrow]) inputState.ThumbLY = short.MaxValue;
                if (Keyboard[Key.LeftArrow]) inputState.ThumbLX = short.MinValue;
                if (Keyboard[Key.DownArrow]) inputState.ThumbLY = short.MinValue;
                if (Keyboard[Key.RightArrow]) inputState.ThumbLX = short.MaxValue;

                if (Keyboard[Key.NumPad8]) inputState.ThumbRY = short.MaxValue;
                if (Keyboard[Key.NumPad4]) inputState.ThumbRX = short.MinValue;
                if (Keyboard[Key.NumPad2]) inputState.ThumbRY = short.MinValue;
                if (Keyboard[Key.NumPad6]) inputState.ThumbRX = short.MaxValue;

                if (Keyboard[Key.NumPad0]) inputState.Buttons |= Buttons.LeftThumb;
                if (Keyboard[Key.NumPad5]) inputState.Buttons |= Buttons.RightThumb;

                if (Keyboard[Key.BackSpace]) inputState.Buttons |= Buttons.Back;
                if (Keyboard[Key.Return]) inputState.Buttons |= Buttons.Start;

                if (Keyboard[Key.W]) inputState.AnalogButtons[(int)AnalogButtons.Y] = 0xFF;
                if (Keyboard[Key.A]) inputState.AnalogButtons[(int)AnalogButtons.X] = 0xFF;
                if (Keyboard[Key.S]) inputState.AnalogButtons[(int)AnalogButtons.A] = 0xFF;
                if (Keyboard[Key.D]) inputState.AnalogButtons[(int)AnalogButtons.B] = 0xFF;

                if (Keyboard[Key.Q]) inputState.AnalogButtons[(int)AnalogButtons.White] = 0xFF;
                if (Keyboard[Key.E]) inputState.AnalogButtons[(int)AnalogButtons.Black] = 0xFF;

                if (Keyboard[Key.I]) inputState.Buttons |= Buttons.Up;
                if (Keyboard[Key.J]) inputState.Buttons |= Buttons.Left;
                if (Keyboard[Key.K]) inputState.Buttons |= Buttons.Down;
                if (Keyboard[Key.L]) inputState.Buttons |= Buttons.Right;

                if (Keyboard[Key.Escape])
                {
                    XBoxIO.XBox.Gamepad.OverrideControllers(false);
                    Application.Exit();
                    running = false;
                }
            }
        }

        short prevX;
        short prevY;

        public void AcceptMouseInput(Input input, InputState inputState)
        {
            MouseState mouseState = input.GetMouseState();
            if (mouseState.GetMouseButtons()[0] > 0) // left click
                inputState.AnalogButtons[(int)AnalogButtons.RightTrigger] = 0xFF;
            if (mouseState.GetMouseButtons()[1] > 0) // right click
                inputState.AnalogButtons[(int)AnalogButtons.LeftTrigger] = 0xFF;
            if (mouseState.GetMouseButtons()[2] > 0) // middle click
                inputState.Buttons |= Buttons.RightThumb;

            Point center = new Point(Location.X + Size.Width / 2, Location.Y + Size.Height / 2);

            inputState.ThumbRX += (short)((MousePosition.X - center.X) * 1000);
            inputState.ThumbRY += (short)(-(MousePosition.Y - center.Y) * 1000);

            Cursor.Position = center;

            previousMousePosition = Cursor.Position;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            inputState.AnalogButtons[(int)AnalogButtons.A] = 0xFF;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            inputState.AnalogButtons[(int)AnalogButtons.B] = 0xFF;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            inputState.AnalogButtons[(int)AnalogButtons.X] = 0xFF;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            inputState.AnalogButtons[(int)AnalogButtons.Y] = 0xFF;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            inputState.Buttons |= Buttons.Up;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            inputState.Buttons |= Buttons.Right;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            inputState.Buttons |= Buttons.Down;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            inputState.Buttons |= Buttons.Left;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            inputState.ThumbLY = short.MaxValue;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            inputState.ThumbLX = short.MaxValue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            inputState.ThumbLY = short.MinValue;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            inputState.ThumbLX = short.MinValue;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            inputState.ThumbRY = short.MaxValue;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            inputState.ThumbRX = short.MaxValue;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            inputState.ThumbRY = short.MinValue;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            inputState.ThumbRX = short.MinValue;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            inputState.AnalogButtons[(int)AnalogButtons.LeftTrigger] = 0xFF;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            inputState.AnalogButtons[(int)AnalogButtons.RightTrigger] = 0xFF;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            inputState.AnalogButtons[(int)AnalogButtons.White] = 0xFF;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            inputState.AnalogButtons[(int)AnalogButtons.Black] = 0xFF;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            inputState.Buttons |= Buttons.Start;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            inputState.Buttons |= Buttons.Back;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            inputState.Buttons |= Buttons.LeftThumb;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            inputState.Buttons |= Buttons.RightThumb;
        }
	};
}