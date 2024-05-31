public class CheckBoxGroupManager
{
    private List<CheckBox> checkBoxes;
    private Action[] actions;
    public Action CurrentAction { get; private set; }

    public CheckBoxGroupManager(Action[] actions, params CheckBox[] checkBoxGroup)
    {
        this.checkBoxes = new List<CheckBox>(checkBoxGroup);
        this.actions = actions;
        int index = 0;
        foreach (CheckBox checkBox in checkBoxes)
        {
            int capture = index;
            checkBox.CheckedChanged += (sender, e) =>
            {
                CheckBox changedCheckBox = sender as CheckBox;
                if (changedCheckBox.Checked)
                {
                    ExecuteOnlyOne(capture);
                    CurrentAction = actions[capture]; 
                }
                else
                {
                    if (CurrentAction == actions[capture])
                    {
                        CurrentAction = null; 
                    }
                }
            };
            index++;
        }
    }

    private void ExecuteOnlyOne(int index)
    {
        for (int i = 0; i < checkBoxes.Count; i++)
        {
            if (i != index)
            {
                checkBoxes[i].Checked = false;
            }
        }
    }
}