using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Microsoft.WindowsAPICodePack.Dialogs;
using TagLib.Riff;
using File = System.IO.File;

namespace XMusic;

public partial class MainWindow : Window
{
    List <string> tagList= new ();
    List <Entry> entries = new ();
    List<Entry> tdEntries = new ();
    private int money;
    private int FinSum;
    public MainWindow()
    {
        
        InitializeComponent();
        datePicker.SelectedDate = DateTime.Today;
        string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        // Видимо, я где-то косячу с путём файла, тк ошибок никаких нет, а данные не сохраняются, крч разобраться не успеваю ((

        /*if (File.Exists(desktop + "\\" + "Entries.json"))  
        {
            List<Entry> des = JsonCon.fromJson<List<Entry>>("Entries.json");
            if (des != null)
            {
                foreach (var i in des)
                {
                    entries.Add(i);
                }
                foreach (var entry in entries)
                {
                    money = (entry.IsIncome == false) ? money *= (-1) : money;
                }
            }
        }
        else
        {
            File.Create(desktop + "\\" + "Entries.json");
        }
        

        List<string> tags = JsonCon.fromJson<List<string>>("Tags.json");
        if (tags != null)
        {
            foreach (var tag in tags)
            {
                if (tag != "")
                {
                    tagList.Add(tag);
                    Tags.Items.Add(tag);
                }
            }
        }*/
        fromJson();
        TodayEntries();

        int sump = 0;
        int summ = 0;
        foreach (var entry in entries)
        {
            if (entry.IsIncome == false)
            {
                summ += entry.Money;
            }
            else
            {
                sump += entry.Money;
            }

            FinSum = sump + summ;
            Fin.Content = FinSum;
        }
    }

    private void Addtag_Click(object sender, RoutedEventArgs e)
    {
        var inputDialog = new InputDialog();

        if (inputDialog.ShowDialog() == true)
        {
            string newItem = inputDialog.Answer;
            Tags.Items.Add(newItem);
            tagList.Add(newItem);
            Tags.SelectedItem = newItem;
        }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        AddEntry();
    }

    private void AddEntry()
    {
        money = (Convert.ToInt32(MoneyB.Text));
        FinSum += money;
        Fin.Content = FinSum;
        bool done = money >= 0 ? true : false;
        money = money < 0 ? money * (-1) : money;
        Entry newE = new Entry(datePicker.SelectedDate.Value, NameB.Text, Tags.SelectedItem.ToString(), money, done);

        entries.Add(newE);
        TodayEntries();
    }

    private void DatePicker_OnSelectedDateChanged(object? sender, SelectionChangedEventArgs e)
    {
        TodayEntries();
    }

    private void MainWindow_OnClosing(object? sender, CancelEventArgs e)
    {
        /*JsonCon.toJson(new List<Entry>(),"Entries.json");
        JsonCon.toJson(new List<string>(), "Tags.json");*/
        toJson();
    }

    private void TodayEntries()
    {
        tdEntries.Clear();
        foreach (var entry in entries)
        {
            if (entry.EntryDate == datePicker.SelectedDate)
            {
                tdEntries.Add(entry);
            }
        }
        DataGrid.ItemsSource = null;
        DataGrid.ItemsSource = tdEntries;
    }

    private void toJson()
    {
        string entr = JsonConvert.SerializeObject(entries);
        File.WriteAllText("Entries.json", entr); // в bin валяются

        string tags = JsonConvert.SerializeObject(tagList);
        File.WriteAllText("Tags.json", tags);
    }

    private void fromJson()
    {
        string text = File.ReadAllText("Entries.json");
        List<Entry> des = JsonConvert.DeserializeObject<List<Entry>>(text);
        if (des != null)
        {
            foreach (var i in des)
            {
                entries.Add(i);
            }
            foreach (var entry in entries)
            {
                money = (entry.IsIncome == false) ? money *= (-1) : money;
            }
        }

        string td = File.ReadAllText("Tags.json");
        List<string> tags = JsonConvert.DeserializeObject<List<string>>(td);
        if (tags != null)
        {
            foreach (var tag in tags)
            {
                if (tag != "")
                {
                    tagList.Add(tag);
                    Tags.Items.Add(tag);
                }
            }
        }
    }

    private void DataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (DataGrid.SelectedIndex >= 0)
        {
            NameB.Text = tdEntries[DataGrid.SelectedIndex].Name;


            Tags.SelectedItem = tdEntries[DataGrid.SelectedIndex].Tag;
            if (tdEntries[DataGrid.SelectedIndex].IsIncome == true)
            {
                MoneyB.Text = tdEntries[DataGrid.SelectedIndex].Money.ToString();
            }
            else
            {
                tdEntries[DataGrid.SelectedIndex].Money *= (-1);
                MoneyB.Text = tdEntries[DataGrid.SelectedIndex].Money.ToString();
            }
        }
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        DelEntry();
    }

    private void DelEntry()
    {
        money = (Convert.ToInt32(MoneyB.Text));
        FinSum -= money;
        Fin.Content = FinSum;
        NameB.Text = null;
        Tags.SelectedItem = 0;
        MoneyB.Text = null;
        entries.Remove(DataGrid.SelectedItem as Entry);
        toJson();
        TodayEntries();
    }

    private void Button_Click_2(object sender, RoutedEventArgs e)
    {
        int delInd = DataGrid.SelectedIndex;
        AddEntry();
        DataGrid.SelectedIndex = delInd;
        DelEntry();
    }
}
