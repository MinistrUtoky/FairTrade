using Fair_Trade.GameClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Fair_Trade.Windows.Menu
{
    public partial class GameSettings : Window
    {
        private Popup Resolutions_Popup;
        private Popup Difficulty_Popup;
        public GameSettings()
        {
            Resolutions_Popup = new Popup();
            Difficulty_Popup = new Popup();
            InitializeComponent();
            GameMode.FormatWindow(this);
            ScrollViewer sw = new ScrollViewer();
            sw.Content = Settings_Panel;
            System.Windows.Controls.Image gameName = new System.Windows.Controls.Image();
            Settings_Panel.Children.Add(gameName);
            Dictionary<List<string>, Type> settings = GameMode.GetSettingsDictionary();
            foreach (KeyValuePair<List<string>, Type> s in settings)
            {
                Settings_Panel.Children.Add(SettingsElement(s));
            }
            Button save = new Button();
            save.Content = "Save"; save.Click += Save_Click;
            Settings_Panel.Children.Add(save);
            Button back = new Button();
            back.Content = "Back"; back.Click += Back_To_Main_Menu;
            Settings_Panel.Children.Add(back);
        }

        private void Back_To_Main_Menu(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //StringBuilder sb = new StringBuilder();
            foreach (object grid in Settings_Panel.Children)
            {
                if (grid.GetType() == typeof(Grid))
                {
                    string parameter = ((grid as Grid).Children[0] as TextBlock).Text;
                    foreach (object element in (grid as Grid).Children)
                    {
                        if (element.GetType() != typeof(Popup) && element.GetType() != typeof(TextBlock))
                        {
                            if (element.GetType() == typeof(Button))
                            {
                                if (parameter == "Bots difficulty") GameMode.SetBotDifficulty((element as Button).Content.ToString());
                                if (parameter == "Resolution")
                                {
                                    string[] res = (element as Button).Content.ToString().Split("x");
                                    int width = Int32.Parse(res[0]); int height = Int32.Parse(res[1]);
                                    GameMode.SetResolution(width, height);
                                    GameMode.FormatWindow(this);
                                }
                            }
                            else if (element.GetType() == typeof(CheckBox))
                            {
                                if (parameter == "Full screen")
                                {
                                    if ((element as CheckBox).IsChecked.Value == true)
                                        ((Settings_Panel.Children[1] as Grid).Children[1] as Button).IsEnabled = false;
                                    else
                                        ((Settings_Panel.Children[1] as Grid).Children[1] as Button).IsEnabled = true;
                                    GameMode.SetFullScreenMode((element as CheckBox).IsChecked.Value);
                                    GameMode.FormatWindow(this);
                                    this.Top = 0; this.Left = 0; 
                                }
                                else if (parameter == "Animations")
                                {
                                    GameMode.SetAnimationsOn((element as CheckBox).IsChecked.Value);
                                }
                                else if (parameter == "Chat is blocked")
                                {
                                    GameMode.SetChatMode((element as CheckBox).IsChecked.Value);
                                }
                            }
                            else if (element.GetType() == typeof(Grid))
                            {
                                if (parameter == "Music")
                                    GameMode.SetMusicLevel(Int32.Parse(((element as Grid).Children[1] as TextBlock).Text.ToString()));
                                if (parameter == "SFX")
                                    GameMode.SetSFXLevel(Int32.Parse(((element as Grid).Children[1] as TextBlock).Text.ToString()));
                            }
                        }
                    }
                }
            }
            GameMode.SaveSettings();
        }

        private Grid SettingsElement(KeyValuePair<List<string>, Type> setting)
        {
            Grid g = new Grid();
            ColumnDefinition first = new ColumnDefinition();
            ColumnDefinition second = new ColumnDefinition();
            first.Width = new GridLength(2, GridUnitType.Star);
            second.Width = new GridLength(1, GridUnitType.Star);
            g.ColumnDefinitions.Add(first);
            g.ColumnDefinitions.Add(second);

            TextBlock tb = new TextBlock();
            tb.Text = setting.Key[0];
            tb.Foreground = Brushes.White;
            tb.SetValue(Grid.ColumnProperty, 0);
            g.Children.Add(tb);

            if (setting.Value == typeof(bool))
            {
                CheckBox cb = new CheckBox();
                if (setting.Key[1] == "True") cb.IsChecked = true;
                else cb.IsChecked = false;
                cb.SetValue(Grid.ColumnProperty, 1);
                g.Children.Add(cb);
            }
            else if (setting.Value == typeof(int))
            {
                Grid gg = new Grid();
                gg.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(6, GridUnitType.Star) });
                gg.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
                gg.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                Slider s = new Slider
                {
                    Minimum = 0,
                    Maximum = 100,
                    SelectionStart = 0,
                    Value = Int32.Parse(setting.Key[1]),
                    IsSelectionRangeEnabled = true,
                };
                s.ValueChanged += Slider_ValueChanged;
                TextBlock txtb = new TextBlock { Text = setting.Key[1], Foreground = Brushes.White, TextWrapping = TextWrapping.Wrap };           
                CheckBox chb = new CheckBox { IsChecked = s.Value == 0.0 ? false : true };
                chb.Click += SoundOn_CheckBox_Clicked;
                gg.SetValue(Grid.ColumnProperty, 1);
                s.SetValue(Grid.ColumnProperty, 0);
                txtb.SetValue(Grid.ColumnProperty, 1);
                chb.SetValue(Grid.ColumnProperty, 2);
                gg.Children.Add(s); gg.Children.Add(txtb);
                gg.Children.Add(chb); g.Children.Add(gg);
            }
            else if (setting.Value == typeof(string))
            {
                if (setting.Key[0]=="Bots difficulty")
                {
                    ListView lw = new ListView { SelectionMode = SelectionMode.Single };
                    lw.SelectionChanged += DifficultyListView_SelectedIndexChanged;
                    Button difficulty = new Button { Content = setting.Key[1], Foreground = Brushes.White, Background = Brushes.Transparent };
                    Difficulty_Popup = new Popup
                    {
                        IsOpen = false,
                        StaysOpen = false,
                        Placement = PlacementMode.Center
                    };
                    difficulty.Click += Difficulty_Button_Click;
                    foreach (string e in GameMode.GetAllDifficultyModes())
                        lw.Items.Add(e);
                    Difficulty_Popup.Child = lw;
                    Difficulty_Popup.SetValue(Grid.ColumnSpanProperty, 10);
                    difficulty.SetValue(Grid.ColumnProperty, 1);
                    g.Children.Add(difficulty);
                    g.Children.Add(Difficulty_Popup);
                }
            }
            else if (setting.Value == typeof(int[]))
            {
                ListView lw = new ListView { SelectionMode = SelectionMode.Single };
                lw.SelectionChanged += ResolutionsListView_SelectedIndexChanged;
                Button resolutions = new Button { Content = setting.Key[1], Foreground = Brushes.White, Background = Brushes.Transparent };
                Resolutions_Popup = new Popup
                {
                    IsOpen = false,
                    StaysOpen = false,
                    Placement = PlacementMode.Center
                };
                resolutions.Click += Resolutions_Button_Click;
                foreach (int[] e in GameMode.GetPossibleResolutions())
                    lw.Items.Add(e[0].ToString() + "x" + e[1].ToString());
                Resolutions_Popup.Child = lw;
                Resolutions_Popup.SetValue(Grid.ColumnSpanProperty, 10);
                resolutions.SetValue(Grid.ColumnProperty, 1);
                g.Children.Add(resolutions);
                g.Children.Add(Resolutions_Popup);
                if (GameMode.GetFullScreenMode()) resolutions.IsEnabled = false;
            }
            return g;
        }

        private void DifficultyListView_SelectedIndexChanged(object sender, RoutedEventArgs e) => ((Difficulty_Popup.Parent as Grid).Children[1] as Button).Content = (sender as ListView).SelectedItem;
        private void ResolutionsListView_SelectedIndexChanged(object sender, RoutedEventArgs e) => ((Resolutions_Popup.Parent as Grid).Children[1] as Button).Content = (sender as ListView).SelectedItem;
        private void Slider_ValueChanged(object sender, RoutedEventArgs e) => (((sender as Slider).Parent as Grid).Children[1] as TextBlock).Text = Math.Round((sender as Slider).Value).ToString();
        public void Resolutions_Button_Click(object sender, RoutedEventArgs e) => Resolutions_Popup.IsOpen = true;
        public void Difficulty_Button_Click(object sender, RoutedEventArgs e) => Difficulty_Popup.IsOpen = true;
        private void SoundOn_CheckBox_Clicked(object sender, RoutedEventArgs e)
        {
            Grid parentGrid = ((sender as CheckBox).Parent as Grid);
            if (!(sender as CheckBox).IsChecked.Value)
            {
                (parentGrid.Children[0] as Slider).Value = 0;
            }
            else
            {
                if (((parentGrid.Parent as Grid).Children[0] as TextBlock).Text.ToString() == "Music")
                    (parentGrid.Children[0] as Slider).Value = GameMode.GetMusicLevel();
                else
                    (parentGrid.Children[0] as Slider).Value = GameMode.GetSFXLevel();
            }
            (parentGrid.Children[1] as TextBlock).Text = (parentGrid.Children[0] as Slider).Value.ToString();
        }
    }
}
