// -----------------------------------------------------------
//  
//  This file was generated, please do not modify.
//  
// -----------------------------------------------------------
namespace EmptyKeys.UserInterface.Generated {
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.ObjectModel;
    using EmptyKeys.UserInterface;
    using EmptyKeys.UserInterface.Data;
    using EmptyKeys.UserInterface.Controls;
    using EmptyKeys.UserInterface.Controls.Primitives;
    using EmptyKeys.UserInterface.Input;
    using EmptyKeys.UserInterface.Media;
    using EmptyKeys.UserInterface.Media.Animation;
    using EmptyKeys.UserInterface.Media.Imaging;
    using EmptyKeys.UserInterface.Shapes;
    using EmptyKeys.UserInterface.Renderers;
    using EmptyKeys.UserInterface.Themes;
    
    
    [GeneratedCodeAttribute("Empty Keys UI Generator", "1.9.0.0")]
    public partial class BasicUI : UIRoot {
        
        private Grid e_0;
        
        private Image logo;
        
        private TextBox ServerIP_TextBox;
        
        private TextBox ServerPort_TextBox;
        
        private TextBox SCON_Password_TextBox;
        
        private CheckBox AutoReconnect_CheckBox;
        
        private Button Connect_Button;
        
        private Button Disconnect_Button;
        
        private Button Refresh_Button;
        
        private TextBlock LastRefresh_TextBlock;
        
        private TabControl TabControl;
        
        private TextBlock Search_TextBlock;
        
        private ComboBox Search_ComboBox;
        
        private TextBox Search_TextBox;
        
        private CheckBox AutoRefresh_CheckBox;
        
        private TextBlock ConsoleOutput_TextBlock;
        
        public BasicUI() : 
                base() {
            this.Initialize();
        }
        
        public BasicUI(int width, int height) : 
                base(width, height) {
            this.Initialize();
        }
        
        private void Initialize() {
            Style style = RootStyle.CreateRootStyle();
            style.TargetType = this.GetType();
            this.Style = style;
            this.InitializeComponent();
        }
        
