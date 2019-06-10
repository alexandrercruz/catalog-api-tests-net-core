using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace Catalog.API.ViewModels.CategoryViewModel
{
    public class EditCategoryViewModel : Notifiable, IValidatable
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .HasMaxLen(Title, 120, "Title", "O título deve conter até 120 caracteres")
                    .HasMinLen(Title, 3, "Title", "O título deve conter no mínimo 3 caracteres")
            );
        }
    }
}