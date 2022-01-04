using System.Data.Objects;
using System.Data;
using HConnexum.Infraestructura;

namespace HConnexum.Tramitador.Datos
{
	public static class ExtensionesEntityFramework
	{
		public static bool IsAttachedTo(this ObjectContext context, object entity)
		{
			ObjectStateEntry entry;
			bool isAttached = false;
			if(context.ObjectStateManager.TryGetObjectStateEntry(context.CreateEntityKey(entity.GetType().Name.Pluralizar(), entity), out entry))
				isAttached = entry.State != EntityState.Detached;
			else
				isAttached = false;
			return isAttached;
		}
	}
}