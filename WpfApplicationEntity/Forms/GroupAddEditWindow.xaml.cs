using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplicationEntity.Forms
{
    /// <summary>
    /// Логика взаимодействия для GroupAddEditWindow.xaml
    /// </summary>
    public partial class GroupAddEditWindow : Window
    {
        private bool add_edit;
        private int id;
        public GroupAddEditWindow()
        {
            InitializeComponent();
        }
        public GroupAddEditWindow(bool add_edit, int id = 0)
        {
            InitializeComponent();
            this.add_edit = add_edit;
            this.id = id;
            if (this.add_edit == false)
            {
                using (WFAEntity.API.MyDBContext objectMyDBContext =
                            new WFAEntity.API.MyDBContext())
                {
                    WFAEntity.API.Group objectGroup = 
                        WFAEntity.API.DatabaseRequest.GetGroupById(objectMyDBContext, this.id);
                    textBlockAddEditGroup.Text = objectGroup.Name;
                }
                ButtonAddEditGroup.Content = "Изменить";
            }
        }
        private bool IsDataCorrcet()
        {
            return textBlockAddEditGroup.Text != string.Empty;
        }
        private void ButtonAddEditGroup_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("--------");
            int x = 0;
            try
            {
                MessageBox.Show("--------");
                using (WFAEntity.API.MyDBContext objectMyDBContext =
                            new WFAEntity.API.MyDBContext())
                {
                    if (this.IsDataCorrcet() == true)
                    {
                        WFAEntity.API.Group objectGroup = new WFAEntity.API.Group();
                        objectGroup.Name = textBlockAddEditGroup.Text;
                        if (this.add_edit == true)
                        {
                            objectMyDBContext.Groups.Add(objectGroup);
                            objectMyDBContext.SaveChanges();
                            /*WFAEntity.API.Group objectGroup = new WFAEntity.API.Group();
                            objectGroup.Name = textBlockAddEditGroup.Text;
                            objectMyDBContext.Groups.Add(objectGroup);
                            objectMyDBContext.SaveChanges();*/
                        }
                        else
                        {
                            WFAEntity.API.Group objectGroupFromDataBase = new WFAEntity.API.Group();
                            objectGroupFromDataBase = WFAEntity.API.DatabaseRequest.GetGroupById(objectMyDBContext, this.id);
                            objectMyDBContext.Entry(objectGroupFromDataBase).CurrentValues.SetValues(objectGroup);
                            objectMyDBContext.SaveChanges();
                            /*WFAEntity.API.Group objectGroup = new WFAEntity.API.Group();
                            objectGroup = WFAEntity.API.DatabaseRequest.GetGroupById(objectMyDBContext, this.id);
                            objectGroup.Name = textBlockAddEditGroup.Text;
                            objectMyDBContext.Entry(objectGroup).State = System.Data.Entity.EntityState.Modified;
                            objectMyDBContext.SaveChanges();*/
                        }
                        this.DialogResult = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА (Группа)", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
