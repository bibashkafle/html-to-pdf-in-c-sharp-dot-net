namespace htmlToPdf.Models
{
    public class ElectronicCheckViewModel
    {
        public ElectronicCheckViewModel()
        {

        }
        public ElectronicCheckViewModel(string checkNumber, string date, string payee, string payeeAddressLine1, string payeeAddressLine2, string payeeAddressCity, string payeeAddressState, string payeeAddressZip, string amountInWord, string amountInNumber, string memo)
        {
            CheckNumber = checkNumber;
            Date = date;
            Payee = payee;
            PayeeAddressLine1 = payeeAddressLine1;
            PayeeAddressLine2 = payeeAddressLine2;
            PayeeAddressCity = payeeAddressCity;
            PayeeAddressState = payeeAddressState;
            PayeeAddressZip = payeeAddressZip;
            AmountInWord = amountInWord;
            AmountInNumber = amountInNumber;
            Memo = memo;
        }

        public string CheckNumber { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string Payee { get; set; } = string.Empty;
        public string PayeeAddressLine1 { get; set; } = string.Empty;
        public string PayeeAddressLine2 { get; set; } = string.Empty;
        public string PayeeAddressCity { get; set; } = string.Empty;
        public string PayeeAddressState { get; set; } = string.Empty;
        public string PayeeAddressZip { get; set; } = string.Empty;
        public string AmountInWord { get; set; } = string.Empty;
        public string AmountInNumber { get; set; } = string.Empty;
        public string Memo { get; set; } = string.Empty;
    }
}
