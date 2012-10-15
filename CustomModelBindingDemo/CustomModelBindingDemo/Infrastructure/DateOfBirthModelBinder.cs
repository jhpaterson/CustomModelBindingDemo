using System;
using System.Web.Mvc;
using System.ComponentModel;

namespace CustomModelBindingDemo.Infrastructure
{
    public class DateOfBirthModelBinder : DefaultModelBinder
    {
        protected override void BindProperty(ControllerContext controllerContext,
            ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor)
        {
            if (propertyDescriptor.PropertyType == typeof(DateTime?)
            && propertyDescriptor.Name == "DateOfBirth")
            {
                var request = controllerContext.HttpContext.Request;
                var prefix = propertyDescriptor.Name;

                var date = string.Format("{0}/{1}/{2}",
                           request["DateOfBirth.Value.Day"],
                           request["DateOfBirth.Value.Month"],
                           request["DateOfBirth.Value.Year"]);

                DateTime dateOfBirth;
                if (DateTime.TryParse(date, out dateOfBirth))
                {
                    SetProperty(controllerContext, bindingContext,
                                propertyDescriptor, dateOfBirth);
                    return;
                }
                else
                {
                    bindingContext.ModelState.AddModelError("DateOfBirth",
                           "Date was not recognised");
                    return;
                }
            }
            base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
        }
    }
}