        private void InitializeComponent() {
            // e_0 element
            this.e_0 = new Grid();
            this.Content = this.e_0;
            this.e_0.Name = "e_0";
            // logo element
            this.logo = new Image();
            this.e_0.Children.Add(this.logo);
            this.logo.Name = "logo";
            this.logo.Width = 160F;
            this.logo.Margin = new Thickness(10F, 10F, 0F, 0F);
            this.logo.HorizontalAlignment = HorizontalAlignment.Left;
            this.logo.VerticalAlignment = VerticalAlignment.Top;
            BitmapImage logo_bm = new BitmapImage();
            logo_bm.TextureAsset = "Images/PokeD Logo";
            this.logo.Source = logo_bm;
            // ServerIP_TextBox element
            this.ServerIP_TextBox = new TextBox();
            this.e_0.Children.Add(this.ServerIP_TextBox);
            this.ServerIP_TextBox.Name = "ServerIP_TextBox";
            this.ServerIP_TextBox.Height = 25F;
            this.ServerIP_TextBox.Width = 160F;
            this.ServerIP_TextBox.Margin = new Thickness(10F, 175F, 0F, 0F);
            this.ServerIP_TextBox.HorizontalAlignment = HorizontalAlignment.Left;
            this.ServerIP_TextBox.VerticalAlignment = VerticalAlignment.Top;
            ToolTip tt_ServerIP_TextBox = new ToolTip();
            this.ServerIP_TextBox.ToolTip = tt_ServerIP_TextBox;
            tt_ServerIP_TextBox.Content = "Host";
            Binding binding_ServerIP_TextBox_Text = new Binding("ServerIP");
            this.ServerIP_TextBox.SetBinding(TextBox.TextProperty, binding_ServerIP_TextBox_Text);
            // ServerPort_TextBox element
            this.ServerPort_TextBox = new TextBox();
            this.e_0.Children.Add(this.ServerPort_TextBox);
            this.ServerPort_TextBox.Name = "ServerPort_TextBox";
            this.ServerPort_TextBox.Height = 25F;
            this.ServerPort_TextBox.Width = 160F;
            this.ServerPort_TextBox.Margin = new Thickness(10F, 205F, 0F, 0F);
            this.ServerPort_TextBox.HorizontalAlignment = HorizontalAlignment.Left;
            this.ServerPort_TextBox.VerticalAlignment = VerticalAlignment.Top;
            ToolTip tt_ServerPort_TextBox = new ToolTip();
            this.ServerPort_TextBox.ToolTip = tt_ServerPort_TextBox;
            tt_ServerPort_TextBox.Content = "Port";
            Binding binding_ServerPort_TextBox_Text = new Binding("ServerPort");
            this.ServerPort_TextBox.SetBinding(TextBox.TextProperty, binding_ServerPort_TextBox_Text);
            // SCON_Password_TextBox element
            this.SCON_Password_TextBox = new TextBox();
            this.e_0.Children.Add(this.SCON_Password_TextBox);
            this.SCON_Password_TextBox.Name = "SCON_Password_TextBox";
            this.SCON_Password_TextBox.Height = 25F;
            this.SCON_Password_TextBox.Width = 160F;
            this.SCON_Password_TextBox.Margin = new Thickness(10F, 235F, 0F, 0F);
            this.SCON_Password_TextBox.HorizontalAlignment = HorizontalAlignment.Left;
            this.SCON_Password_TextBox.VerticalAlignment = VerticalAlignment.Top;
            ToolTip tt_SCON_Password_TextBox = new ToolTip();
            this.SCON_Password_TextBox.ToolTip = tt_SCON_Password_TextBox;
            tt_SCON_Password_TextBox.Content = "Password";
            Binding binding_SCON_Password_TextBox_Text = new Binding("SCON_Password");
            this.SCON_Password_TextBox.SetBinding(TextBox.TextProperty, binding_SCON_Password_TextBox_Text);
            // AutoReconnect_CheckBox element
            this.AutoReconnect_CheckBox = new CheckBox();
            this.e_0.Children.Add(this.AutoReconnect_CheckBox);
            this.AutoReconnect_CheckBox.Name = "AutoReconnect_CheckBox";
            this.AutoReconnect_CheckBox.Height = 25F;
            this.AutoReconnect_CheckBox.Width = 160F;
            this.AutoReconnect_CheckBox.Margin = new Thickness(15F, 265F, 0F, 0F);
            this.AutoReconnect_CheckBox.HorizontalAlignment = HorizontalAlignment.Left;
            this.AutoReconnect_CheckBox.VerticalAlignment = VerticalAlignment.Top;
            this.AutoReconnect_CheckBox.Content = "Auto Reconnect";
            this.AutoReconnect_CheckBox.CommandParameter = "AutoReconnect";
            Binding binding_AutoReconnect_CheckBox_Command = new Binding("CheckBoxCommand");
            this.AutoReconnect_CheckBox.SetBinding(CheckBox.CommandProperty, binding_AutoReconnect_CheckBox_Command);
            // Connect_Button element
            this.Connect_Button = new Button();
            this.e_0.Children.Add(this.Connect_Button);
            this.Connect_Button.Name = "Connect_Button";
            this.Connect_Button.Width = 160F;
            this.Connect_Button.Margin = new Thickness(10F, 295F, 0F, 0F);
            this.Connect_Button.HorizontalAlignment = HorizontalAlignment.Left;
            this.Connect_Button.VerticalAlignment = VerticalAlignment.Top;
            this.Connect_Button.Content = "Connect";
            this.Connect_Button.CommandParameter = "Connect";
            Binding binding_Connect_Button_IsEnabled = new Binding("ConnectButtonVisible.Value");
            this.Connect_Button.SetBinding(Button.IsEnabledProperty, binding_Connect_Button_IsEnabled);
            Binding binding_Connect_Button_IsHitTestVisible = new Binding("ConnectButtonVisible.Value");
            this.Connect_Button.SetBinding(Button.IsHitTestVisibleProperty, binding_Connect_Button_IsHitTestVisible);
            Binding binding_Connect_Button_Command = new Binding("ButtonCommand");
            this.Connect_Button.SetBinding(Button.CommandProperty, binding_Connect_Button_Command);
            // Disconnect_Button element
            this.Disconnect_Button = new Button();
            this.e_0.Children.Add(this.Disconnect_Button);
            this.Disconnect_Button.Name = "Disconnect_Button";
            this.Disconnect_Button.Width = 160F;
            this.Disconnect_Button.Margin = new Thickness(10F, 322F, 0F, 0F);
            this.Disconnect_Button.HorizontalAlignment = HorizontalAlignment.Left;
            this.Disconnect_Button.VerticalAlignment = VerticalAlignment.Top;
            this.Disconnect_Button.Content = "Disconnect";
            this.Disconnect_Button.CommandParameter = "Disconnect";
            Binding binding_Disconnect_Button_IsEnabled = new Binding("ConnectButtonVisible.Invert");
            this.Disconnect_Button.SetBinding(Button.IsEnabledProperty, binding_Disconnect_Button_IsEnabled);
            Binding binding_Disconnect_Button_IsHitTestVisible = new Binding("ConnectButtonVisible.Invert");
            this.Disconnect_Button.SetBinding(Button.IsHitTestVisibleProperty, binding_Disconnect_Button_IsHitTestVisible);
            Binding binding_Disconnect_Button_Command = new Binding("ButtonCommand");
            this.Disconnect_Button.SetBinding(Button.CommandProperty, binding_Disconnect_Button_Command);
            // Refresh_Button element
            this.Refresh_Button = new Button();
            this.e_0.Children.Add(this.Refresh_Button);
            this.Refresh_Button.Name = "Refresh_Button";
            this.Refresh_Button.Width = 160F;
            this.Refresh_Button.Margin = new Thickness(10F, 349F, 0F, 0F);
            this.Refresh_Button.HorizontalAlignment = HorizontalAlignment.Left;
            this.Refresh_Button.VerticalAlignment = VerticalAlignment.Top;
            this.Refresh_Button.Content = "Refresh (F5)";
            this.Refresh_Button.CommandParameter = "Refresh";
            Binding binding_Refresh_Button_IsEnabled = new Binding("ConnectButtonVisible.Invert");
            this.Refresh_Button.SetBinding(Button.IsEnabledProperty, binding_Refresh_Button_IsEnabled);
            Binding binding_Refresh_Button_IsHitTestVisible = new Binding("ConnectButtonVisible.Invert");
            this.Refresh_Button.SetBinding(Button.IsHitTestVisibleProperty, binding_Refresh_Button_IsHitTestVisible);
            Binding binding_Refresh_Button_Command = new Binding("ButtonCommand");
            this.Refresh_Button.SetBinding(Button.CommandProperty, binding_Refresh_Button_Command);
            // LastRefresh_TextBlock element
            this.LastRefresh_TextBlock = new TextBlock();
            this.e_0.Children.Add(this.LastRefresh_TextBlock);
            this.LastRefresh_TextBlock.Name = "LastRefresh_TextBlock";
            this.LastRefresh_TextBlock.Height = 25F;
            this.LastRefresh_TextBlock.Width = 160F;
            this.LastRefresh_TextBlock.Margin = new Thickness(10F, 376F, 0F, 0F);
            this.LastRefresh_TextBlock.HorizontalAlignment = HorizontalAlignment.Left;
            this.LastRefresh_TextBlock.VerticalAlignment = VerticalAlignment.Top;
            this.LastRefresh_TextBlock.TextWrapping = TextWrapping.Wrap;
            Binding binding_LastRefresh_TextBlock_Text = new Binding("LastRefresh");
            this.LastRefresh_TextBlock.SetBinding(TextBlock.TextProperty, binding_LastRefresh_TextBlock_Text);
            // TabControl element
            this.TabControl = new TabControl();
            this.e_0.Children.Add(this.TabControl);
            this.TabControl.Name = "TabControl";
            this.TabControl.Margin = new Thickness(175F, 10F, 10F, 135F);
            this.TabControl.Foreground = new SolidColorBrush(new ColorW(33, 9, 9, 255));
            this.TabControl.ItemsSource = Get_TabControl_Items();
            // Search_TextBlock element
            this.Search_TextBlock = new TextBlock();
            this.e_0.Children.Add(this.Search_TextBlock);
            this.Search_TextBlock.Name = "Search_TextBlock";
            this.Search_TextBlock.Height = 25F;
            this.Search_TextBlock.Width = 60F;
            this.Search_TextBlock.Margin = new Thickness(180F, 0F, 0F, 105F);
            this.Search_TextBlock.HorizontalAlignment = HorizontalAlignment.Left;
            this.Search_TextBlock.VerticalAlignment = VerticalAlignment.Bottom;
            this.Search_TextBlock.Text = "Search:";
            this.Search_TextBlock.TextWrapping = TextWrapping.Wrap;
            // Search_ComboBox element
            this.Search_ComboBox = new ComboBox();
            this.e_0.Children.Add(this.Search_ComboBox);
            this.Search_ComboBox.Name = "Search_ComboBox";
            this.Search_ComboBox.Height = 25F;
            this.Search_ComboBox.Width = 120F;
            this.Search_ComboBox.Margin = new Thickness(245F, 0F, 0F, 105F);
            this.Search_ComboBox.HorizontalAlignment = HorizontalAlignment.Left;
            this.Search_ComboBox.VerticalAlignment = VerticalAlignment.Bottom;
            this.Search_ComboBox.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            this.Search_ComboBox.VerticalContentAlignment = VerticalAlignment.Stretch;
            this.Search_ComboBox.ItemsSource = Get_Search_ComboBox_Items();
            this.Search_ComboBox.SelectedIndex = 0;
            this.Search_ComboBox.MaxDropDownHeight = 105F;
            // Search_TextBox element
            this.Search_TextBox = new TextBox();
            this.e_0.Children.Add(this.Search_TextBox);
            this.Search_TextBox.Name = "Search_TextBox";
            this.Search_TextBox.Height = 25F;
            this.Search_TextBox.Margin = new Thickness(370F, 0F, 110F, 105F);
            this.Search_TextBox.VerticalAlignment = VerticalAlignment.Bottom;
            this.Search_TextBox.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            this.Search_TextBox.VerticalContentAlignment = VerticalAlignment.Bottom;
            // AutoRefresh_CheckBox element
            this.AutoRefresh_CheckBox = new CheckBox();
            this.e_0.Children.Add(this.AutoRefresh_CheckBox);
            this.AutoRefresh_CheckBox.Name = "AutoRefresh_CheckBox";
            this.AutoRefresh_CheckBox.Height = 25F;
            this.AutoRefresh_CheckBox.Width = 95F;
            this.AutoRefresh_CheckBox.Margin = new Thickness(0F, 0F, 10F, 105F);
            this.AutoRefresh_CheckBox.HorizontalAlignment = HorizontalAlignment.Right;
            this.AutoRefresh_CheckBox.VerticalAlignment = VerticalAlignment.Bottom;
            this.AutoRefresh_CheckBox.Content = "Auto Refresh";
            // ConsoleOutput_TextBlock element
            this.ConsoleOutput_TextBlock = new TextBlock();
            this.e_0.Children.Add(this.ConsoleOutput_TextBlock);
            this.ConsoleOutput_TextBlock.Name = "ConsoleOutput_TextBlock";
            this.ConsoleOutput_TextBlock.Height = 90F;
            this.ConsoleOutput_TextBlock.Width = 610F;
            this.ConsoleOutput_TextBlock.Margin = new Thickness(0F, 0F, 10F, 10F);
            this.ConsoleOutput_TextBlock.HorizontalAlignment = HorizontalAlignment.Right;
            this.ConsoleOutput_TextBlock.VerticalAlignment = VerticalAlignment.Bottom;
            this.ConsoleOutput_TextBlock.Text = "Test 123\r\nTest 132\r\nTest 231\r\nTest 213\r\nTest 321\r\n";
            this.ConsoleOutput_TextBlock.TextAlignment = TextAlignment.Left;
            ImageManager.Instance.AddImage("Images/PokeD Logo");
            FontManager.Instance.AddFont("Segoe UI", 12F, FontStyle.Regular, "Segoe_UI_9_Regular");
        }
        
