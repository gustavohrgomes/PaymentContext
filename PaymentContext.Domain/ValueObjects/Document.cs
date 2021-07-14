using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Document : ValueObject
    {
        public Document(string number, EDocumentType type)
        {
            Number = number;
            Type = type;

            AddNotifications(new Contract()
                .Requires()
                .IsTrue(Validate(), "Document.Number", "Documento Inválido")
            );
        }

        private const int CNPJ_LENGHT = 14;
        private const int CPF_LENGHT = 11;
        public string Number { get; private set; }
        public EDocumentType Type { get; private set; }
        
        private bool Validate() 
        {
            if (Type == EDocumentType.CNPJ && Number.Length == CNPJ_LENGHT)
                return true;
            
            if (Type == EDocumentType.CPF && Number.Length == CPF_LENGHT)
                return true;

            return false;
        }
    }
}