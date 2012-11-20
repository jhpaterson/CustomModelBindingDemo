using System.Collections.Generic;
using System.Web.Mvc;
using CustomModelBindingDemo.Models;

namespace CustomModelBindingDemo.Infrastructure
{
    public class UserModelBinder : IModelBinder {
    private readonly IModelBinder binder;

    public UserModelBinder(IModelBinder binder) {
        this.binder = binder;
    }

    public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
    {
        var user = (User)binder.BindModel(controllerContext, bindingContext);
        AddRoles(user, bindingContext);
        return user;
    }

    private static void AddRoles(User user, ModelBindingContext bindingContext)
    {
        foreach (var role in GetRoles(bindingContext)) {
            user.AddRole(role);
        }
    }

    private static IEnumerable<Role> GetRoles(ModelBindingContext bindingContext)
    {
        var roles = bindingContext.ValueProvider.GetValue("roles").AttemptedValue;
        if (roles == null) yield break;
        foreach (var role in roles.Split(',')) {
            yield return new Role { RoleName = role };
        }
    }
}
}