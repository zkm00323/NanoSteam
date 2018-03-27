using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCtrl : MonoBehaviour {
    public enum GAME_CONTROLLER { PC, JOY, NULL }
    public enum INPUT_TYPE { BUTTON, AXIS, NULL }

    public class InputInfo{
        public readonly string InputName;
        public readonly GAME_CONTROLLER controller;
        public readonly INPUT_TYPE type;

        public InputInfo(){
            InputName = "unknow";
            controller = GAME_CONTROLLER.NULL;
            type = INPUT_TYPE.NULL;
        }
        public InputInfo(string nameIn, GAME_CONTROLLER ctrlerIn, INPUT_TYPE typeIn){
            InputName = nameIn;
            controller = ctrlerIn;
            type = typeIn;
        }
    }

    public class Get{
        public enum GAME_BUTTON {
            LEFT, RIGHT, UP, DOWN, JUMP, ATTACK, DODGE, CREATE
        }
        public enum BUTTON_ACTION {
            DOWN,
            HOLD,
            UP
        }

        public static bool Button(GAME_BUTTON input, BUTTON_ACTION action) {
            string button = null;
            switch (input) {
                case GAME_BUTTON.LEFT:
                    button = CtrlButtonName.PC.LEFT.InputName;
                    break;
                case GAME_BUTTON.RIGHT:
                    button = CtrlButtonName.PC.RIGHT.InputName;
                    break;
                case GAME_BUTTON.UP:
                    button = CtrlButtonName.PC.UP.InputName;
                    break;
                case GAME_BUTTON.DOWN:
                    button = CtrlButtonName.PC.DOWN.InputName;
                    break;
                case GAME_BUTTON.JUMP:
                    button = CtrlButtonName.PC.A.InputName;
                    break;
                case GAME_BUTTON.ATTACK:
                    button = CtrlButtonName.PC.S.InputName;
                    break;
                case GAME_BUTTON.DODGE:
                    button = CtrlButtonName.PC.D.InputName;
                    break;
                case GAME_BUTTON.CREATE:
                    button = CtrlButtonName.PC.SPACE.InputName;
                    break;
            }
            switch (action) {
                case BUTTON_ACTION.DOWN:
                    return Input.GetButtonDown(button);
                case BUTTON_ACTION.HOLD:
                    return Input.GetButton(button);
                case BUTTON_ACTION.UP:
                    return Input.GetButtonUp(button);
            }
            return false;
        }

        public enum GAME_AXIS {
            MOVE, UPDN
        }

        public static float Axis(GAME_AXIS input) {
            switch (input) {
                case GAME_AXIS.MOVE:
                    bool left = Button(GAME_BUTTON.LEFT, BUTTON_ACTION.HOLD);
                    bool right = Button(GAME_BUTTON.RIGHT, BUTTON_ACTION.HOLD);
                    return left && right ? 0 : left ? -1 : right ? 1 : 0;

                case GAME_AXIS.UPDN:
                    bool up = Button(GAME_BUTTON.UP, BUTTON_ACTION.HOLD);
                    bool down = Button(GAME_BUTTON.DOWN, BUTTON_ACTION.HOLD);
                    return up && down ? 0 : down ? -1 : up ? 1 : 0;
            }
            return 0;
        }

        public static float Axis(GAME_AXIS input,float Senctive){
            float value = Axis(input);
            return value < -Senctive || value > Senctive ? value : 0;
        }

        public static int Axis10(GAME_AXIS input,float Senctive){
            float value = Axis(input);
            if(value<-Senctive)
                return -1;
            if(value>Senctive)
                return 1;
            return 0;
        }

        public static int Axis10(GAME_AXIS input){
            return Axis10(input,0.2f);
        }
    }

    public class CtrlButtonName {
        public class PC {
            public static readonly InputInfo UP = new InputInfo("Up", GAME_CONTROLLER.PC, INPUT_TYPE.BUTTON);
            public static readonly InputInfo DOWN = new InputInfo("Down", GAME_CONTROLLER.PC, INPUT_TYPE.BUTTON);
            public static readonly InputInfo LEFT = new InputInfo("Left", GAME_CONTROLLER.PC, INPUT_TYPE.BUTTON);
            public static readonly InputInfo RIGHT = new InputInfo("Right", GAME_CONTROLLER.PC, INPUT_TYPE.BUTTON);

            public static readonly InputInfo A = new InputInfo("A", GAME_CONTROLLER.PC, INPUT_TYPE.BUTTON);
            public static readonly InputInfo S = new InputInfo("S", GAME_CONTROLLER.PC, INPUT_TYPE.BUTTON);
            public static readonly InputInfo D = new InputInfo("D", GAME_CONTROLLER.PC, INPUT_TYPE.BUTTON);
            public static readonly InputInfo SPACE = new InputInfo("Space", GAME_CONTROLLER.PC, INPUT_TYPE.BUTTON);

            public static readonly InputInfo LEFTMOUSE = new InputInfo("Left Mouse", GAME_CONTROLLER.PC, INPUT_TYPE.BUTTON);
            public static readonly InputInfo RIGHTMOUSE = new InputInfo("Right Mouse", GAME_CONTROLLER.PC, INPUT_TYPE.BUTTON);
            public static readonly InputInfo MIDDLEMOUSE = new InputInfo("Middle Mouse", GAME_CONTROLLER.PC, INPUT_TYPE.BUTTON);
            public static readonly InputInfo MOUSEWHEEL = new InputInfo("Mouse Wheel", GAME_CONTROLLER.PC, INPUT_TYPE.AXIS);


            public static readonly InputInfo[] PCInputs = { UP, DOWN, LEFT, RIGHT, A, S, D, SPACE, LEFTMOUSE, RIGHTMOUSE, MIDDLEMOUSE, MOUSEWHEEL};

        }
        /*
        public class Joy {
            public static readonly InputInfo A = new InputInfo("Joy A", GAME_CONTROLLER.JOY, INPUT_TYPE.BUTTON);
            public static readonly InputInfo B = new InputInfo("Joy B", GAME_CONTROLLER.JOY, INPUT_TYPE.BUTTON);
            public static readonly InputInfo X = new InputInfo("Joy X", GAME_CONTROLLER.JOY, INPUT_TYPE.BUTTON);
            public static readonly InputInfo Y = new InputInfo("Joy Y", GAME_CONTROLLER.JOY, INPUT_TYPE.BUTTON);

            public static readonly InputInfo LB = new InputInfo("Joy LB", GAME_CONTROLLER.JOY, INPUT_TYPE.BUTTON);
            public static readonly InputInfo LT = new InputInfo("Joy LT", GAME_CONTROLLER.JOY, INPUT_TYPE.AXIS);

            public static readonly InputInfo RB = new InputInfo("Joy RB", GAME_CONTROLLER.JOY, INPUT_TYPE.BUTTON);
            public static readonly InputInfo RT = new InputInfo("Joy RT", GAME_CONTROLLER.JOY, INPUT_TYPE.AXIS);

            public static readonly InputInfo START = new InputInfo("Joy Start", GAME_CONTROLLER.JOY, INPUT_TYPE.BUTTON);
            public static readonly InputInfo BACK = new InputInfo("Joy Back", GAME_CONTROLLER.JOY, INPUT_TYPE.BUTTON);

            public static readonly InputInfo RIGHT_X = new InputInfo("Joy Right X", GAME_CONTROLLER.JOY, INPUT_TYPE.AXIS);
            public static readonly InputInfo RIGHT_BUTTON = new InputInfo("Joy Right", GAME_CONTROLLER.JOY, INPUT_TYPE.BUTTON);
            public static readonly InputInfo RIGHT_Y = new InputInfo("Joy Right Y", GAME_CONTROLLER.JOY, INPUT_TYPE.AXIS);

            public static readonly InputInfo DPAD_X = new InputInfo("Joy Dpad X", GAME_CONTROLLER.JOY, INPUT_TYPE.AXIS);
            public static readonly InputInfo DPAD_Y = new InputInfo("Joy Dpad Y", GAME_CONTROLLER.JOY, INPUT_TYPE.AXIS);

            public static readonly InputInfo LEFT_X = new InputInfo("Joy Left X", GAME_CONTROLLER.JOY, INPUT_TYPE.AXIS);
            public static readonly InputInfo LEFT_BUTTON = new InputInfo("Joy Left", GAME_CONTROLLER.JOY, INPUT_TYPE.BUTTON);
            public static readonly InputInfo LEFT_Y = new InputInfo("Joy Left Y", GAME_CONTROLLER.JOY, INPUT_TYPE.AXIS);
            public static readonly InputInfo[] JoyInputs = { A, B, X, Y, LB, LT, RB, RT, START, BACK, RIGHT_X, RIGHT_BUTTON, RIGHT_Y, DPAD_X, DPAD_Y, LEFT_X, LEFT_BUTTON, LEFT_Y };
        }
        */
        public static readonly InputInfo[][] AllInput = { PC.PCInputs};//, Joy.JoyInputs };
    }
}
