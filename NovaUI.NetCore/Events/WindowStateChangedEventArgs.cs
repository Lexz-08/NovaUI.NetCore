namespace NovaUI.NetCore.Events
{
    public class WindowStateChangedEventArgs(FormWindowState PreviousState, FormWindowState CurrentState) : EventArgs
    {
        private readonly FormWindowState prevState = PreviousState;
        private readonly FormWindowState currState = CurrentState;

        public FormWindowState PreviousState => prevState;

        public FormWindowState CurrentState => currState;
    }
}
