using osu_Bridge.Core.Attributes;
using System.Reflection;

namespace osu_Bridge.WinForm.Service;

internal static class UIBuilder
{
    private static readonly Font TitleFont = new Font("Yu Gothic UI", 11F, FontStyle.Bold);

    internal static void BuildUI(Control parent, object? target)
    {
        parent.Controls.Clear();
        if (target == null) return;

        int y = 5;
        var type = target.GetType();

        foreach (var prop in type.GetProperties())
        {
            if (prop.GetCustomAttributes(typeof(UIFieldAttribute), false).FirstOrDefault() is not UIFieldAttribute attr)
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

            Control input;

            if (prop.PropertyType == typeof(bool))
            {
                input = new CheckBox()
                {
                    Checked = (bool)prop.GetValue(target)!,
                    Location = new Point(150, y)
                };
                ((CheckBox)input).CheckedChanged += (s, e) => OnCheckboxPropertyChanged(((CheckBox)input), prop, target);
            }
            else
            {
                input = new TextBox()
                {
                    Text = prop.GetValue(target)?.ToString(),
                    Location = new Point(150, y),
                    Width = parent.Width - 180
                };

                if (Attribute.IsDefined(prop, typeof(ConfidentialAttribute))) ((TextBox)input).PasswordChar = '*';

                ((TextBox)input).TextChanged += (s, e) => OnTextPropertyChanged((TextBox)input, prop, target);
            }

            parent.Controls.Add(input);

            y += 30;
        }
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
    #endregion
}
