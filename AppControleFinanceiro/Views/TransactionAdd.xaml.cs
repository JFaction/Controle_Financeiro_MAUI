using AppControleFinanceiro.Libraries.Utils.FixBugs;
using AppControleFinanceiro.Models;
using AppControleFinanceiro.Repositories;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Platform;
using System.Text;

namespace AppControleFinanceiro.Views;

public partial class TransactionAdd : ContentPage
{
    private ITransactionRepository _repository;
    public TransactionAdd(ITransactionRepository repository)
	{
        _repository = repository;
		InitializeComponent();
		
	}

    private void TapGestureRecognizerTapped_ToClose(object sender, TappedEventArgs e)
    {
        KeyBoardBugs.HideKeyBoard();
        Navigation.PopModalAsync();

    }

    private void OnButtonClicked_Save(object sender, EventArgs e)
    {
        if (IsValidData() == false)
        {
            return;
        }

        SaveTransactionInDatabase();

        KeyBoardBugs.HideKeyBoard();

        Navigation.PopModalAsync();

        WeakReferenceMessenger.Default.Send<string>(string.Empty);
    }

    private void SaveTransactionInDatabase()
    {

        Transaction transaction = new Transaction()
        {
            Type = RadioIncome.IsChecked ? TransactionType.Income : TransactionType.Expenses,
            Name = EntryName.Text,
            Date = DatePickerDate.Date,
            Value = double.Parse(EntryValue.Text)
        };

        _repository.Add(transaction);
    }

    private bool IsValidData()
    {
        bool valid = true;
        StringBuilder sb = new StringBuilder();

        if (string.IsNullOrEmpty(EntryName.Text) || string.IsNullOrWhiteSpace(EntryName.Text))
        {
            sb.Append("O campo 'Nome' deve ser preenchido! \n");
            valid = false;
        }
        if (string.IsNullOrEmpty(EntryValue.Text) || string.IsNullOrWhiteSpace(EntryValue.Text))
        {
            sb.Append("O campo 'Valor' deve ser preenchido! \n");
            valid = false;
        }
        double result;
        if (!string.IsNullOrEmpty(EntryValue.Text) && !double.TryParse(EntryValue.Text, out result))
        {
            sb.Append("O campo 'Valor' é inválido!");
            valid = false;
        }

        if(valid == false)
        {
            LabelError.IsVisible = true;
            LabelError.Text = sb.ToString();
            sb.ToString();
        }
        return valid;
    }
}