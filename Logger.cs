﻿using System;

using PCLExt.Input;

namespace PokeD.SCON
{
    /// <summary>
    /// Message Log Type
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// General Log Type.
        /// </summary>
        Info,

        /// <summary>
        /// Chat Log Type.
        /// </summary>
        Chat,

        /// <summary>
        /// Server Chat Log Type.
        /// </summary>
        Event,

        /// <summary>
        /// Trade Log Type.
        /// </summary>
        Trade,

        /// <summary>
        /// PvP Log Type.
        /// </summary>
        PvP,

        /// <summary>
        /// Command Log Type.
        /// </summary>
        Command,

        /// <summary>
        /// Should be reported.
        /// </summary>
        Error,

        /// <summary>
        /// Error Log Type.
        /// </summary>
        Warning,

        /// <summary>
        /// Debug Log Type.
        /// </summary>
        Debug,
    }

    public static class Logger
    {
        public static void Log(LogType type, string message) => Input.LogWriteLine(DateTime.Now, $"[{type}]: {message}");

        public static void LogChatMessage(string player, string message) => Input.LogWriteLine(DateTime.Now, $"<{player}>: {message}");
    }
}