        private static System.Collections.ObjectModel.ObservableCollection<object> Get_TabControl_Items() {
            System.Collections.ObjectModel.ObservableCollection<object> items = new System.Collections.ObjectModel.ObservableCollection<object>();
            // e_1 element
            TabItem e_1 = new TabItem();
            e_1.Name = "e_1";
            e_1.Header = "Players";
            // e_2 element
            Grid e_2 = new Grid();
            e_1.Content = e_2;
            e_2.Name = "e_2";
            // PlayersDataGrid element
            DataGrid PlayersDataGrid = new DataGrid();
            e_2.Children.Add(PlayersDataGrid);
            PlayersDataGrid.Name = "PlayersDataGrid";
            PlayersDataGrid.Margin = new Thickness(0F, 0F, 0F, 0F);
            PlayersDataGrid.AutoGenerateColumns = false;
            DataGridTextColumn PlayersDataGrid_Col0 = new DataGridTextColumn();
            PlayersDataGrid_Col0.Header = "#";
            Binding PlayersDataGrid_Col0_b = new Binding("Number");
            PlayersDataGrid_Col0.Binding = PlayersDataGrid_Col0_b;
            PlayersDataGrid.Columns.Add(PlayersDataGrid_Col0);
            DataGridTextColumn PlayersDataGrid_Col1 = new DataGridTextColumn();
            PlayersDataGrid_Col1.Header = "IP";
            Binding PlayersDataGrid_Col1_b = new Binding("IP");
            PlayersDataGrid_Col1.Binding = PlayersDataGrid_Col1_b;
            PlayersDataGrid.Columns.Add(PlayersDataGrid_Col1);
            DataGridTextColumn PlayersDataGrid_Col2 = new DataGridTextColumn();
            PlayersDataGrid_Col2.Header = "Ping";
            Binding PlayersDataGrid_Col2_b = new Binding("Ping");
            PlayersDataGrid_Col2.Binding = PlayersDataGrid_Col2_b;
            PlayersDataGrid.Columns.Add(PlayersDataGrid_Col2);
            DataGridTextColumn PlayersDataGrid_Col3 = new DataGridTextColumn();
            PlayersDataGrid_Col3.Header = "GameJolt ID";
            Binding PlayersDataGrid_Col3_b = new Binding("GameJoltID");
            PlayersDataGrid_Col3.Binding = PlayersDataGrid_Col3_b;
            PlayersDataGrid.Columns.Add(PlayersDataGrid_Col3);
            DataGridTextColumn PlayersDataGrid_Col4 = new DataGridTextColumn();
            PlayersDataGrid_Col4.Header = "Name";
            Binding PlayersDataGrid_Col4_b = new Binding("Name");
            PlayersDataGrid_Col4.Binding = PlayersDataGrid_Col4_b;
            PlayersDataGrid.Columns.Add(PlayersDataGrid_Col4);
            Binding binding_PlayersDataGrid_ItemsSource = new Binding("PlayersGridDataList");
            PlayersDataGrid.SetBinding(DataGrid.ItemsSourceProperty, binding_PlayersDataGrid_ItemsSource);
            items.Add(e_1);
            // e_3 element
            TabItem e_3 = new TabItem();
            e_3.Name = "e_3";
            e_3.Header = "Bans";
            // e_4 element
            Grid e_4 = new Grid();
            e_3.Content = e_4;
            e_4.Name = "e_4";
            // BansDataGrid element
            DataGrid BansDataGrid = new DataGrid();
            e_4.Children.Add(BansDataGrid);
            BansDataGrid.Name = "BansDataGrid";
            BansDataGrid.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            BansDataGrid.VerticalContentAlignment = VerticalAlignment.Stretch;
            BansDataGrid.AutoGenerateColumns = false;
            DataGridTextColumn BansDataGrid_Col0 = new DataGridTextColumn();
            BansDataGrid_Col0.Header = "#";
            Binding BansDataGrid_Col0_b = new Binding("Number");
            BansDataGrid_Col0.Binding = BansDataGrid_Col0_b;
            BansDataGrid.Columns.Add(BansDataGrid_Col0);
            DataGridTextColumn BansDataGrid_Col1 = new DataGridTextColumn();
            BansDataGrid_Col1.Header = "Game Jolt ID / IP";
            Binding BansDataGrid_Col1_b = new Binding("GameJoltID");
            BansDataGrid_Col1.Binding = BansDataGrid_Col1_b;
            BansDataGrid.Columns.Add(BansDataGrid_Col1);
            DataGridTextColumn BansDataGrid_Col2 = new DataGridTextColumn();
            BansDataGrid_Col2.Header = "Minutes Left";
            Binding BansDataGrid_Col2_b = new Binding("MinutesLeft");
            BansDataGrid_Col2.Binding = BansDataGrid_Col2_b;
            BansDataGrid.Columns.Add(BansDataGrid_Col2);
            DataGridTextColumn BansDataGrid_Col3 = new DataGridTextColumn();
            BansDataGrid_Col3.Header = "Reason";
            Binding BansDataGrid_Col3_b = new Binding("Reason");
            BansDataGrid_Col3.Binding = BansDataGrid_Col3_b;
            BansDataGrid.Columns.Add(BansDataGrid_Col3);
            Binding binding_BansDataGrid_ItemsSource = new Binding("BansGridDataList");
            BansDataGrid.SetBinding(DataGrid.ItemsSourceProperty, binding_BansDataGrid_ItemsSource);
            items.Add(e_3);
            // e_5 element
            TabItem e_5 = new TabItem();
            e_5.Name = "e_5";
            e_5.Header = "Player Database";
            // e_6 element
            Grid e_6 = new Grid();
            e_5.Content = e_6;
            e_6.Name = "e_6";
            // PlayersDatabaseDataGrid element
            DataGrid PlayersDatabaseDataGrid = new DataGrid();
            e_6.Children.Add(PlayersDatabaseDataGrid);
            PlayersDatabaseDataGrid.Name = "PlayersDatabaseDataGrid";
            PlayersDatabaseDataGrid.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            PlayersDatabaseDataGrid.VerticalContentAlignment = VerticalAlignment.Stretch;
            PlayersDatabaseDataGrid.AutoGenerateColumns = false;
            DataGridTextColumn PlayersDatabaseDataGrid_Col0 = new DataGridTextColumn();
            PlayersDatabaseDataGrid_Col0.Header = "#";
            Binding PlayersDatabaseDataGrid_Col0_b = new Binding("Number");
            PlayersDatabaseDataGrid_Col0.Binding = PlayersDatabaseDataGrid_Col0_b;
            PlayersDatabaseDataGrid.Columns.Add(PlayersDatabaseDataGrid_Col0);
            DataGridTextColumn PlayersDatabaseDataGrid_Col1 = new DataGridTextColumn();
            PlayersDatabaseDataGrid_Col1.Header = "Last IP";
            Binding PlayersDatabaseDataGrid_Col1_b = new Binding("LastIP");
            PlayersDatabaseDataGrid_Col1.Binding = PlayersDatabaseDataGrid_Col1_b;
            PlayersDatabaseDataGrid.Columns.Add(PlayersDatabaseDataGrid_Col1);
            DataGridTextColumn PlayersDatabaseDataGrid_Col2 = new DataGridTextColumn();
            PlayersDatabaseDataGrid_Col2.Header = "Last Seen";
            Binding PlayersDatabaseDataGrid_Col2_b = new Binding("LastSeen");
            PlayersDatabaseDataGrid_Col2.Binding = PlayersDatabaseDataGrid_Col2_b;
            PlayersDatabaseDataGrid.Columns.Add(PlayersDatabaseDataGrid_Col2);
            DataGridTextColumn PlayersDatabaseDataGrid_Col3 = new DataGridTextColumn();
            PlayersDatabaseDataGrid_Col3.Header = "Game Jolt ID";
            Binding PlayersDatabaseDataGrid_Col3_b = new Binding("GameJoltID");
            PlayersDatabaseDataGrid_Col3.Binding = PlayersDatabaseDataGrid_Col3_b;
            PlayersDatabaseDataGrid.Columns.Add(PlayersDatabaseDataGrid_Col3);
            DataGridTextColumn PlayersDatabaseDataGrid_Col4 = new DataGridTextColumn();
            PlayersDatabaseDataGrid_Col4.Header = "Name";
            Binding PlayersDatabaseDataGrid_Col4_b = new Binding("Name");
            PlayersDatabaseDataGrid_Col4.Binding = PlayersDatabaseDataGrid_Col4_b;
            PlayersDatabaseDataGrid.Columns.Add(PlayersDatabaseDataGrid_Col4);
            DataGridTextColumn PlayersDatabaseDataGrid_Col5 = new DataGridTextColumn();
            PlayersDatabaseDataGrid_Col5.Header = "Online";
            Binding PlayersDatabaseDataGrid_Col5_b = new Binding("Online");
            PlayersDatabaseDataGrid_Col5.Binding = PlayersDatabaseDataGrid_Col5_b;
            PlayersDatabaseDataGrid.Columns.Add(PlayersDatabaseDataGrid_Col5);
            Binding binding_PlayersDatabaseDataGrid_ItemsSource = new Binding("BansGridDataList");
            PlayersDatabaseDataGrid.SetBinding(DataGrid.ItemsSourceProperty, binding_PlayersDatabaseDataGrid_ItemsSource);
            items.Add(e_5);
            // e_7 element
            TabItem e_7 = new TabItem();
            e_7.Name = "e_7";
            e_7.Header = "Logs";
            // e_8 element
            Grid e_8 = new Grid();
            e_7.Content = e_8;
            e_8.Name = "e_8";
            // LogsDataGrid element
            DataGrid LogsDataGrid = new DataGrid();
            e_8.Children.Add(LogsDataGrid);
            LogsDataGrid.Name = "LogsDataGrid";
            LogsDataGrid.Margin = new Thickness(0F, 0F, 0F, 37F);
            LogsDataGrid.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            LogsDataGrid.VerticalContentAlignment = VerticalAlignment.Stretch;
            LogsDataGrid.AutoGenerateColumns = false;
            DataGridTextColumn LogsDataGrid_Col0 = new DataGridTextColumn();
            LogsDataGrid_Col0.Header = "#";
            Binding LogsDataGrid_Col0_b = new Binding("Number");
            LogsDataGrid_Col0.Binding = LogsDataGrid_Col0_b;
            LogsDataGrid.Columns.Add(LogsDataGrid_Col0);
            DataGridTextColumn LogsDataGrid_Col1 = new DataGridTextColumn();
            LogsDataGrid_Col1.Header = "Log File Name";
            Binding LogsDataGrid_Col1_b = new Binding("LogFilename");
            LogsDataGrid_Col1.Binding = LogsDataGrid_Col1_b;
            LogsDataGrid.Columns.Add(LogsDataGrid_Col1);
            Binding binding_LogsDataGrid_ItemsSource = new Binding("LogsGridDataList");
            LogsDataGrid.SetBinding(DataGrid.ItemsSourceProperty, binding_LogsDataGrid_ItemsSource);
            // button element
            Button button = new Button();
            e_8.Children.Add(button);
            button.Name = "button";
            button.Width = 75F;
            button.Margin = new Thickness(0F, 0F, 10F, 10F);
            button.HorizontalAlignment = HorizontalAlignment.Right;
            button.VerticalAlignment = VerticalAlignment.Bottom;
            button.HorizontalContentAlignment = HorizontalAlignment.Center;
            button.VerticalContentAlignment = VerticalAlignment.Center;
            button.Content = "Get Log";
            items.Add(e_7);
            // e_9 element
            TabItem e_9 = new TabItem();
            e_9.Name = "e_9";
            e_9.Header = "Settings";
            // e_10 element
            Grid e_10 = new Grid();
            e_9.Content = e_10;
            e_10.Name = "e_10";
            // listBox element
            ListBox listBox = new ListBox();
            e_10.Children.Add(listBox);
            listBox.Name = "listBox";
            listBox.Height = 443F;
            listBox.Width = 585F;
            listBox.Margin = new Thickness(10F, 10F, 0F, 0F);
            listBox.HorizontalAlignment = HorizontalAlignment.Left;
            listBox.VerticalAlignment = VerticalAlignment.Top;
            Func<UIElement, UIElement> listBox_dtFunc = listBox_dtMethod;
            listBox.ItemTemplate = new DataTemplate(listBox_dtFunc);
            Binding binding_listBox_ItemsSource = new Binding("PlayersGridDataList");
            listBox.SetBinding(ListBox.ItemsSourceProperty, binding_listBox_ItemsSource);
            items.Add(e_9);
            return items;
        }
        
        private static UIElement listBox_dtMethod(UIElement parent) {
            // e_11 element
            CheckBox e_11 = new CheckBox();
            e_11.Parent = parent;
            e_11.Name = "e_11";
            e_11.Content = "Tr";
            return e_11;
        }
        
        private static System.Collections.ObjectModel.ObservableCollection<object> Get_Search_ComboBox_Items() {
            System.Collections.ObjectModel.ObservableCollection<object> items = new System.Collections.ObjectModel.ObservableCollection<object>();
            // Entry1 element
            ComboBoxItem Entry1 = new ComboBoxItem();
            Entry1.Name = "Entry1";
            Entry1.Content = "Name";
            Entry1.IsSelected = true;
            items.Add(Entry1);
            // Entry2 element
            ComboBoxItem Entry2 = new ComboBoxItem();
            Entry2.Name = "Entry2";
            Entry2.Content = "Game Jold ID";
            Entry2.IsSelected = false;
            items.Add(Entry2);
            // Entry3 element
            ComboBoxItem Entry3 = new ComboBoxItem();
            Entry3.Name = "Entry3";
            Entry3.Content = "IP";
            Entry3.IsSelected = false;
            items.Add(Entry3);
            return items;
        }
    }
}
