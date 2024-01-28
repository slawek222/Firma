﻿using Firma.Helpers;
using Firma.ViewResources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Firma.ViewModels
{
    /// <summary>
    /// This is class for element of side bar
    /// </summary>
    public class CommandViewModel:BaseViewModel
    {
        #region FieldAndProperties

        /// <summary>
        /// This is name of the button in sidebar
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Every button contains Command, that opens tab
        /// </summary>
        public ICommand Command { get; set; }
        public Style ButtonStyle { get; set; }
        #endregion

        #region Konstruktor

        public CommandViewModel(string displayName, ICommand command)
        {
            if (command == null)
                throw new ArgumentNullException("Command");
            this.DisplayName = displayName;
            this.Command = command;
        }

        #endregion
    }
}
