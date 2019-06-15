using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Xml.Serialization;
using WpfApp1.Help;
using WpfApp1.Login;
using WpfApp1.Model;
using WpfApp1.ViewWindows;
using WpfApp1.WindowsChoose;

namespace WpfApp1
{
    public partial class MainWindow : Window, CommandExecuted
    {
        private Point startPoint;
        private static MainWindow instance = null;
        public ObservableCollection<Species> SpeciesList
        {
            get;
            set;
        }

        public static MainWindow getInstance()
        {
            if (instance == null)
                instance = new MainWindow();

            return instance;
        }
        
        public MainWindow()
        {
            instance = this;
            SpeciesList = Lists.SpeciesList;
            InitializeComponent();
            this.DataContext = this;
            MapSpecies();

        }
        //Adding all species on map
        private void MapSpecies()
        {
            DragImage.Source = null;
            foreach (Species sp in Lists.MapSpecies)
            {
                Image img = new Image();
                if (!sp.Species_Path.Equals(""))
                    img.Source = new BitmapImage(new Uri(sp.Species_Path));
                img.Width = 30;
                img.Height = 30;
                img.Stretch = System.Windows.Media.Stretch.Fill;
                img.Tag = sp.Mark;
                img.AllowDrop = false;
                img.PreviewMouseRightButtonDown += DraggedImagePreviewMouseRightButtonDown;
                img.PreviewMouseLeftButtonDown += DraggedImagePreviewMouseLeftButtonDown;
                img.MouseMove += DraggedImageMouseMove;
                AddImageInfo(img, sp);
                canvasMap.Children.Add(img);
                Canvas.SetLeft(img, sp.X - 20);
                Canvas.SetTop(img, sp.Y - 20);

            }

          

        }

        //Aktions
        private void New_Species_Click(object sender, RoutedEventArgs e)
        {
            SpeciesWindow speciesWindow = new SpeciesWindow();
            speciesWindow.Show();
        }

        private void New_Etiquette(object sender, RoutedEventArgs e)
        {
            EtiquetteWindow etiqWindow = new EtiquetteWindow();
            etiqWindow.Show();
        }

        private void Show_Species_Click(object sender, RoutedEventArgs e)
        {
            Window2 spWindow = new Window2();
            spWindow.Show();
        }

        private void New_Type_Click(object sender, RoutedEventArgs e)
        {
            TypeWindow newType = new TypeWindow();
            newType.Show();
        }

        private void Show_Type_Click(object sender, RoutedEventArgs e)
        {
            TypeView wn = new TypeView();
            wn.Show();
        }

        private void Show_Etiquette(object sender, RoutedEventArgs e)
        {
            EtiquettePreview ep = new EtiquettePreview();
            ep.Show();
        }

