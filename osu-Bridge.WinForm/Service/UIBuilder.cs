using osu_Bridge.Core.Attributes;
using osu_Bridge.Core.Attributes.Lazer;
using System.Reflection;

namespace osu_Bridge.WinForm.Service;

internal static class UIBuilder
{
    private static readonly Font TitleFont = new("Yu Gothic UI", 11F, FontStyle.Bold);

    internal static void BuildUI(Control parent, object? target, bool isLazerMode)
    {
        parent.Controls.Clear();
        if (target == null) return;

        int y = 5;
        var type = target.GetType();

        foreach (var prop in type.GetProperties())
        {
            if (prop.GetCustomAttributes(typeof(UIFieldAttribute), false).FirstOrDefault() is not UIFieldAttribute attr)
                continue;

            if (isLazerMode && Attribute.IsDefined(prop, typeof(LazerNotSupported)))
                continue;

            if (prop.GetCustomAttributes(typeof(SpaceAttribute), false).FirstOrDefault() is SpaceAttribute spaceAttr)
                y += spaceAttr.Space;

            if (prop.GetCustomAttributes(typeof(TitleAttribute), false).FirstOrDefault() is TitleAttribute titleAttr)
            {
                Label titleLabel = new()
                {
                    Font = TitleFont,
                    Text = titleAttr.Title,
                    Location = new Point(10, y),
                    AutoSize = true
                };
                parent.Controls.Add(titleLabel);

                y += TitleFont.Height + 5;

                Label titleLineLabel = new()
                {
                    BorderStyle = BorderStyle.FixedSingle,
                    Location = new Point(10, y),
                    Size = new Size(parent.Width - 30, 1)
                };
                parent.Controls.Add(titleLineLabel);

                y += 10;
            }

            Label label = new()
            {
                Text = attr.Label != "" ? attr.Label : prop.Name,
                Location = new Point(10, y + 3),
                AutoSize = true
            };

            parent.Controls.Add(label);

            Control input = GenerateUIField(parent, target, prop, y);
            parent.Controls.Add(input);

            y += 30;
        }
    }

    private static Control GenerateUIField(Control parentControl, object targetbject, PropertyInfo property, int currentY)
    {
        Control input;

        if (property.PropertyType == typeof(bool))
        {
            input = new CheckBox()
            {
                Checked = (bool)property.GetValue(targetbject)!,
                Location = new Point(150, currentY)
            };
            ((CheckBox)input).CheckedChanged += (s, e) => OnCheckboxPropertyChanged((CheckBox)input, property, targetbject);
        }
        else if (property.GetCustomAttributes(typeof(ChoiceAttribute), false).FirstOrDefault() is ChoiceAttribute choiceAttr)
        {
            input = new ComboBox()
            {
                Location = new Point(150, currentY),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = parentControl.Width - 180
            };
            ((ComboBox)input).Items.AddRange(choiceAttr.Choices);

            if (((ComboBox)input).Items.IndexOf(property.GetValue(targetbject)) != -1) ((ComboBox)input).SelectedIndex = ((ComboBox)input).Items.IndexOf(property.GetValue(targetbject));
            else if (((ComboBox)input).Items.Count > 0) ((ComboBox)input).SelectedIndex = 0;

            ((ComboBox)input).SelectedIndexChanged += (s, e) => OnComboBoxPropertyChanged((ComboBox)input, property, targetbject);
        }
        else
        {
            input = new TextBox()
            {
                Text = property.GetValue(targetbject)?.ToString(),
                Location = new Point(150, currentY),
                Width = parentControl.Width - 180
            };

            if (Attribute.IsDefined(property, typeof(ConfidentialAttribute))) ((TextBox)input).PasswordChar = '*';
            if (property.GetCustomAttributes(typeof(PlaceHolderAttribute), false).FirstOrDefault() is PlaceHolderAttribute placeHolderAttribute)
            {
                ((TextBox)input).PlaceholderText = placeHolderAttribute.PlaceHolderText;
            }

            ((TextBox)input).TextChanged += (s, e) => OnTextPropertyChanged((TextBox)input, property, targetbject);
        }

        return input;
    }

    #region イベントハンドラー
    private static void OnCheckboxPropertyChanged(CheckBox checkBox, PropertyInfo property, object target)
    {
        property.SetValue(target, checkBox.Checked);
    }

    private static void OnTextPropertyChanged(TextBox textBox, PropertyInfo property, object target)
    {
        try
        {
            if (string.IsNullOrEmpty(textBox.Text)) return;

            object value = Convert.ChangeType(textBox.Text, property.PropertyType);
            property.SetValue(target, value);
        }
        catch
        {
            // Ignored
        }
    }

    private static void OnComboBoxPropertyChanged(ComboBox comboBox, PropertyInfo property, object target)
    {
        try
        {
            if (string.IsNullOrEmpty(comboBox.Text)) return;

            object value = Convert.ChangeType(comboBox.Text, property.PropertyType);
            property.SetValue(target, value);
        }
        catch
        {
            // Ignored
        }
    }
    #endregion
}
