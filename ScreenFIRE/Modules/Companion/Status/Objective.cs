namespace ScreenFIRE.Modules.Companion.Status {

    //!? TODO:
    //? - Add ObjectiveResult enum (ok / cancel / yes (/to all) / no(/to all) / abort / etc..)
    //? - Add GUI with dynamic data support

    internal enum ObjectiveLevel {
        Question = -1,
        Information = 0,
        Warning = 1,
        Error = 10,
        Fatal = 100
    }
    internal class Objective {


        /// <summary> Last recorded <see cref="ObjectiveResult"/> </summary>
        internal static Objective LastObjective;


        private ObjectiveLevel _Level;
        /// <summary> Objective level
        /// <list type="bullet">
        /// <item> <see cref="ObjectiveLevel.Question"/> = -1 </item>
        /// <item> <see cref="ObjectiveLevel.Information"/> = 0 </item>
        /// <item> <see cref="ObjectiveLevel.Warning"/> = 1 </item>
        /// <item> <see cref="ObjectiveLevel.Error"/> = 10 </item>
        /// <item> <see cref="ObjectiveLevel.Fatal"/> = 100 </item>
        /// </list></summary>
        internal ObjectiveLevel Level {
            get => _Level;
            set { _Level = value; UpdateLastObjective(); }
        }
        private string _Code;
        /// <summary> Specific code represending the current <see cref="Objective"/> </summary>
        internal string Code {
            get => _Code;
            set { _Code = value; UpdateLastObjective(); }
        }
        private string _Description;
        /// <summary> <see cref="Objective"/> Description </summary>
        internal string Description {
            get => _Description;
            set { _Description = value; UpdateLastObjective(); }
        }

        private bool _Silent;
        /// <summary> Notification status: Determines whether the <see cref="Objective"/> will avoid using GUI </summary>
        internal bool Silent {
            get => _Silent;
            set { _Silent = value; UpdateLastObjective(); }
        }

        private void UpdateLastObjective() {
            Common.Cache.ObjectiveHistory.Remove(LastObjective);
            LastObjective = this;
        }

        private void Init(ObjectiveLevel level, string code, string description, bool silent = false) {
            Level = level;
            Code = code;
            Description = description;
            Silent = silent;

            LastObjective = this;

            Common.Cache.ObjectiveHistory ??= new();
            Common.Cache.ObjectiveHistory.Add(this);
            Common.Cache.Save();
        }

        internal Objective(ObjectiveLevel level, string code, bool silent = false) {
            Init(level, code, string.Empty, silent);
        }
        internal Objective(ObjectiveLevel level, string code, string description, bool silent = false) {
            Init(level, code, description, silent);
        }
    }
}