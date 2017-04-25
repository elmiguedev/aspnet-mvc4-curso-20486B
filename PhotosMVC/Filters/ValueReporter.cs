using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PhotosMVC.Filters
{
    /**
     * 
     * Los Filtros son "Anotaciones" que se pueden poner sobre las Acciones de los controllers
     * y que ejecutan alguna accion previa a que se ingrese a la accion misma. Por ejemplo
     * se pueden utilizar para Acciones de autorizacion, debug o cacheo.
     * 
     * Este ejemplo es para hacer un filtro que logee todo lo que hace nuestra aplicacion (debug)
     * 
     */

    public class ValueReporter : ActionFilterAttribute
    {

        /// <summary>
        /// Debug de las acciones realizadas a los controllers etiquetados
        /// </summary>
        /// <param name="routeData"></param>
        private void logValues(RouteData routeData)
        {
            var controller = routeData.Values["controller"];
            var action = routeData.Values["action"];

            var message = string.Format("Controller: {0}; Action: {1}", controller, action);

            Debug.WriteLine(message, "ActionValues");

            foreach (var item in routeData.Values)
            {
                Debug.WriteLine(">> Key: {0}; Value: {1}",item.Key, item.Value);
            }
        }

        /// <summary>
        /// Evento de la llamada al filtro
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            logValues(filterContext.RouteData);
        }

    }
}