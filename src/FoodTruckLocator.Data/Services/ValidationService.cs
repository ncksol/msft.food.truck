using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geolocation;

namespace FoodTruckLocator.Data.Services
{
    public class ValidationService : IValidationService
    {
        public bool ValidateCoordinates(double latitude, double longitude)
        {
            return CoordinateValidator.Validate(latitude, longitude);
        }
    }
}
