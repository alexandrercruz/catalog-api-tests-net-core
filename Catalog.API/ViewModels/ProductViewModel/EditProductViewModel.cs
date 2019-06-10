using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace Catalog.API.ViewModels.ProductViewModel
{
    public class EditProductViewModel : Notifiable, IValidatable
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid CategoryId { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .HasMinLen(Title, 3, "Title", "O título deve conter no mínimo 3 caracteres")
                    .HasMaxLen(Title, 120, "Title", "O título deve conter até 120 caracteres")
                    .HasMinLen(Description, 3, "Description", "O título deve conter no mínimo 3 caracteres")
                    .HasMaxLen(Description, 120, "Description", "O título deve conter até 120 caracteres")
                    .IsGreaterThan(Price, 0, "Price", "O preço deve ser maior que zero")
                    .IsGreaterOrEqualsThan(Quantity, 0, "Quantity", "A quantidade deve ser informada")
                    .IsNotEmpty(CategoryId, "CategoryId", "O categoria deve ser informada")
            );
        }
    }
}