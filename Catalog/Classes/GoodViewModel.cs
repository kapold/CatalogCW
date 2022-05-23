using ReactiveValidation;
using ReactiveValidation.Extensions;
using ReactiveValidation.Attributes;
using ReactiveValidation.Exceptions;
using ReactiveValidation.Helpers;
using ReactiveValidation.Validators;
using ReactiveValidation.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Classes
{
    public class GoodViewModel : ValidatableObject
    {
        // Reactive Validation
        public GoodViewModel()
        {
            Validator = GetValidator();
            // Ресурс по умолчанию
            ValidationOptions.LanguageManager.DefaultResourceManager = Resources.Additional.ResourceManager;
        }

        private IObjectValidator GetValidator()
        {
            var builder = new ValidationBuilder<GoodViewModel>();

            builder.RuleFor(vm => vm.Name)
                .NotEmpty()
                .When(vm => vm.Name, name => String.IsNullOrEmpty(name) == true)
                    .WithLocalizedMessage(nameof(Resources.Additional.NotEmptyRES))
                .Must(IsValidName)
                    .WithLocalizedMessage(nameof(Resources.Additional.ContainsRES));

            builder.RuleFor(vm => vm.Price)
                .NotEmpty()
                .When(vm => vm.Price, price => String.IsNullOrEmpty(price) == true)
                    .WithLocalizedMessage(nameof(Resources.Additional.NotEmptyRES))
                .Must(IsValidPrice)
                    .WithLocalizedMessage(nameof(Resources.Additional.PriceRES));

            builder.RuleFor(vm => vm.Count)
                .NotEmpty()
                .When(vm => vm.Count, count => String.IsNullOrEmpty(count) == true)
                    .WithLocalizedMessage(nameof(Resources.Additional.NotEmptyRES))
                .Must(IsValidCount)
                    .WithLocalizedMessage(nameof(Resources.Additional.CountRES));

            builder.RuleFor(vm => vm.Image)
                .NotEmpty()
                .When(vm => vm.Count, count => String.IsNullOrEmpty(count) == true)
                    .WithLocalizedMessage(nameof(Resources.Additional.NotEmptyRES))
                .Must(IsValidImage)
                    .WithLocalizedMessage(nameof(Resources.Additional.ImageRES));

            builder.RuleFor(vm => vm.Description)
                .NotEmpty()
                .When(vm => vm.Description, description => String.IsNullOrEmpty(description) == true)
                    .WithLocalizedMessage(nameof(Resources.Additional.NotEmptyRES));


            return builder.Build(this);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _price;
        public string Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        private string _count;
        public string Count
        {
            get => _count;
            set
            {
                _count = value;
                OnPropertyChanged();
            }
        }

        private string _image;
        public string Image
        {
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        private static bool IsValidName(string name)
        {
            if (String.IsNullOrEmpty(name) == true)
                return true;

            if (name.Contains(':') || name.Contains(';') ||
                name.Contains('.') || name.Contains(',') ||
                name.Contains('|') || name.Contains('/') ||
                name.Contains(@"\") || name.Contains(@"'"))
                return false;

            return true;
        }

        private static bool IsValidPrice(string price)
        {
            if (String.IsNullOrEmpty(price) == true)
                return true;

            if (price.Contains(':') || price.Contains(';') ||
                price.Contains('|') || price.Contains('/') ||
                price.Contains(@"\") || price.Contains(@"'"))
                return false;

            double num;
            if (!double.TryParse(price, out num))
                return false;

            if (Convert.ToDouble(price) < 0.01)
                return false;


            return true;
        }

        private static bool IsValidCount(string count)
        {
            if (String.IsNullOrEmpty(count) == true)
                return true;

            if (count.Contains(':') || count.Contains(';') ||
                count.Contains('.') || count.Contains(',') ||
                count.Contains('|') || count.Contains('/') ||
                count.Contains(@"\") || count.Contains(@"'"))
                return false;

            int num;
            if (!int.TryParse(count, out num))
                return false;

            if (Convert.ToInt32(count) < 1)
                return false;


            return true;
        }

        private static bool IsValidImage(string image)
        {
            if (String.IsNullOrEmpty(image) == true)
                return true;

            if (image.Contains(':') || image.Contains(';'))
                return false;

            if (image.Length < 4)
                return false;

            if (image.Substring(0, 4) == "http")
                return true;
            else
                return false;
        }
    }
}
