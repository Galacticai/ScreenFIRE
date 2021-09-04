using GeekAssistant.Forms;

namespace GeekAssistant.Modules.General {
    internal static class wait {
        public static Wait Wait = null;
        public static void Run(bool Enable) {
            if (Enable) {
                if (Wait != null) //dispose if exists and is not disposed
                    if (!Wait.IsDisposed) Wait.Dispose();
                (Wait = new()).Show(); //! create new ( Always create new() : Don't merge with null check above ) 
            } else {
                if (Wait == null) return; // cancel if no instance saved 
                Wait.UserClosing = false; //unflag or it won't close
                Wait.Close();
            }
        }
    }
}
