using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class AnimalInputValidator
    {
        public ValidationResult ValidateAnimalName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > 50)
            {
                return ValidationResult.Error("Please enter a valid name for the animal. (1-50 chars)");
            }
            return ValidationResult.Success();
        }

        public ValidationResult ValidateAltitude(int altitude)
        {
            if (altitude <= 0 || altitude > 1000)
            {
                return ValidationResult.Error("Please enter a valid altitude for the animal. (1-1000)");
            }
            return ValidationResult.Success();
        }

        public ValidationResult ValidateDivingDepth(int divingDepth)
        {
            if (divingDepth <= 0 || divingDepth > 1000)
            {
                return ValidationResult.Error("Please enter a valid diving depth for the animal. (1-1000)");
            }
            return ValidationResult.Success();
        }

        public ValidationResult ValidateSpeed(int speed)
        {
            if (speed <= 0 || speed > 200)
            {
                return ValidationResult.Error("Please enter a valid sped for the animal. (1-200)");
            }
            return ValidationResult.Success();
        }
    }
}
