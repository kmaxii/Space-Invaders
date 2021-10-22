using System;

namespace Space_Invaders.MenuPages.OptionsPage
{
    public class Option
    {
        private readonly Action<Scene, string> _action;
        private readonly string _stringInAction;

        public string Text { get; }

        public Option(string text, Action<Scene, string> action, string stringInAction)
        {
            this.Text = text;
            _action = action;
            this._stringInAction = stringInAction;
        }

        public void InvokeAction(Scene scene)
        {
            _action.Invoke(scene, _stringInAction);
        }
    }
}