        private void App_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Save_Lists();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Save_Lists();
            LoginClass login = new LoginClass();
            login.Show();
            this.Close();
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            CommandBinding_Executed(MainH, null);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            CommandBinding_Executed(MainH, null);
        }
        //Reloading species on map
        public void Refresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshPublic();
        }


        private void Save_Lists()
        {
            var requiredPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)));
            var current_file = requiredPath.Substring(6);
            var path_etiquette = current_file + "\\Etiquettes.xml";
            var path_type = current_file + "\\Types.xml";
            var path_species = current_file + "\\Species.xml";
            var path_user = current_file + "\\Users.xml";
            var path_map_species = current_file + "\\MapSpecies.xml";

            using (FileStream fs = new FileStream(path_etiquette, FileMode.Create))
            {
                XmlSerializer xSer = new XmlSerializer(typeof(ObservableCollection<Etiquette>));

                xSer.Serialize(fs, Lists.Etiquette_list);
            }
            using (FileStream fs = new FileStream(path_type, FileMode.Create))
            {
                XmlSerializer xSer = new XmlSerializer(typeof(ObservableCollection<TypeClass>));

                xSer.Serialize(fs, Lists.Type_list);
            }
            using (FileStream fs = new FileStream(path_species, FileMode.Create))
            {
                XmlSerializer xSer = new XmlSerializer(typeof(ObservableCollection<Species>));

                xSer.Serialize(fs, Lists.SpeciesList);
            }
            using (FileStream fs = new FileStream(path_user, FileMode.Create))
            {
                XmlSerializer xSer = new XmlSerializer(typeof(ObservableCollection<User>));

                xSer.Serialize(fs, Lists.User_list);
            }
            using (FileStream fs = new FileStream(path_map_species, FileMode.Create))
            {
                XmlSerializer xSer = new XmlSerializer(typeof(ObservableCollection<Species>));

                xSer.Serialize(fs, Lists.MapSpecies);
            }
        }

        private void Species_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Changing drag image source
            if (Species.SelectedValue != null)
            {
                Species sp = (Species)Species.SelectedValue;
                if (!sp.Species_Path.Equals(""))
                    DragImage.Source = new BitmapImage(new Uri(sp.Species_Path));
            }
            else
            {
                DragImage.Source = null;
            }
        }

        private void DragImage_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void DragImage_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            System.Windows.Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;
            if (e.LeftButton == MouseButtonState.Pressed &&
               (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
               Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                try
                {
                    Species selected = (Species)Species.SelectedValue;
                    if (selected != null)
                    {
                        DataObject dragData = new DataObject("species", selected);
                        DragDrop.DoDragDrop(DragImage, dragData, DragDropEffects.Move);
                    }
                }
                catch
                {
                    return;
                }
            }
        }

        private void dropOnMe_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("species"))
            {
                Species sp = e.Data.GetData("species") as Species;

                bool result = canvasMap.Children.Cast<Image>()
                           .Any(x => x.Tag != null && x.Tag.ToString() == sp.Mark);
                System.Windows.Point d0 = e.GetPosition(canvasMap);

                //Not allowing overlapping pictures, there is a safe zone around each picture
                foreach (Species spec in Lists.MapSpecies)
                {
                    if(sp.Mark != spec.Mark)
                    {
                        if (Math.Abs(spec.X - d0.X) <= 30 && Math.Abs(spec.Y - d0.Y) <= 30)
                        {
                            System.Windows.MessageBox.Show("Species already exists in this location! Please readd species!", "Warning!");
                            return;
                        }
                    }

                }
                
                if (result == false) //Already on map?
                {

                    Image img = new Image();
                    if (!sp.Species_Path.Equals(""))
                        img.Source = new BitmapImage(new Uri(sp.Species_Path));
                   

                    img.Width = 30;
                    img.Height = 30;
                    img.Stretch = System.Windows.Media.Stretch.Fill;
                    img.Tag = sp.Mark;
                
                    var positionX = e.GetPosition(canvasMap).X;
                    var positionY = e.GetPosition(canvasMap).Y;
                 
                    sp.X = positionX;
                    sp.Y = positionY;
                    //Adding info for picture
                    AddImageInfo(img, sp);
                    //Allow delete
                    img.PreviewMouseRightButtonDown += DraggedImagePreviewMouseRightButtonDown;
              
                    img.PreviewMouseLeftButtonDown += DraggedImagePreviewMouseLeftButtonDown;
                    
                    img.MouseMove += DraggedImageMouseMove;
                    img.AllowDrop = false;
                   
                    canvasMap.Children.Add(img);
                    Canvas.SetLeft(img, positionX - 20);
                    Canvas.SetTop(img, positionY - 20);
                    bool exists = false;
                    foreach(Species spec in Lists.MapSpecies)
                    {
                        if (spec.Mark.Equals(sp.Mark))
                        {
                            exists = true;
                        }
                    }

                    if(!exists)
                     Lists.MapSpecies.Add(sp);
                   
                }
                else
                {
                    System.Windows.MessageBox.Show("Species with this mark already exists on map!", "Add species on map");

                }
            }
        }
    
        //Right click - delete dialog
        private void DraggedImagePreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

            YesNo dialog = new YesNo();
           
            dialog.ShowDialog();
            if(dialog.YES == 1)
            {
                Image img = sender as Image;

                foreach (Species sp in Lists.MapSpecies)
                {
                    if (img.Tag.Equals(sp.Mark))
                    {
                        Lists.MapSpecies.Remove(sp);
                        break;
                    }
                }
                canvasMap.Children.Remove(img);
            }

        }

        private void DraggedImagePreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            startPoint = e.GetPosition(null);
            Image img = sender as Image;

            foreach (Species sp in Lists.SpeciesList)
            {
                if (sp.Mark.Equals(img.Tag))
                {
                    Species.SelectedValue = sp;
                    if (!sp.Species_Path.Equals(""))
                    {
                        DragImage.Source = new BitmapImage(new Uri(sp.Species_Path));
                        DragImage.Tag = sp.Mark;
                    }
                 

                }
            }
            if (e.LeftButton == MouseButtonState.Released)
                e.Handled = true;

        }
        //pomjeranje fotke
        private void DraggedImageMouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;
            if (e.LeftButton == MouseButtonState.Pressed &&
               (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
               Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {

                Species selected = (Species)Species.SelectedValue;

                if (selected != null)
                {
                    Image img = sender as Image;
                   
                    foreach (Species sp in Lists.MapSpecies)
                    {
                        if (img.Tag.Equals(sp.Mark))
                        {
                            Lists.MapSpecies.Remove(sp);
                            break;
                        }
                    }
                    canvasMap.Children.Remove(img);


                    DataObject dragData = new DataObject("species", selected);
                    DragDrop.DoDragDrop(img, dragData, DragDropEffects.Move);

                }

            }

        }
 

        public void RefreshPublic()
        {
            canvasMap.Children.Clear();
            MapSpecies();
        }

        private void AddImageInfo(Image img, Species sp)
        {

            WrapPanel wp = new WrapPanel();
            wp.Orientation = Orientation.Vertical;
            TextBlock info = new TextBlock();
           
            string type = sp.Species_Type;
            if (type.Equals(""))
            {
                type = "No type";
            }

            string dangerous = "No";
            string red = "No";
            string settled = "No";
            if (sp.Species_Dangerous)
                dangerous = "Yes";
            if (sp.Species_Red)
                red = "Yes";
            if (sp.Species_Settled)
                settled = "Yes";

            string endange = sp.Species_Endange;
            string touristic = sp.Species_Touristic;
            if (endange.Equals(""))
                endange = "No info";
            if (touristic.Equals(""))
                touristic = "No info";
            info.Inlines.Add(new Bold(new Run("Species name: ")));
            info.Inlines.Add(sp.Species_Name);
            info.Inlines.Add(Environment.NewLine);
            info.Inlines.Add(new Bold(new Run("Species type: ")));
            info.Inlines.Add(type);
            info.Inlines.Add(Environment.NewLine);
            info.Inlines.Add(new Bold(new Run("Species mark: ")));
            info.Inlines.Add(sp.Mark);
            info.Inlines.Add(Environment.NewLine);
            info.Inlines.Add(new Bold(new Run("Discovered: ")));
            info.Inlines.Add(sp.Discovered);
            info.Inlines.Add(Environment.NewLine);
            info.Inlines.Add(new Bold(new Run("Dangerous: ")));
            info.Inlines.Add(dangerous);
            info.Inlines.Add(Environment.NewLine);
            info.Inlines.Add(new Bold(new Run("On IUCN red list: ")));
            info.Inlines.Add(red);
            info.Inlines.Add(Environment.NewLine);
            info.Inlines.Add(new Bold(new Run("Lives in settled region: ")));
            info.Inlines.Add(settled);
            info.Inlines.Add(Environment.NewLine);
            info.Inlines.Add(new Bold(new Run("Endangerment: ")));
            info.Inlines.Add(Environment.NewLine);
            info.Inlines.Add("* ");
            info.Inlines.Add(endange);
            info.Inlines.Add(Environment.NewLine);
            info.Inlines.Add(new Bold(new Run("Touristic status: ")));
            info.Inlines.Add(Environment.NewLine);
            info.Inlines.Add("*");
            info.Inlines.Add(touristic);
            info.Inlines.Add(Environment.NewLine);
            info.Inlines.Add(new Bold(new Run("Income: ")));
            info.Inlines.Add(sp.Income.ToString());
            info.Inlines.Add(Environment.NewLine);
            info.Inlines.Add(new Bold(new Run("Etiquettes: ")));
             int i = 0;
             foreach (Etiquette et in sp.selected_etiquettes)
             {
                i++;
                info.Inlines.Add(Environment.NewLine);
                info.Inlines.Add(new Bold(new Run(i.ToString() + ") ")));
                info.Inlines.Add(et.Etiquette_Mark);
              
             }
             if (i == 0)
             {
                info.Inlines.Add("No etiquette");
               
             }
             
            wp.Children.Add(info);


            ToolTip tt = new ToolTip();
            tt.Content = wp;
            img.ToolTip = tt;
        }

        public void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            if (focusedControl is DependencyObject)
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                HelpProvider.ShowHelp(str);
            }
        }

       
    }
}

