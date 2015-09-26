#region File Description
#endregion
#region Using Statements
using Microsoft.Xna.Framework;
using RougyMon;
using MiniEngine;
#endregion
namespace GameStateManagementSample
{
    class OptionsMenuScreen : MenuScreen
    {
        #region Fields
        MenuEntry ungulateMenuEntry;
        MenuEntry languageMenuEntry;
        MenuEntry frobnicateMenuEntry;
        MenuEntry elfMenuEntry;
        public static bool MoveArrows = false;
        enum Ungulate
        {
            BactrianCamel,
            Dromedary,
            Llama,
        }
        static Ungulate currentUngulate = Ungulate.Dromedary;
        static string[] languages = { "C#", "French", "Hi Chris" };
        static int currentLanguage = 0;
        //static bool frobnicate = true;
        static int elf = 23;
        #endregion
        #region Initialization
        public OptionsMenuScreen()
            : base("Options")
        {
            ungulateMenuEntry = new MenuEntry(string.Empty);
            languageMenuEntry = new MenuEntry(string.Empty);
            frobnicateMenuEntry = new MenuEntry(string.Empty);
            elfMenuEntry = new MenuEntry(string.Empty);
            SetMenuEntryText();
            MenuEntry back = new MenuEntry("Back");
            ungulateMenuEntry.Selected += UngulateMenuEntrySelected;
            languageMenuEntry.Selected += LanguageMenuEntrySelected;
            frobnicateMenuEntry.Selected += FrobnicateMenuEntrySelected;
            elfMenuEntry.Selected += ElfMenuEntrySelected;
            back.Selected += OnCancel;
            MenuEntries.Add(ungulateMenuEntry);
            MenuEntries.Add(languageMenuEntry);
            MenuEntries.Add(frobnicateMenuEntry);
            MenuEntries.Add(elfMenuEntry);
            MenuEntries.Add(back);
            
        }
        void SetMenuEntryText()
        {
            ungulateMenuEntry.Text = "Preferred ungulate: " + currentUngulate;
            languageMenuEntry.Text = "Language: " + languages[currentLanguage];
            frobnicateMenuEntry.Text = "Movement with: " + (MoveArrows ? "Arrows" : "WASD");
            elfMenuEntry.Text = "elf: " + elf;
        }
        #endregion
        #region Handle Input
        void UngulateMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            currentUngulate++;
            if (currentUngulate > Ungulate.Llama)
                currentUngulate = 0;
            SetMenuEntryText();
        }
        void LanguageMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            currentLanguage = (currentLanguage + 1) % languages.Length;
            SetMenuEntryText();
        }
        void FrobnicateMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            MoveArrows = !MoveArrows;
            SetMenuEntryText();
        }
        void ElfMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            elf++;
            SetMenuEntryText();
        }
        #endregion
    }
}
