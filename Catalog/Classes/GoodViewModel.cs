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

        public static bool IsValidGood(string Name, string Price, string Count, string Image, string Processor, string DisplaySize, string Battery, string Camera)
        {
            if (!IsValidName(Name))
                return false;
            if (!IsValidPrice(Price))
                return false;
            if (!IsValidCount(Count))
                return false;
            if (!IsValidImage(Image))
                return false;
            if (!IsValidProcessor(Processor))
                return false;
            if (!IsValidDisplaySize(DisplaySize))
                return false;
            if (!IsValidBattery(Battery))
                return false;
            if (!IsValidCamera(Camera))
                return false;

            return true;
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

            builder.RuleFor(vm => vm.Processor)
                .NotEmpty()
                .When(vm => vm.Processor, processor => String.IsNullOrEmpty(processor) == true)
                    .WithLocalizedMessage(nameof(Resources.Additional.NotEmptyRES))
                .Must(IsValidProcessor)
                    .WithLocalizedMessage(nameof(Resources.Additional.ProcessorRES));

            builder.RuleFor(vm => vm.DisplaySize)
                .NotEmpty()
                .When(vm => vm.DisplaySize, displaySize => String.IsNullOrEmpty(displaySize) == true)
                    .WithLocalizedMessage(nameof(Resources.Additional.NotEmptyRES))
                .Must(IsValidDisplaySize)
                    .WithLocalizedMessage(nameof(Resources.Additional.DisplaySizeRES));

            builder.RuleFor(vm => vm.Battery)
                .NotEmpty()
                .When(vm => vm.Battery, battery => String.IsNullOrEmpty(battery) == true)
                    .WithLocalizedMessage(nameof(Resources.Additional.NotEmptyRES))
                .Must(IsValidBattery)
                    .WithLocalizedMessage(nameof(Resources.Additional.BatteryRES));

            builder.RuleFor(vm => vm.Camera)
                .NotEmpty()
                .When(vm => vm.Camera, camera => String.IsNullOrEmpty(camera) == true)
                    .WithLocalizedMessage(nameof(Resources.Additional.NotEmptyRES))
                .Must(IsValidCamera)
                    .WithLocalizedMessage(nameof(Resources.Additional.CameraRES));


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

        private string _processor;
        public string Processor
        {
            get => _processor;
            set
            {
                _processor = value;
                OnPropertyChanged();
            }
        }

        private string _displaySize;
        public string DisplaySize
        {
            get => _displaySize;
            set
            {
                _displaySize = value;
                OnPropertyChanged();
            }
        }

        private string _battery;
        public string Battery
        {
            get => _battery;
            set
            {
                _battery = value;
                OnPropertyChanged();
            }
        }

        private string _camera;
        public string Camera
        {
            get => _camera;
            set
            {
                _camera = value;
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

            if (image.Contains(';'))
                return false;

            if (image.Length < 4)
                return false;

            if (image.Substring(0, 4) == "http")
                return true;
            else
                return false;
        }

        private static bool IsValidProcessor(string processor)
        {
            if (String.IsNullOrEmpty(processor) == true)
                return true;

            if (processor.Contains(':') || processor.Contains(';') ||
                processor.Contains('.') || processor.Contains(',') ||
                processor.Contains('|') || processor.Contains('/') ||
                processor.Contains(@"\") || processor.Contains(@"'"))
                return false;

            return true;
        }

        private static bool IsValidDisplaySize(string displaySize)
        {
            if (String.IsNullOrEmpty(displaySize) == true)
                return true;

            if (displaySize.Contains(':') || displaySize.Contains(';') ||
                displaySize.Contains('|') || displaySize.Contains('/') ||
                displaySize.Contains(@"\") || displaySize.Contains(@"'"))
                return false;

            double num;
            if (!double.TryParse(displaySize, out num))
                return false;

            if (Convert.ToDouble(displaySize) < 0.01)
                return false;


            return true;
        }

        private static bool IsValidBattery(string battery)
        {
            if (String.IsNullOrEmpty(battery) == true)
                return true;

            if (battery.Contains(':') || battery.Contains(';') ||
                battery.Contains('.') || battery.Contains(',') ||
                battery.Contains('|') || battery.Contains('/') ||
                battery.Contains(@"\") || battery.Contains(@"'"))
                return false;

            int num;
            if (!int.TryParse(battery, out num))
                return false;

            if (Convert.ToInt32(battery) < 1)
                return false;


            return true;
        }

        private static bool IsValidCamera(string camera)
        {
            if (String.IsNullOrEmpty(camera) == true)
                return true;

            if (camera.Contains(':') || camera.Contains(';') ||
                camera.Contains('.') || camera.Contains(',') ||
                camera.Contains('|') || camera.Contains('/') ||
                camera.Contains(@"\") || camera.Contains(@"'"))
                return false;

            int num;
            if (!int.TryParse(camera, out num))
                return false;

            if (Convert.ToInt32(camera) < 1)
                return false;


            return true;
        }
    }
}
