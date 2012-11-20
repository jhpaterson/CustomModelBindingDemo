using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Web.Mvc;
using CustomModelBindingDemo.Models;

namespace CustomModelBindingDemo.Infrastructure
{
    public class CardModelBinder : DefaultModelBinder
    {
        protected override void BindProperty(ControllerContext controllerContext,
            ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor)
        {
            if (propertyDescriptor.PropertyType == typeof(DateTime?)
                && propertyDescriptor.Name == "Expiry")
            {
                var valueProvider = bindingContext.ValueProvider;

                var date = string.Format("1/{0}/{1}",
                           valueProvider.GetValue("Expiry.Value.Month").AttemptedValue,
                           valueProvider.GetValue("Expiry.Value.Year").AttemptedValue
                           );

                DateTime expiry;
                if (DateTime.TryParse(date, out expiry))
                {
                    SetProperty(controllerContext, bindingContext,
                                propertyDescriptor, expiry);
                    return;
                }
                else
                {
                    bindingContext.ModelState.AddModelError("Expiry",
                           "Date was not recognised");
                    return;
                }
            }
            base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
        }
    }
}